using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using WebApplication1.Models;
using X.PagedList.Extensions;

namespace WebApplication1.Controllers
{
    public class SanPhamController : Controller
    {
        AppDbContext _context;
        public SanPhamController(AppDbContext db)
        {
            _context = db;
        }
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var name = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(name))
            {
                return Content("Chưa đang nhập");
            }
            else
            {
                var sanPhams = _context.SanPhams.ToPagedList(page, pageSize);
                return View(sanPhams);
            }
        }
        [HttpPost]
        public ActionResult Index(string key, int page = 1, int pageSize = 3)
        {

            if (string.IsNullOrEmpty(key))
            {
                return View(_context.SanPhams.ToPagedList(page, pageSize));
            }
            else
            {
                key = key.ToLower().Trim();
                var result = _context.SanPhams.Where(c => c.Name.ToLower().Contains(key)).ToPagedList(page, pageSize);
                if (result == null)
                {
                    ViewBag.Message = "Không có sản phẩm nào";
                    return View();
                }
                else
                {
                    return View(result);
                }
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SanPham sanPham, IFormFile Img)
        {
            try
            {
                if (Img != null && Img.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", Img.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Img.CopyTo(stream);
                    }
                    sanPham.ImgUrl = Img.FileName;
                }
                _context.SanPhams.Add(sanPham);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var sanPham = _context.SanPhams.Find(id);
            return View(sanPham);
        }
        [HttpPost]
        public ActionResult Edit(SanPham sanPham)
        {
            _context.SanPhams.Update(sanPham);
            _context.SaveChanges(true);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Buy(Guid idSanPham, int soLuong)
        {
            var name = HttpContext.Session.GetString("Name");
            var idGioHang = Guid.Parse(HttpContext.Session.GetString("IdGioHang"));
            if (string.IsNullOrEmpty(name))
            {
                return Content("Chưa đang nhập");
            }
            else
            {
                var check = _context.ChiTietGioHangs.FirstOrDefault(c => c.SanPhamId == idSanPham && c.GioHangId == idGioHang);
                if (check == null)
                {
                    var chiTietGioHang = new ChiTietGioHang()
                    {
                        SanPhamId = idSanPham,
                        GioHangId = Guid.Parse(HttpContext.Session.GetString("IdGioHang")),
                        SoLuong = soLuong
                    };
                    _context.ChiTietGioHangs.Add(chiTietGioHang);
                }
                else
                {
                    check.SoLuong += 1;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
