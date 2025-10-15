using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MobileShop.Models;
using System.Linq;

namespace MobileShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly MobileShopDbContext _context;

        public AdminController(MobileShopDbContext context)
        {
            _context = context;
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public IActionResult Index()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Account");

            return View();
        }

        // 🧱 Partial: Hiển thị danh sách sản phẩm
        public IActionResult SanPhamPartial()
        {
            var products = _context.SanPhams.ToList();
            return PartialView("_SanPhamPartial", products);
        }

        // 🧱 Partial: Hiển thị danh sách khách hàng
        public IActionResult KhachHangPartial()
        {
            var customers = _context.KhachHangs.ToList();
            return PartialView("_KhachHangPartial", customers);
        }

        // 🧱 Partial: Hiển thị danh sách đơn hàng
        public IActionResult DonHangPartial()
        {
            var orders = _context.DonHangs.ToList();
            return PartialView("_DonHangPartial", orders);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
