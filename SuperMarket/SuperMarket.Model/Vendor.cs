namespace SuperMarket.Model
{
    public class Vendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Vendor(int id, string Name)
        {
            this.Id = id;
            this.Name = Name;
        }
    }
}