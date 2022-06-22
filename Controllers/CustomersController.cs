using Microsoft.AspNetCore.Mvc;
using AppMercadoBasico.Models;
using AppMercadoBasico.Repository;
using AppMercadoBasico.ViewModels;

namespace AppMercadoBasico.Controllers
{
    public class CustomersController : Controller
    {
        private CustomersRepository _customersRepository;
        private ViewModel _viewModel;

        public CustomersController(CustomersRepository customersRepository, ViewModel viewModel)
        {
            _customersRepository = customersRepository;
            _viewModel = viewModel;
        }
        [HttpGet]
        public ViewResult Index()
        {
            _viewModel.CustomerList = _customersRepository.GetAll().CustomerList;
            return View(_viewModel);
        }

        [HttpPost]
        public ViewResult Save(Customer customer)
        {
            _viewModel.result = _customersRepository.Save(customer).result;
            _viewModel.CustomerList.Clear();
            _viewModel.CustomerList = _customersRepository.GetAll().CustomerList;

            return View("Index", _viewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Delete(Guid id)
        {
            _viewModel = _customersRepository.Delete(id);
            _viewModel.CustomerList.Clear();
            _viewModel.CustomerList = _customersRepository.GetAll().CustomerList;

            return View("Index", _viewModel);
        }
    }
}
