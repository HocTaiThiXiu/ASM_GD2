using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HoaDonController : Controller
    {
        AppDbContext _context;
        public HoaDonController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            var hoaDons= _context.HoaDons.Where(c=>c.UserId==Guid.Parse(HttpContext.Session.GetString("UserId"))).ToList();
            return View(hoaDons);
        }
        public ActionResult Details(Guid id)
        {
            var hoaDon=_context.HoaDons.Find(id);
            if (hoaDon == null)
                return Content("Không có hóa đơn");
            else
            {
                List<ChiTietHoaDonViewModel> chiTietHoaDonVMs = _context.ChiTietHoaDons.Where(c => c.HoaDonId == id).Join(_context.SanPhams,h=>h.SanPhamId,s=>s.Id,(h,s)=> new ChiTietHoaDonViewModel
                {
                    TenSanPham=s.Name,
                    SoLuong=h.SoLuong,
                    ThanhTien=h.ThanhTien,
                }).ToList();
                ViewBag.NgayTao = hoaDon.NgayTao;
                ViewBag.ThanhTien = hoaDon.ThanhTien;
                return View(chiTietHoaDonVMs);
            }

        }
    }
}
