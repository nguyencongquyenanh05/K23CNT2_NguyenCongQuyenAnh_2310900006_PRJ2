using Microsoft.EntityFrameworkCore;
using MobileShop.Models;

var builder = WebApplication.CreateBuilder(args);

// 🧩 Đăng ký DbContext
builder.Services.AddDbContext<MobileShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🧩 Thêm MVC
builder.Services.AddControllersWithViews();

// ✅ Thêm HttpContextAccessor (để truy cập Session trong View)
builder.Services.AddHttpContextAccessor();

// ✅ Cấu hình Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session tồn tại
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 🧱 Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Phải bật Session **trước** Authorization
app.UseSession();

// (Nếu sau này bạn dùng [Authorize] thì thêm dòng này)
app.UseAuthentication();
app.UseAuthorization();

// ✅ Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// 👉 Nếu bạn muốn mặc định chạy trang chủ:
app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
