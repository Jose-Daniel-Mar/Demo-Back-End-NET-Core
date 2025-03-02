using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Data.Interface
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
