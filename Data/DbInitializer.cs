using AdApi.Models;

namespace AdApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AdContext context)
        {
            if (context.Ads.Any())
            {
                return;
            }

            for (int i = 1; i < 51; i++)
            {
                context.Ads.Add(new Ad
                {
                    Title = $"Ad {i}",
                    Price = i * 1000,
                    Photos = new List<Photo>
                    {
                        new Photo { URL = $"https://test{i}.com/photo1.png" },
                        new Photo { URL = $"https://test{i}.com/photo2.png" },
                        new Photo { URL = $"https://test{i}.com/photo3.png" },
                    },
                });
            }

            context.SaveChanges();
        }
    }
}
