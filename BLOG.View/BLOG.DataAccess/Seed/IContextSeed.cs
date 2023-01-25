namespace BLOG.DataAccess.Seed
{
    using System.Threading.Tasks;

    public interface IContextSeed
    {
        BlogContext Context { get; set; }

        Task Execute(bool isProduction);
    }
}