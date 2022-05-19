using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdApi.Models
{
    public class Ad
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? CreatedDate { get; private set; }

        [MaxLength(3)]
        [MinLength(1)]
        public List<Photo>? Photos { get; set; }
    }

    public class AdDTO
    {
        public string? Title { get; set; }

        public decimal Price { get; set; }

        public string? MainPhoto { get; set; }

        public AdDTO(Ad ad)
        {
            Title = ad.Title;
            Price = ad.Price;
            MainPhoto = ad.Photos!.First().URL;
        }
    }

    public class AdWithDescDTO : AdDTO
    {
        public string? Description { get; set; }

        public AdWithDescDTO(Ad ad)
            : base(ad)
        {
            Description = ad.Description;
        }
    }

    public class AdWithPhotosDTO : AdDTO
    {
        public IEnumerable<string?> Photos { get; set; }

        public AdWithPhotosDTO(Ad ad)
            : base(ad)
        {
            Photos = ad.Photos!.Select(p => p.URL);
        }
    }

    public class AdDetailedDTO : AdDTO
    {
        public string? Description { get; set; }

        public IEnumerable<string?> Photos { get; set; }

        public AdDetailedDTO(Ad ad)
            : base(ad)
        {
            Photos = ad.Photos!.Select(p => p.URL);
            Description = ad.Description;
        }
    }

}
