using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileShop.Models;

namespace MobileShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly MobileShopDbContext _context;

        public HomeController(MobileShopDbContext context)
        {
            _context = context;
        }

        // 🏠 Trang chủ
        public IActionResult Index()
        {
            // Lấy danh sách sản phẩm (có thể là nổi bật)
            var sanPhams = _context.SanPhams
                                   .Include(sp => sp.MaDanhMucNavigation)
                                   .ToList();

            // Truyền danh sách sản phẩm ra view
            return View(sanPhams);
        }

        // ⚙️ Trang quản trị
        [Route("Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
