using AutoMapper;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface;
using MarCorp.DemoBack.Domain.Interface;
using MarCorp.DemoBack.Support.Common;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace MarCorp.DemoBack.Application.Main
{
    public class CategoriesApplication : ICategoriesApplication
    {
        private readonly ICategoriesDomain _categoriesDomain;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly IAppLogger<CategoriesApplication> _logger;

        public CategoriesApplication(ICategoriesDomain categoriesDomain, IMapper mapper, IDistributedCache distributedCache, IAppLogger<CategoriesApplication> logger)
        {
            _categoriesDomain = categoriesDomain;
            _mapper = mapper;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<CategoriesDTO>>> GetAllAsync()
        {
            bool redisAvailable = true;
            var response = new Response<IEnumerable<CategoriesDTO>>();
            var cacheKey = "categoriesList";

            // 1. Intento obtener datos de Redis con manejo específico de excepciones
            try
            {
                var redisCategories = await _distributedCache.GetAsync(cacheKey);
                if (redisCategories != null)
                {
                    response.Data = JsonSerializer.Deserialize<IEnumerable<CategoriesDTO>>(redisCategories);
                    response.IsSuccess = true;
                    response.Message = "Exito: Datos desde Redis";
                    return response;
                }
            }
            catch (Exception ex)
            {
                redisAvailable = false;  // Marcar Redis como no disponible
                _logger.LogWarning($"Redis no disponible, obteniendo datos de la base de datos. Error:{ex.Message}");
            }

            // 2. Si Redis falla o no tiene datos, obtener de la base de datos
            try
            {
                var categories = await _categoriesDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CategoriesDTO>>(categories);
                if (response.Data == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontraron datos en base";
                    return response;
                }

                // 3. Intentar cachear solo si Redis estaba disponible
                if (redisAvailable)
                {
                    try
                    {
                        var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));
                        var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                        await _distributedCache.SetAsync(cacheKey, serializedCategories, options);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Error al intentar guardar datos en Redis. Error:{ex.Message}");
                    }
                }
                response.IsSuccess = true;
                response.Message = "Exito: Datos desde Base de Datos";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {ex.Message}";
                _logger.LogError($"Error al obtener datos de la base de datos. Error:{ex.Message}");
            }
            return response;
        }
    }
}
