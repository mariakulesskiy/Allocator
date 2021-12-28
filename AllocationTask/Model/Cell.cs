using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllocationTask.Model
{
    public class Cell
    {
        public int vertical;
        public int horizontal;
        public string productId;
        public int quantity;
        public const int MAXQ_QUANTITY = 10;

        public bool AddProduct(Product product, int quantity)
        {
            foreach (var adapter in product.Adapters)
            {
                if (!adapter.isUsed(product))
                    return false;
            }
            if (string.IsNullOrEmpty(this.productId) ||
                (this.productId.Equals(product.PrductId) && quantity + this.quantity <= MAXQ_QUANTITY))
            {
                this.quantity += quantity;
                return true;
            }

            return false;
        }
    }
}
