using BTLWEBNC_WEBNOITHAT.Models;
using BTLWEBNC_WEBNOITHAT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using X.PagedList;

namespace BTLWEBNC_WEBNOITHAT.Controllers
{
    public class BlogController : Controller
    {
        QLBanNoiThatContext db = new QLBanNoiThatContext();

        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }
        public IActionResult Blog(int? page)
        {
            var userClaims = User.Identity as ClaimsIdentity;

            if (userClaims.IsAuthenticated)
            {
                if (userClaims != null)
                {
                    // Find the claim by its type (ClaimTypes.NameIdentifier in this case)
                    var usernameClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);
                    var idLogin = userClaims.FindFirst("IDUseName");
                    var tenusername = userClaims.FindFirst("UseName");
                    if (usernameClaim != null)
                    {
                        string username1 = tenusername.Value;
                        string username = usernameClaim.Value;
                        TempData["Username"] = username1;
                        TempData["LoginData"] = username;
                        // Now, 'username' contains the value of the Claim with ClaimTypes.NameIdentifier.
                    }


                    int pageSize = 4;
                    int pageNumber = page == null || page < 0 ? 1 : page.Value;
                    var lsttintuc = db.TBlogs.AsNoTracking().OrderBy(x => x.Idblog);
                    PagedList<TBlog> lst = new PagedList<TBlog>(lsttintuc, pageNumber, pageSize);

                    return View(lst);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
        }

        [Route("Blog/{id:int}")]
        public IActionResult BlogDetails(int id)
        {
           
            var blogPost = db.TBlogs.FirstOrDefault(b => b.Idblog == id);

            
            if (blogPost == null)
            {
                return NotFound(); 
            }

            return View(blogPost);
        }



    }
}
