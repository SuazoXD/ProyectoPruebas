namespace Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }  // Para el caso de actualizaciones o respuestas
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
