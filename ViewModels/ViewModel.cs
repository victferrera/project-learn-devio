using AppMercadoBasico.Models;
using System.Net;

namespace AppMercadoBasico.ViewModels
{
    public class ViewModel
    {
        public ViewModel()
        {
            ProductsList = new List<Product>();
            result = new Dictionary<string, dynamic>();
            CustomerList = new List<Customer>();
        }

        public IEnumerable<Product> ProductsList;
        public Product product;
        public Dictionary<string, dynamic> result;
        public Address address;
        public List<Customer> CustomerList;
        public Customer customer;

    }
}
