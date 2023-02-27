namespace BLOG.DataAccess.Seed
{
    using BLOG.Entities;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleSeed : IContextSeed
    {
        public ArticleSeed(BlogContext context)
        {
            this.Context = context;
        }

        public BlogContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Articles.Any() && !isProduction)
            {
                List<TagEntity> tagEntities = await this.Context.Tags.ToListAsync();

                ArticleEntity article = new ArticleEntity()
                {
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ![MonImage][MonImage1] Morbi hendrerit ultrices tristique. Praesent eget nibh tempor, consequat lorem id, pulvinar lacus. Cras metus lorem, congue eu nibh eu, sollicitudin molestie quam. Curabitur fermentum, justo interdum suscipit ullamcorper, leo tortor porta diam, id hendrerit quam nisi non felis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed odio leo, malesuada consectetur lorem nec, rutrum vestibulum massa. Maecenas consectetur, massa non varius volutpat, nunc nisi hendrerit justo, nec vulputate leo massa ac risus. Aliquam eget dui a dolor tempus varius luctus et mi. Nam mollis semper cursus. Curabitur gravida fringilla augue in commodo. Phasellus quis purus vel enim rhoncus imperdiet eu a libero. Morbi at urna ut enim malesuada faucibus. Donec egestas, augue vel vehicula porttitor, arcu nulla luctus purus, quis ornare nunc eros tincidunt nisl. Suspendisse aliquet neque quis nisi feugiat, eget tincidunt nunc commodo.\r\n\r\nNam dignissim in nunc vulputate ultrices. Vivamus vulputate tincidunt odio in facilisis. Maecenas euismod vitae turpis in pharetra. Aenean aliquet tincidunt sodales. Aliquam sem ante, ornare at lacinia in, viverra ac sem. Suspendisse potenti. Cras dictum sem nisl, a lobortis felis maximus vel. Ut rutrum massa at neque pulvinar, at mattis ex commodo. Nulla laoreet elit eu ipsum mattis, ac volutpat ligula ullamcorper. Maecenas commodo, risus ac interdum blandit, dolor dolor vestibulum ex, nec ornare leo odio vel erat. Sed sit amet tincidunt turpis. Cras tortor massa, fermentum vitae condimentum molestie, molestie ac dui. Vestibulum eu justo ut turpis sollicitudin interdum a id lacus.\r\n\r\nFusce dictum vitae est at pulvinar. Donec dui justo, dapibus a lobortis ut, facilisis quis felis. Mauris commodo consequat lacinia. Vestibulum ac enim euismod, suscipit purus id, malesuada purus. Curabitur maximus semper lectus, ut feugiat velit pellentesque quis. Praesent maximus, dolor sed pulvinar gravida, dui massa aliquet ligula, eget iaculis metus nunc id mi. Donec imperdiet vulputate orci, vel porta augue eleifend vitae. Vivamus in tortor maximus diam facilisis fringilla sodales porttitor erat. Quisque pellentesque a nisi id consectetur. Sed condimentum leo vitae euismod tempus. Aliquam ac hendrerit dui. Nunc finibus iaculis eleifend. Morbi velit nulla, consectetur tincidunt commodo venenatis, lacinia ut orci. Pellentesque vehicula et magna eget vulputate. Praesent vel tristique lacus. Suspendisse viverra a sapien eu maximus.\r\n\r\nVivamus mattis nulla quis purus porta convallis. Phasellus in magna congue eros vehicula varius. Maecenas volutpat finibus nibh, sed tristique nunc consequat sed. Curabitur sit amet dui nec nulla posuere suscipit. Suspendisse potenti. Curabitur quis justo lectus. Pellentesque viverra non nisl quis vehicula. Donec tempus, ligula non luctus pulvinar, erat libero suscipit nisl, eu iaculis sem dolor sit amet est. Quisque lobortis dolor nec tincidunt scelerisque. Suspendisse tincidunt risus justo, sed consectetur enim porttitor eget. Morbi non mollis odio. Aliquam ultrices eros vel feugiat efficitur. Fusce sagittis at urna pulvinar congue. Proin egestas neque a lectus condimentum hendrerit. Pellentesque nec lectus id risus porttitor consequat.\r\n\r\nDuis in iaculis odio, vel venenatis arcu. Proin et felis laoreet, viverra risus ac, volutpat nisi. Nunc interdum metus in sem suscipit aliquet. Nam nunc velit, volutpat non sem in, sagittis elementum nibh. In urna ipsum, scelerisque eu nibh ut, gravida aliquam leo. Etiam semper purus at orci gravida dapibus. Duis arcu tortor, dictum in dictum eu, porta in nibh. Mauris elementum aliquam faucibus. Sed placerat sem dolor, porta elementum libero viverra id. Proin sed nibh vel ex cursus facilisis sit amet quis nisl. Nam viverra facilisis metus non fringilla. Fusce venenatis tincidunt ipsum. Sed viverra consequat gravida. In efficitur id quam vel convallis. Vestibulum eget orci tincidunt, fermentum ante a, ullamcorper sem. Nullam sit amet justo vitae nunc ornare auctor ac commodo sapien.\r\n\r\nMauris nibh velit, elementum eget egestas quis, rutrum at dolor. Aliquam blandit finibus tortor, quis sagittis justo eleifend eu. Praesent cursus rutrum tempor. Quisque laoreet est quis sapien interdum, eu ultricies sapien egestas. Proin tempus ullamcorper magna non placerat. Donec tristique, mi mattis maximus venenatis, velit nunc sollicitudin neque, nec tempus purus arcu at metus. Nam ullamcorper interdum nisi, vestibulum lacinia felis vehicula et. Sed placerat lobortis urna, non dapibus lectus tempor sit amet.",
                    Description = "Fake description",
                    Slug = "Entity",
                    Title = "Mon titre de test",
                    Views = 0,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = null,
                };

                await this.Context.Articles.AddAsync(article);
                await this.Context.SaveChangesAsync();

                if (tagEntities.Any())
                {
                    await this.Context.ArticlesTags.AddAsync(new ArticleTagEntity()
                    {
                        ArticleEntityId = article.Id,
                        TagEntityId = tagEntities.First().Id,
                    });
                }

                if (tagEntities.Count >= 2)
                {
                    await this.Context.ArticlesTags.AddAsync(new ArticleTagEntity()
                    {
                        ArticleEntityId = article.Id,
                        TagEntityId = tagEntities.Last().Id,
                    });
                }

                await this.Context.SaveChangesAsync();
            }
        }
    }
}