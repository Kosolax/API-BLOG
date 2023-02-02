namespace BLOG.Services.Interfaces
{
    using CSharpFunctionalExtensions;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseService<T, T1>
    {
        Task<Result<T>> Create(T1 itemToCreate);

        Task Delete(int id);

        Task<Result<T>> Get(int id);

        Task<Result<List<T>>> List();

        Task<Result<T>> Update(T1 itemToUpdate, int id);
    }
}