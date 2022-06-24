namespace AppMercadoBasico.Models
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        /* EF Relation */
        public Address Address { get; set; }
        public IEnumerable<CustomerProduct> CustomerProduct { get; set; }
    }
}
