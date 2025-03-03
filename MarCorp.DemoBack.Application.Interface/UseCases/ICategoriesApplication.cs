using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Support.Common;

namespace MarCorp.DemoBack.Application.Interface.UseCases
{
    public interface ICategoriesApplication
    {
        Task<Response<IEnumerable<CategoriesDTO>>> GetAllAsync();
    }
}