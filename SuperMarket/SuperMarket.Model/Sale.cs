using System;
using System.Collections.Generic;
using System.Data;

namespace SuperMarket.Model
{
    public class Sale
    {
        private List<Sale> sales; 

        public int Id { get; set; }

        public DateTime SoldOn { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal Cost { get; set; }

        public int SupermarketId { get; set; }

        public int ProductId { get; set; }

        public Sale(int id, DateTime soldOn, int quantity, decimal pricePerUnit, decimal cost, int supermarketId, int productId)
        {
            this.Id = id;
            this.SoldOn = soldOn;
            this.Quantity = quantity;
            this.PricePerUnit = pricePerUnit;
            this.Cost = cost;
            this.SupermarketId = supermarketId;
            this.ProductId = productId;
        }
    }
}
