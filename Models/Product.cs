using System;

namespace Models
{
    public class Product
    {
        public Product()
        {

        }

        public Product(string name, int sales)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("No name provided");
            }
            Name = name;
            Sales = sales;
        }

        public Product(int id, string name, int sales)
        {
            Id = id;
            Name = name;
            Sales = sales;
        }

        public int Id { get; }
        public string Name { get; }
        public int Sales { get; }
    }
}
