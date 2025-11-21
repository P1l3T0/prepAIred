using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace prepAIred.Data
{
    public class ProfilePictureDTO
    {
        [Required]
        public IFormFile? ImageFile { get; set; }
    }
}
