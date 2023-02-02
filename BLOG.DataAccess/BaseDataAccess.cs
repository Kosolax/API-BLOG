namespace BLOG.DataAccess
{
    using BLOG.DataAccess.Interfaces;
    using BLOG.Entities;

    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BaseDataAccess<T> : IBaseDataAccess<T> where T : BaseEntity
    {
        public BaseDataAccess(BlogContext context)
        {
            this.Context = context;
        }

        public BlogContext Context { get; private set; }

        public virtual async Task<int> Count()
        {
            return await this.Context.Set<T>().CountAsync();
        }

        public virtual async Task<T> Create(T itemToCreate)
        {
            await this.Context.Set<T>().AddAsync(itemToCreate);
            await this.Context.SaveChangesAsync();

            return itemToCreate;
        }

        public virtual async Task Delete(params object[] keyValues)
        {
            T item = await this.Context.Set<T>().FindAsync(keyValues);
            if (item != null)
            {
                this.Context.Set<T>().Remove(item);
                await this.Context.SaveChangesAsync();
            }
        }

        public virtual async Task<T> Find(params object[] keyValues)
        {
            return await this.Context.Set<T>().FindAsync(keyValues);
        }

        public virtual async Task<List<T>> List()
        {
            return await this.Context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> ListSkipTake(int skip, int take)
        {
            return await this.Context.Set<T>().Skip(skip).Take(take).ToListAsync();
        }

        public virtual async Task<T> Update(T itemToUpdate, T item)
        {
            this.Context.Entry(item).CurrentValues.SetValues(itemToUpdate);
            await this.Context.SaveChangesAsync();

            return itemToUpdate;
        }
    }
}