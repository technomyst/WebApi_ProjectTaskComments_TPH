namespace WebApi_ProjectTaskComments.Services.Interfaces
{
    public interface IUniversalService<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByParentItemId(int parentItemId);
        T? GetById(int id);
        T Create(T item);
        T? Update(T item);
        //bool Delete(int id);
        bool Delete(T item);
    }
}
