using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdApi.Models
{
    public class Photo
    {
        public int Id { get; private set; }

        [Required]
        [Url]
        public string? URL { get; set; }

        [JsonIgnore]
        public Ad? Ad { get; set; }
    }
}
