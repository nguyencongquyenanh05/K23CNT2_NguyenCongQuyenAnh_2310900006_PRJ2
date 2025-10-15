using Microsoft.AspNetCore.Mvc;
using MobileShop.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MobileShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly MobileShopDbContext _context;

        public AccountController(MobileShopDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            // Nếu đã đăng nhập rồi thì điều hướng cho đúng vai trò
            var role = HttpContext.Session.GetString("Role");
            if (role == "Admin")
                return RedirectToAction("Index", "Admins");
            else if (role == "User")
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "⚠️ Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            // 🔹 Kiểm tra ADMIN
            var admin = _context.Admins
                .FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("Role", "Admin");
                HttpContext.Session.SetString("UserName", admin.FullName ?? admin.Username ?? "Admin");
                HttpContext.Session.SetInt32("UserID", admin.AdminId);

                // 👉 Chuyển đến trang quản trị
                return RedirectToAction("Index", "Admins");
            }

            // 🔹 Kiểm tra KHÁCH HÀNG
            var user = _context.KhachHangs
                .FirstOrDefault(u => u.Email == username && u.MatKhau == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Role", "User");
                HttpContext.Session.SetString("UserName", user.HoTen ?? user.Email ?? "Khách hàng");
                HttpContext.Session.SetInt32("UserID", user.MaKh);

                // 👉 Chuyển đến trang người dùng
                return RedirectToAction("Index", "Home");
            }

            // 🚫 Sai thông tin đăng nhập
            ViewBag.Error = "❌ Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
