using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // La inyección del repositorio permite delegar la persistencia, siguiendo el principio de inversión de dependencias (SOLID)
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            });
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            // Mapeo del DTO a la entidad del dominio
            var product = new Product(productDto.Name, productDto.Description, productDto.Price);
            var created = await _productRepository.CreateAsync(product);
            return new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price
            };
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            // Se actualiza la entidad. 
            // Por simplicidad, actualizamos solo el precio mediante el método de negocio.
            // En un caso real, se deberían incluir métodos para actualizar otros atributos o recrear la entidad de forma controlada.
            product.UpdatePrice(productDto.Price);
            // Aquí también podrías actualizar Name y Description mediante métodos específicos del dominio si los defines.
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }
    }
}
