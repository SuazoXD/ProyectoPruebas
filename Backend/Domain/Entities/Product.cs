using System;

namespace Domain.Entities
{
    public class Product
    {
        // Propiedades de solo lectura para evitar modificaciones no controladas
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        // Constructor para crear un nuevo producto. Se valida la entrada para cumplir con las buenas prácticas.
        public Product(string name, string description, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio", nameof(name));
            if (price < 0)
                throw new ArgumentException("El precio no puede ser negativo", nameof(price));

            Name = name;
            Description = description;
            Price = price;
        }

        // Constructor protegido para EF Core o deserialización.
        protected Product() { }

        // Método de negocio para actualizar el precio, cumpliendo con el SRP.
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("El precio no puede ser negativo", nameof(newPrice));
            Price = newPrice;
        }

        // Se pueden agregar otros métodos de negocio relacionados exclusivamente con el Product.
    }
}
