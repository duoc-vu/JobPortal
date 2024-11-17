using Microsoft.AspNetCore.Mvc;
using JobPortal.Models;
using JobPortal.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace JobPortal.Controllers
{
    public class UserController : Controller
    {
        JobPortalDbContext db = new JobPortalDbContext();
        private readonly IUserRepository _userRepository;
        // Biến toàn cục theo dõi số lần đăng nhập
        private static int _loginAttempts = 0;
        private static DateTime _firstAttemptTime = DateTime.MinValue;
        private const int MaxAttempts = 3;
        private const int LockoutDurationMinutes = 1;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TblAccount user)
        {
            // Kiểm tra nếu số lần đăng nhập thất bại vượt quá giới hạn trong vòng 30 phút
            if (_loginAttempts >= MaxAttempts)
            {
                if ((DateTime.Now - _firstAttemptTime).TotalMinutes < LockoutDurationMinutes)
                {
                    ModelState.AddModelError("", "Hệ thống đã khóa đăng nhập trong 30 phút do quá số lần thử.");
                    ViewData["IsLocked"] = true;  // Gửi trạng thái khóa đến View
                    return View(user);
                }
                else
                {
                    // Đặt lại bộ đếm nếu đã qua thời gian khóa
                    _loginAttempts = 0;
                    _firstAttemptTime = DateTime.MinValue;
                }
            }

            var account = db.TblAccounts.FirstOrDefault(x => x.SUsername == user.SUsername && x.SPassword == user.SPassword);
            if (account != null)
            {
                HttpContext.Session.SetInt32("IRoleId", account.IRoleId ?? 0); // Lưu vào Session
                HttpContext.Session.SetString("UserName", account.SUsername);
                HttpContext.Session.SetInt32("IUserId", account.IUserId);

                _loginAttempts = 0;  // Reset số lần đăng nhập sau khi đăng nhập thành công
                return RedirectToAction("Index", "Home", new {id = account.IUserId });
            }
            else
            {
                // Xử lý tăng số lần đăng nhập thất bại
                if (_loginAttempts == 0)
                {
                    _firstAttemptTime = DateTime.Now;
                }
                _loginAttempts++;

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
                return View(user);
            }
        }




        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TblAccount user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);

            }
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TblAccounts.Where(x => x.SUsername.Equals(user.SUsername)).ToList();
                if (u.Count == 0)
                {
                    db.TblAccounts.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa tất cả session
            return RedirectToAction("Login", "User"); // Chuyển hướng về trang đăng nhập
        }

        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("IUserId");
            var role = HttpContext.Session.GetInt32("IRoleId"); // Giả sử RoleId được lưu trong session

            if (id == null || role == null)
            {
                return RedirectToAction("Login", "User"); // Nếu không có userId hoặc role, chuyển hướng đến trang đăng nhập
            }

            if (role == 3) // Candidate
            {
                var user = db.TblCandidates.FirstOrDefault(c => c.IUserId == id);

                if (user == null)
                {
                    ViewData["ProfileMessage"] = "Bạn chưa đăng ký thông tin. Vui lòng đăng ký thông tin cá nhân!";
                    return View(); 
                }

                return View(user); 
            }
            else if (role == 2) 
            {
                var user = db.TblEmployers.FirstOrDefault(e => e.IUserId == id);

                if (user == null)
                {
                    ViewData["ProfileMessage"] = "Bạn chưa đăng ký thông tin nhà tuyển dụng. Vui lòng đăng ký thông tin công ty!";
                    return View(); 
                }

                return View(user); 
            }

            return RedirectToAction("Login", "User");
        }


        [HttpGet]
        public IActionResult RegisterProfile()
        {
            var role = HttpContext.Session.GetInt32("IRoleId"); 
            if (role == 3) 
            {
                return View(new JobPortal.Models.TblCandidate());
            }
            else if (role == 2) 
            {
                return View(new JobPortal.Models.TblEmployer());
            }
            return View();
        }

        [HttpPost]
        public IActionResult RegisterProfile(TblCandidate candidate, TblEmployer employer)
        {
            var id = HttpContext.Session.GetInt32("IUserId");
            var role = HttpContext.Session.GetInt32("IRoleId"); 

            if (role == 3)
            {
                candidate.IUserId = id.Value;
                db.TblCandidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Profile", "User");
            }
            else if (role == 2)
            {
                employer.IUserId = id.Value;
                db.TblEmployers.Add(employer);
                db.SaveChanges();
                return RedirectToAction("Profile", "User");
            }
            return View();
        }
    }
}
