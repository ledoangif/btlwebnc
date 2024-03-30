using BTLWEBNC_WEBNOITHAT.Models;

namespace BTLWEBNC_WEBNOITHAT.ViewModels
{
    public class BlogPostVM
    {
        public int Id { get; set; }
        public string? TieuDe { get; set; }
        public string? TacGia { get; set; }
        public DateTime NgayDang { get; set; }
        public string? TenFileAnh { get; set; }
        public string? Scontent { get; set; }
        public string? Content { get; set; }


    }
}
