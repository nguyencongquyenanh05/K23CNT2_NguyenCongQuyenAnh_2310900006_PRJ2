using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MobileShop.Models;

namespace MobileShop.Controllers
{
    public class ChiTietSanPhamsController : Controller
    {
        private readonly MobileShopDbContext _context;

        public ChiTietSanPhamsController(MobileShopDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietSanPhams
        public async Task<IActionResult> Index()
        {
            var mobileShopDbContext = _context.ChiTietSanPhams.Include(c => c.MaSpNavigation);
            return View(await mobileShopDbContext.ToListAsync());
        }

        // GET: ChiTietSanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSanPham = await _context.ChiTietSanPhams
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaCtsp == id);
            if (chiTietSanPham == null)
            {
                return NotFound();
            }

            return View(chiTietSanPham);
        }

        // GET: ChiTietSanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp");
            return View();
        }

        // POST: ChiTietSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCtsp,MaSp,CauHinh,MoTaChiTiet,SoLuongTon,MauSac")] ChiTietSanPham chiTietSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietSanPham.MaSp);
            return View(chiTietSanPham);
        }

        // GET: ChiTietSanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSanPham = await _context.ChiTietSanPhams.FindAsync(id);
            if (chiTietSanPham == null)
            {
                return NotFound();
            }
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietSanPham.MaSp);
            return View(chiTietSanPham);
        }

        // POST: ChiTietSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaCtsp,MaSp,CauHinh,MoTaChiTiet,SoLuongTon,MauSac")] ChiTietSanPham chiTietSanPham)
        {
            if (id != chiTietSanPham.MaCtsp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietSanPhamExists(chiTietSanPham.MaCtsp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaSp"] = new SelectList(_context.SanPhams, "MaSp", "MaSp", chiTietSanPham.MaSp);
            return View(chiTietSanPham);
        }

        // GET: ChiTietSanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietSanPham = await _context.ChiTietSanPhams
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(m => m.MaCtsp == id);
            if (chiTietSanPham == null)
            {
                return NotFound();
            }

            return View(chiTietSanPham);
        }

        // POST: ChiTietSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietSanPham = await _context.ChiTietSanPhams.FindAsync(id);
            if (chiTietSanPham != null)
            {
                _context.ChiTietSanPhams.Remove(chiTietSanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietSanPhamExists(int id)
        {
            return _context.ChiTietSanPhams.Any(e => e.MaCtsp == id);
        }
    }
}
