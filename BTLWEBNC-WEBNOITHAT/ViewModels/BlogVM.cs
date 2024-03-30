using BTLWEBNC_WEBNOITHAT.Models;
using X.PagedList;

namespace BTLWEBNC_WEBNOITHAT.ViewModels
{
    public class BlogVM
    {
        public string? TieuDe { get; set; }
        public string? SContent { get; set; }
        public string? TenFileAnh { get; set; }
        public IPagedList<TBlog>? Blog { get; set; }
    }
}
