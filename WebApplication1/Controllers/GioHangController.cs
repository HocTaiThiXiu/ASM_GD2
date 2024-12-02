using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModel;
using X.PagedList.Extensions;

namespace WebApplication1.Controllers
{
    public class GioHangController : Controller
    {
        AppDbContext _context;
        public GioHangController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var idGioHang = Guid.Parse(HttpContext.Session.GetString("IdGioHang"));
            var gioHang = _context.ChiTietGioHangs.Where(c => c.GioHangId == idGioHang).Join(_context.SanPhams, c => c.SanPhamId, s => s.Id, (c, s) => new GioHangViewModel
            {
                Id = s.Id,
                TenSanPham = s.Name,
                Gia = s.Price,
                SoLuong = c.SoLuong,
                TongTien = c.SoLuong * s.Price,
            }).ToPagedList(page, pageSize);
            return View(gioHang);
        }
        public IActionResult Remove(Guid id)
        {
            var sanPham = _context.SanPhams.Find(id);
            if (sanPham != null)
            {
                var idGioHang = Guid.Parse(HttpContext.Session.GetString("IdGioHang"));
                var chiTietGioHang = _context.ChiTietGioHangs.FirstOrDefault(c => c.SanPhamId == id && c.GioHangId == idGioHang);
                if (chiTietGioHang == null)
                    return Content("Khong co san pham nay");
                else
                {
                    _context.ChiTietGioHangs.Remove(chiTietGioHang);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
                return Content("khong co san pham nay");
        }
        public IActionResult ThanhToan()
        {
            List<ChiTietGioHang> chiTietGioHangs = _context.ChiTietGioHangs.Include(c => c.SanPham).Where(c => c.GioHangId == Guid.Parse(HttpContext.Session.GetString("IdGioHang"))).ToList();
            if (chiTietGioHangs == null)
                return Content("Gio hang trong");
            else
            {
                DateTime thisTime = DateTime.Now;
                var hoaDon = new HoaDon()
                {
                    UserId = Guid.Parse(HttpContext.Session.GetString("UserId")),
                    NgayTao= thisTime,
                    ThanhTien = 0,
                };
                _context.HoaDons.Add(hoaDon);
                _context.SaveChanges();
                decimal tongTien = 0;
                List<ChiTietHoaDonViewModel> chiTietHoaDonViewModels = new List<ChiTietHoaDonViewModel>();
                var currentHoaDon = _context.HoaDons.FirstOrDefault(c => c.UserId == hoaDon.UserId && c.NgayTao == thisTime);
                foreach (var item in chiTietGioHangs)
                {
                    var sanPham= item.SanPham;
                    if (sanPham.SoLuong >= item.SoLuong)
                        sanPham.SoLuong -= item.SoLuong;
                    else
                        return Content($"{sanPham.Name} Không đủ tồn kho");
                    var chiTietHoaDon = new ChiTietHoaDon()
                    {
                        SanPhamId = item.SanPhamId,
                        SoLuong = item.SoLuong,
                        ThanhTien = item.SoLuong * item.SanPham.Price,
                        HoaDonId = currentHoaDon.Id
                    };
                    tongTien += chiTietHoaDon.ThanhTien;
                    _context.ChiTietHoaDons.Add(chiTietHoaDon);
                    var chiTietHoaDonViewModel = new ChiTietHoaDonViewModel()
                    {
                        TenSanPham=item.SanPham.Name,
                        SoLuong=item.SoLuong,
                        ThanhTien=chiTietHoaDon.ThanhTien
                    };
                    chiTietHoaDonViewModels.Add(chiTietHoaDonViewModel);
                    currentHoaDon.ThanhTien = tongTien;
                }
                _context.ChiTietGioHangs.RemoveRange(chiTietGioHangs);
                _context.SaveChanges();
                ViewBag.ThanhTien = currentHoaDon.ThanhTien;
                ViewBag.NgayTao=currentHoaDon.NgayTao;
                return View(chiTietHoaDonViewModels);
            }
        }
    }
}
