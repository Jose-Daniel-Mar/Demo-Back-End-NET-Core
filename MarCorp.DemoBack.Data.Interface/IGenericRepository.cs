namespace MarCorp.DemoBack.Data.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        #region Métodos Síncronos

        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string Id);
        T Get(string Id);
        IEnumerable<T> GetAll();

        #endregion

        #region Métodos Asíncronos

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string Id);
        Task<T> GetAsync(string Id);
        Task<IEnumerable<T>> GetAllAsync();

        #endregion
    }
}
