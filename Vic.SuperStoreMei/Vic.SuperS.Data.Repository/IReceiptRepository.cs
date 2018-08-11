using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vic.SuperS.Data.Model;

namespace Vic.SuperS.Data.Repository
{
    public interface IReceiptRepository
    {
        Receipt Create(int shoppingCardId);
        Receipt GetById(int id);
        Receipt GetByShoppingCardId(int shoppingCardId);
        List<Receipt> GetAll();
        Receipt Update(int id, Receipt newValue);
        bool DeleteById(int id);
        bool Delete(Receipt item);
        void PrintByReceiptId(int id);
        void PrintByShoppingCardId(int shoppingCardId);
    }
}
