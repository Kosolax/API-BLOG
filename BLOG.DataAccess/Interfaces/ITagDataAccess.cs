namespace BLOG.DataAccess.Interfaces
{
    using BLOG.Entities;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITagDataAccess : IBaseDataAccess<TagEntity>
    {
        Task<bool> DoListExist(List<int> list);

        Task<List<TagEntity>> ListFromIds(List<int> ids);
    }
}