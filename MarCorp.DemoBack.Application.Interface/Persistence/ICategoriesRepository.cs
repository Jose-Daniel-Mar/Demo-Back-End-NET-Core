using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Application.Interface.Persistence
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
