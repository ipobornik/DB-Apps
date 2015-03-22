namespace SuperMarket.Model
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Tax { get; set; }
        
        public int VendorId { get; set; }
        
        public int MeasureId { get; set; }

        public Product(int id, string name, decimal tax, int vendorId, int measureId)
        {
            this.Id = id;
            this.Name = name;
            this.Tax = tax;
            this.VendorId = vendorId;
            this.MeasureId = measureId;
        }
    }
}