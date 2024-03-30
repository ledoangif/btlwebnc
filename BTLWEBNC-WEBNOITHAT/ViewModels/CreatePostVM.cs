using BTLWEBNC_WEBNOITHAT.Models;
using System.ComponentModel.DataAnnotations;

namespace BTLWEBNC_WEBNOITHAT.ViewModels
{
    public class CreatePostVM
    {
        public int Id { get; set; }
        [Required]
        public string? TieuDe { get; set; }
        public string? SContent { get; set; }
        public string? TacGia { get; set; }
        
        public string? Content { get; set; }
        public string? TenFileAnh { get; set; }
        public IFormFile? Anh { get; set; }
    }
}
