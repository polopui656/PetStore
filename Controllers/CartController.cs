using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetStore.Helper;
using PetStore.Models;

namespace PetStore.Controllers
{
    public class CartController : Controller
    {
        private readonly PetStoreContext _context;
        private readonly ILogger<HomeController> _logger;

        public CartController(ILogger<HomeController> logger, PetStoreContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //Lay cart tu session
            Cart cart = HttpContext.Session.Get<Cart>("cart")??new Cart();
            return View(cart);
        }

        public async Task<IActionResult> Add(int id)
        {
            Product? p = _context.Products.Include(p => p.Category).First(p => p.Id == id);
               
            Item item = new Item
            {
                Id = p.Id,
                Category = p.Category.Name,
                Description = p.Description,
                Discount = p.Discount,
                Price = p.Price,
                Quantity = 1
            };
            //Lay cart tu session
            Cart cart = HttpContext.Session.Get<Cart>("cart")??new Cart();
            //Luu item vao cart
            cart.Add(item);
            //Luu cart vao session
            HttpContext.Session.Set<Cart>("cart", cart);
            //Quay ve /Home/Index de hien lai home page
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            //Lay cart tu session
            Cart cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            //Remove item tu cart
            cart.Remove(id);
            //Luu cart vao session
            HttpContext.Session.Set<Cart>("cart", cart);
            //Quay ve thuc hien action /Cart/Index
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Empty()
        {
            //Lay cart tu session
            Cart cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            //Empty cart
            cart.Empty();
            //Luu cart vao session
            HttpContext.Session.Set<Cart>("cart", cart);
            //Quay ve thuc hien action /Cart/Index
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id, int quantity)
        {
            //Lay cart tu session
            Cart cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            //Empty cart
            cart.Update(id, quantity);
            //Luu cart vao session
            HttpContext.Session.Set<Cart>("cart", cart);
            //Quay ve thuc hien action /Cart/Index
            return RedirectToAction("Index");
        }

    }
}
