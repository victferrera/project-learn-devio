using System.ComponentModel.DataAnnotations;

namespace AppMercadoBasico.Models
{
    public class Address : Entity
    {
        public Address()
        {
            Id = Guid.NewGuid();
        }
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }

        /* EF Relation */
        public Guid CustomerId { get; set; }
    }
}
