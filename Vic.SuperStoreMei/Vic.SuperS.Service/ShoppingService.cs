using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vic.SuperS.Data.Model;
using Vic.SuperS.Data.Repository;

namespace Vic.SuperS.Service
{
    public class ShoppingService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository = new MockShoppingCartRepository();
        private readonly IProductRepository _productRepository = new MockProductRepository();
        private readonly IReceiptRepository _receiptRepository = new MockReceiptRepository();

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public ShoppingCart CreateShoppingCart()
        {
            return _shoppingCartRepository.Create();
        }

        public Receipt CreateReceipt(int shoppingCartId)
        {
            return _receiptRepository.Create(shoppingCartId);
        }

        public Product BuyProduct(int shoppingCartId, int productId, int count = 1)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("Cannot remove product less than zero!");
            }

            var cart = _shoppingCartRepository.GetById(shoppingCartId);
            var product = _productRepository.GetById(productId);
            var shoppingItem = cart.ShoppingItems.FirstOrDefault(i => i.ProductId == productId);
       
            if (shoppingItem != null)
            {
                shoppingItem.Count += count;
            }
            else
            {
                var newShoppingItem = new ShoppingItem
                {
                    ProductId = productId,
                    Price = product.Price,
                    Count = count
                };

                cart.ShoppingItems.Add(newShoppingItem);
            }

            return product; 

        }

        public void RemoveProduct(int shoppingCartId, int productId, int count = 1)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("Can not remove product less than zero.");
            }
            var cart = _shoppingCartRepository.GetById(shoppingCartId);
            var product = _productRepository.GetById(productId);
            var shoppingItem = cart.ShoppingItems.FirstOrDefault(i => i.ProductId == productId);

            if (shoppingItem != null)
            {
                shoppingItem.Count -= count;

                if (shoppingItem.Count <= 0)
                {
                    cart.ShoppingItems.Remove(shoppingItem);
                }

            }

        }

        public void PrintReceiptByShoppingCardId (int shoppingCartId)
        {
             _receiptRepository.PrintByShoppingCardId(shoppingCartId);

        }

        public void PrintReceiptByReceiptdId(int shoppingCartId)
        {
            _receiptRepository.PrintByReceiptId(shoppingCartId);

        }

    }

}
