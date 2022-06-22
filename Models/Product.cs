using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMercadoBasico.Models
{
    public class Product : Entity
    {
        public Product()
        {
            CreateAt = DateTime.Now;
        }

        [Required(ErrorMessage = "Campo {0} é obrigatorio!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatorio!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Campo {0} é obrigatorio!")]
        public decimal Price { get; set; }

        [DisplayName("Enable")]
        public bool IsEnable { get; set; }
        public DateTime CreateAt { get; set; }

        /* EF Relation */
        public IEnumerable<Customer> Customers { get; set; }
    }
}
