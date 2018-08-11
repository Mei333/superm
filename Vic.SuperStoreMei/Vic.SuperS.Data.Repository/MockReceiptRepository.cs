using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vic.SuperS.Data.Model;

namespace Vic.SuperS.Data.Repository
{
    public class MockReceiptRepository : IReceiptRepository
    {
        private readonly List<Receipt> _receipt = new List<Receipt>();
        private readonly IShoppingCartRepository _shoppingCartRepository = new MockShoppingCartRepository();


        public Receipt Create(int shoppingCartId)
        {
            int currentMaxId = 0;

            if (_receipt.Any())
            {
                currentMaxId = _receipt.Max(i => i.Id);
            }

            currentMaxId++;

            var _shoppingCart = _shoppingCartRepository.GetById(shoppingCartId);

            var _shoppingItems = _shoppingCart.ShoppingItems;

            decimal _totalPrice = 0;

            foreach (ShoppingItem item in _shoppingItems)
            {
                _totalPrice += item.Count * item.Price;
            }
            var result = new Receipt
            {
                Id = currentMaxId,
                ShoppingCardId = shoppingCartId,
                ShoppingItems = _shoppingItems,
                TotalPrice = _totalPrice
            };

            _receipt.Add(result);

            return result;

        }

        public Receipt GetById(int id)
        {
            return _receipt.First(i => i.Id == id);
        }

        public Receipt GetByShoppingCardId(int shoppingCardId)
        {
            return _receipt.First(i => i.ShoppingCardId == shoppingCardId);
        }

        public Receipt Update(int id, Receipt newValue)
        {
            var old = _receipt.FirstOrDefault(i => i.Id == id);

            if (old != null)
            {
                old.ShoppingCardId = newValue.ShoppingCardId;
                old.ShoppingItems = newValue.ShoppingItems;
                old.TotalPrice = newValue.TotalPrice;
                old.Created = newValue.Created;
            }

            return old;
        }

        public bool DeleteById(int id)
        {
            if (this._receipt.Any(i => i.Id == id))
            {
                this._receipt.RemoveAll(i => i.Id == id);
                return true;
            }

            return false;

        }

        public bool Delete(Receipt item)
        {

            if (item == null)
            {
                return false;
            }

            if (this._receipt.Any(i => i.Id == item.Id))
            {
                this._receipt.RemoveAll(i => i.Id == item.Id);
                return true;
            }

            return false;
        }

        public List<Receipt> GetAll()
        {
            return this._receipt;
        }

        public void PrintByReceiptId(int id)
        {
            if (this._receipt.Any(i => i.Id == id))
            {
                var receipt = _receipt.FirstOrDefault(i => i.Id == id);
                receipt.ToString();
                receipt.ShoppingItems.ForEach(Console.WriteLine);
            }

        }

        public void PrintByShoppingCardId(int shoppingCardId)
        {
            if (this._receipt.Any(i => i.ShoppingCardId == shoppingCardId))
            {
                var receipt = _receipt.FirstOrDefault(i => i.ShoppingCardId == shoppingCardId);
                receipt.ToString();
                receipt.ShoppingItems.ForEach(Console.WriteLine);
            }

        }


    }



}
