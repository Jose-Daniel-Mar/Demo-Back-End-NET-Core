using MarCorp.DemoBack.Data.Interface;
using MarCorp.DemoBack.Domain.Interface;
using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Domain.Core
{
    public class CategoriesDomain : ICategoriesDomain
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CategoriesDomain(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }
        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _categoriesRepository.GetAllAsync();
        }
    }
}