using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MobileShop.Models;

namespace MobileShop.Controllers
{
    public class AdminsController : Controller
    {
        private readonly MobileShopDbContext _context;

        public AdminsController(MobileShopDbContext context)
        {
            _context = context;
        }

        // 🔒 Kiểm tra quyền đăng nhập admin
        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        // 🔁 Hàm dùng để tự động chuyển hướng nếu chưa đăng nhập
        private IActionResult RequireAdminLogin()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Account");
            return null!;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            var admins = await _context.Admins.ToListAsync();
            return View(admins);
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            if (id == null) return NotFound();

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null) return NotFound();

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            return View();
        }

        // POST: Admins/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,Username,Password,FullName,Email,CreatedAt")] Admin admin)
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            if (!ModelState.IsValid)
                return View(admin);

            admin.CreatedAt ??= DateTime.Now; // Gán ngày tạo nếu chưa có

            _context.Add(admin);
            await _context.SaveChangesAsync();
            TempData["Success"] = "✅ Thêm admin mới thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            if (id == null) return NotFound();

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            return View(admin);
        }

        // POST: Admins/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,Username,Password,FullName,Email,CreatedAt")] Admin admin)
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            if (id != admin.AdminId) return NotFound();

            if (!ModelState.IsValid)
                return View(admin);

            try
            {
                _context.Update(admin);
                await _context.SaveChangesAsync();
                TempData["Success"] = "✅ Cập nhật thông tin admin thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(admin.AdminId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            if (id == null) return NotFound();

            var admin = await _context.Admins.FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null) return NotFound();

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var redirect = RequireAdminLogin();
            if (redirect != null) return redirect;

            var admin = await _context.Admins.FindAsync(id);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
                TempData["Success"] = "🗑️ Đã xóa admin thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}
