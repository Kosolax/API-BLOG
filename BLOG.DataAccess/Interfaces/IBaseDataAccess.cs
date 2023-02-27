namespace BLOG.DataAccess.Interfaces
{
    using BLOG.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseDataAccess<T> where T : BaseEntity
    {
        Task<int> Count();

        Task<T> Create(T itemToCreate);

        Task Delete(params object[] keyValues);

        Task<T> Find(params object[] keyValues);

        Task<List<T>> List();

        Task<List<T>> ListSkipTake(int skip, int take);

        Task<T> Update(T itemToUpdate, T oldItem);
    }
}