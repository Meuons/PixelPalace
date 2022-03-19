#nullable disable

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Web;
using System.Text;

namespace Project.Controllers
{

    public class UsersController : Controller
    {
        private readonly UserContext _context;
        public Uri UrlReferrer { get; }
        public UsersController(UserContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string? Username, string? Password)
        {   //Check that the username and password match with the ones in the database
            var users = await _context.Users.ToListAsync();
             if(users.Count == 0)
            {
                ViewData["message"] = "No user accounts exist";
            }
            foreach (var user in users)
            {
                if (Password == user.Password && Username == user.Username)
                {
                    HttpContext.Session.SetString("Logged in", "true");
                    
                    
                    return RedirectToAction("Index", "Products");
                   
                 
                }
                else
                {
                    ViewData["message"] = "Wrong login credentials";
                }
            }
         
            return View();
        }

   

        public IActionResult Login()
        {
            
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Logged in", "false");
            ViewBag.Login = "false";
            return View();
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            
            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                return View();
               
            }
            else
            {
              return RedirectToAction("Login", "");
            }
            
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Username,Password")] User user)
        {
               bool duplicate = false;
            if (ModelState.IsValid)
            {
                      var usersList = await _context.Users.ToListAsync();

                //Prevent duplicate usernames from being created

                foreach( var item in usersList)
                {
                    if(user.Username == item.Username)
                    {
                        duplicate = true;
                        
                        
                    }
                }
                if(duplicate != true)
                {
                _context.Add(user);  
                await _context.SaveChangesAsync();
                 ViewBag.duplicateUser = false;
                 return RedirectToAction("Login", "Users");
                }
                else{
                    ViewBag.duplicateUser = true;
                    ;
                }

               
            }
             return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
             

            }
            else
            {
                return RedirectToAction("Login");
            }
          
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("UserID,Username,Password")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                 if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);

            }
            else
            {
                return RedirectToAction("Login");
            }


         
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int? id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
