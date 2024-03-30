using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTLWEBNC_WEBNOITHAT.Models;
using X.PagedList;
using BTLWEBNC_WEBNOITHAT.ViewModels;

namespace BTLWEBNC_WEBNOITHAT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Blog")]
    public class TBlogsController : Controller
    {
        private readonly QLBanNoiThatContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TBlogsController(QLBanNoiThatContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return _context.TBlogs != null ?
                        View(await _context.TBlogs.ToListAsync()) :
                        Problem("Entity set 'QLBanNoiThatContext.TBlogs'  is null.");
        }
        //Tin tuc
        [Route("danhmuctintuc")]
        public IActionResult DanhMucTinTuc(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsttintuc = _context.TBlogs.AsNoTracking().OrderBy(x => x.Idblog);
            PagedList<TBlog> lst = new PagedList<TBlog>(lsttintuc, pageNumber, pageSize);

            return View(lst);
        }

        [Route("ThemTinTucMoi")]
        [HttpGet]
        public IActionResult ThemTinTucMoi()
        {
            return View(new CreatePostVM());
        }
        [Route("ThemTinTucMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemTinTucMoi(CreatePostVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }


            var post = new TBlog();

            post.TieuDe = vm.TieuDe;
            post.TacGia = vm.TacGia;
            post.Scontent = vm.SContent;
            post.Content = vm.Content;
            post.NgayDang = DateTime.Now;



            if (vm.Anh != null)
            {
                post.TenFileAnh = UploadImage(vm.Anh);

            }

            await _context.TBlogs!.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("DanhMucTinTuc");
        }


        [Route("XoaTinTuc")]
        [HttpGet]
        public IActionResult XoaTinTuc(int id)
        {
            TempData["Message"] = "";

            
            var delBlog = _context.TBlogs.Find(id);

            if (delBlog == null)
            {
                TempData["Message"] = "Không tìm thấy tin tức để xóa!";
                return RedirectToAction("DanhMucTinTuc", "TBlogs");
            }

            _context.Remove(delBlog);
            _context.SaveChanges();

            TempData["Message"] = "Tin tức này đã được xóa!";
            return RedirectToAction("DanhMucTinTuc", "TBlogs");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateTinTuc(int id)
        {
            TempData["Message"] = "";
            var post = await _context.TBlogs!.FirstOrDefaultAsync(x => x.Idblog == id);

            if (post == null)
            {
                TempData["Message"] = "Không tìm thấy tin tức!";
                return View();
            }

            var vm = new CreatePostVM()
            {
                Id = post.Idblog,
                TieuDe = post.TieuDe,
                TacGia = post.TacGia,
                SContent = post.Scontent,
                Content = post.Content,
                TenFileAnh = post.TenFileAnh,
            };

            // Add logging to check the state of vm
            // You can replace Console.WriteLine with your logging framework
            Console.WriteLine($"Model initialized: {vm != null}, TenFileAnh: {vm?.TenFileAnh}");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTinTuc(CreatePostVM vm)
        {
            TempData["Message"] = "";

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var post = await _context.TBlogs!.FirstOrDefaultAsync(x => x.Idblog == vm.Id);

            if (post == null)
            {
                TempData["Message"] = "Không tìm thấy tin tức!";
                return View();
            }

            post.TieuDe = vm.TieuDe;
            post.TacGia = vm.TacGia;
            post.Scontent = vm.SContent;
            post.Content = vm.Content;

            if (vm.Anh != null)
            {
                post.TenFileAnh = UploadImage(vm.Anh);
            }

            
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("DanhMucTinTuc", "TBlogs");
        }

        
        private string UploadImage(IFormFile file)
        {
            try
            {
                string uniqueFileName = "";
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "blog");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                var destFilePath = Path.Combine(folderPath, uniqueFileName);

                using (var stream = new FileStream(destFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Lỗi, không thể upload ảnh: {ex.Message}");
                return null;
            }
        }

        private bool TBlogExists(int id)
        {
            return (_context.TBlogs?.Any(e => e.Idblog == id)).GetValueOrDefault();
        }


    }
}
