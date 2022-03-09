#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Amount++;
            var list = new List<Product>();

            if (HttpContext.Session.GetString("cart") == null)
            {
                list = new List<Product>();
                list.Add(product);
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("cart"));
                bool NoMatch = true;
                foreach (var item in list)
                {
                    if (product.ProductID == item.ProductID)
                    {
                        
                        
                        list.First(d => d.ProductID == item.ProductID).Amount += product.Amount;
                        list.First(d => d.ProductID == item.ProductID).Price += product.Price;
                        NoMatch = false;
                        break;
                    }

                }

                if (NoMatch == true)
                {
                    list.Add(product);
                }
            }
            
            var jsonProduct= JsonConvert.SerializeObject(list);
      
      
            HttpContext.Session.SetString("cart", jsonProduct);
      

            return View(await _context.Products.ToListAsync());
        }
        // GET: Products

        public IActionResult Cart()
        {
            if(HttpContext.Session.GetString("cart") != null) { 
            ViewData["Cart"] = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("cart"));
            }
            /*var total = JsonConvert.DeserializeObject<Total>(HttpContext.Session.GetString("cart"));*/
            return View();
        }
        [HttpPost, ActionName("Cart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int? id)
        { 
            if (HttpContext.Session.GetString("cart") != null)
            {
             var list = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("cart"));
            var item = list.FirstOrDefault(c => c.ProductID == id);
                    int singlePrice = item.Price / item.Amount;
                      item.Amount --;
                    item.Price = singlePrice * item.Amount;
                   
                    if (item.Amount == 0)
                      {
                          list.Remove(item);
                      }
                if (list.Count == 0)
                {
                    HttpContext.Session.Remove("cart");
                }
                else { 
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(list));
                ViewData["Cart"] = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("cart"));
                }
            
                
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            

           ViewBag.Login = HttpContext.Session.GetString("Logged in");
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Users");
            }


        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Title,Description,Price,ImagePath")] Product product, IFormFile Image)
        { if (ModelState.IsValid)
            {
            if (Image != null)
            {

                //Set Key Name
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);

                //Get url To Save
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageName);

                    product.ImagePath = SavePath;

                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
            }
            
           
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                  if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

          
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProductID,Title,Description,Price,Image")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (HttpContext.Session.GetString("Logged in") == "true")
            {
               
     if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

       
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int? id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
