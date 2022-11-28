using KRISTINA_VEZBA.Models;
using KRISTINA_VEZBA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KRISTINA_VEZBA.Controllers
{
    public class ShoppingCartController: Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pieId, int amount)
        { 
            var selectedPie = _pieRepository.GetPieById(pieId);

            if (selectedPie != null)
            {
                _shoppingCart.AddToCart(selectedPie, amount);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pieId)
        {
            var selectedPie = _pieRepository.GetPieById(pieId);

            if (selectedPie != null)
            {
                _shoppingCart.RemoveFromCart(selectedPie);
            }

            return RedirectToAction("Index");
        }

    }
}
