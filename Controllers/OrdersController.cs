#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Data;
using Project.Models;
using System.Net;
using System.Net.Mail;

namespace Project.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderContext _context;

        public OrdersController(OrderContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        
        // GET: Orders/Create
        public IActionResult Create()
        {

           
               //Return the cart and address to the view.

            ViewData["Cart"] = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("cart"));
            ViewBag.Address = HttpContext.Session.GetString("address");
            return View();
         
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(string AddressID, string Email)
        {
            
         int totalAmount = 0;
                int totalPrice = 0;
                string body = "<h1>The following order</h1>";

                //Send and email with the order and save it to the database

                foreach (var item in JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("cart")))
                {
                    body += $"<span>X{item.Amount}, {item.Title}, {item.Price}kr  </ span ><br> ";
                     var order = new Order();
            order.Amount = item.Amount;
            order.ProductID = item.ProductID;
            order.Sum = item.Price;
            order.AddressID = Int32.Parse(AddressID);
            order.Date = DateTime.Today.ToString("yyyy/MM/dd");
            _context.Add(order);
            await _context.SaveChangesAsync();
                    totalAmount += item.Amount;
                    totalPrice += item.Price;
                }
                body += $"<span> {totalAmount} items {totalPrice} kr <span> <h2> will be deilvered to</h2> ";

                var address = JsonConvert.DeserializeObject<Address>(HttpContext.Session.GetString("address"));

                body += $" <span>{address.Street}, {address.Zipcode}, {address.City}, {address.Country}  </ span> ";



            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("mans16160@gmail.com"),

                    Subject = "Order confirmation",
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add($"{Email}");
                smtp.Host = "smtp.gmail.com ";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("mans16160@gmail.com", "****");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Send(mailMessage);
                return View();
        }
     
            // GET: Orders/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {

            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order); 

            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

           
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("OrderID,Date,Sum,Amount,ProductID,AddressID")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (HttpContext.Session.GetString("Logged in") == "true")
            {
                      if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order); 

            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

     
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int? id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
