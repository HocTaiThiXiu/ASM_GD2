using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        AppDbContext _context;
        public UserController(AppDbContext db)
        {
            _context = db;
        }
        // GET: UserController
        public ActionResult Index(int currentPage, int pageChange)
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (userName == "Admin")
            {
                var users = _context.Users.ToList();
                var lastPage = (int)Math.Ceiling((float)users.Count / (float)5);
                if (currentPage + pageChange > lastPage || currentPage == 0)
                    currentPage = 1;
                else if (currentPage + pageChange < 1)
                    currentPage = (int)lastPage;
                else
                    currentPage += pageChange;
                ViewBag.LastPage = lastPage;
                ViewBag.CurrentPage = currentPage;
                var usersPage = users.OrderBy(c => c.Name).Skip((currentPage - 1) * 5).Take(5).ToList();
                return View(usersPage);
            }
            else
            {
                return Content("Không đủ quyền");
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Regiter()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Regiter(User user)
        {
            try
            {
                _context.Users.Add(user);
                var giohang = new GioHang()
                {
                    UserId = user.Id,
                };
                _context.Add(giohang);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User Update)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _context.Users.Include(u => u.GioHang).FirstOrDefault(c => c.Username == loginViewModel.Username && c.Password == loginViewModel.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("Name", user.Name.ToString());
                HttpContext.Session.SetString("UserName", user.Username.ToString());
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("IdGioHang", user.GioHang.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View(loginViewModel);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("IdGioHang");
            return RedirectToAction("Index", "Home");
        }
    }
}
