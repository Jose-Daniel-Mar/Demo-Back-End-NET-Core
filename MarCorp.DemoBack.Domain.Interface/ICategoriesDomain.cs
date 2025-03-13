using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Domain.Interface
{
    public interface ICategoriesDomain
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
