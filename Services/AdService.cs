using AdApi.Data;
using AdApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdApi.Services
{
    public class AdService
    {
        private readonly AdContext _context;
        private const int ADS_PER_PAGE = 10;

        public AdService(AdContext context)
        {
            _context = context;
            //DbInitializer.Initialize(context);
        }

        public IEnumerable<Ad> GetAll()
        {
            return _context.Ads
                .OrderBy(ad => ad.Id)
                .Skip(ADS_PER_PAGE)
                .Take(10)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<AdDTO> GetPage(int page, string? orderBy)
        {
            var ads = _context.Ads;
            var orderedAds = orderBy switch
            {
                "price" => ads.OrderBy(ad => ad.Price),
                "price_desc" => ads.OrderByDescending(ad => ad.Price),
                "date" => ads.OrderBy(ad => ad.CreatedDate),
                "date_desc" => ads.OrderByDescending(ad => ad.CreatedDate),
                _ => ads.OrderBy(ad => ad.Id),
            };

            return orderedAds
                .Skip(ADS_PER_PAGE * (page - 1))
                .Take(10)
                .Include(ad => ad.Photos)
                .Select(ad => new AdDTO(ad))
                .AsNoTracking()
                .ToList();
        }

        public AdDTO? GetById(int id, string? fields)
        {
            var ad = _context.Ads.Include(ad => ad.Photos).AsNoTracking().SingleOrDefault(ad => ad.Id == id);

            if (ad is not null)
            {
                //all | desc | photos
                return fields switch
                {
                    "all" => new AdDetailedDTO(ad),
                    "desc" => new AdWithDescDTO(ad),
                    "photos" => new AdWithPhotosDTO(ad),
                    _ => new AdDTO(ad)
                };
            }
           
            return null;
        }

        public Ad? Create(Ad ad)
        {
            _context.Ads.Add(ad);
            _context.SaveChanges();
            return ad;
        }
    }
}
