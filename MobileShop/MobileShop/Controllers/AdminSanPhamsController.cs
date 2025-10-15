using Microsoft.AspNetCore.Mvc;
using MobileShop.Models;

namespace MobileShop.Controllers
{
    public class AdminSanPhamsController : Controller
    {
        private readonly MobileShopDbContext _context;

        public AdminSanPhamsController(MobileShopDbContext context)
        {
            _context = context;
        }

        // Danh sách sản phẩm
        public IActionResult Index()
        {
            var sanPhams = _context.SanPhams.ToList();
            return View(sanPhams);
        }

        // Chi tiết sản phẩm
        public IActionResult Details(int id)
        {
            var sanPham = _context.SanPhams.FirstOrDefault(sp => sp.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }
    }
}
