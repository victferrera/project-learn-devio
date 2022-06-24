namespace AppMercadoBasico.Models
{
    public class CustomerProduct
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
