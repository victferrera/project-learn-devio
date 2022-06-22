using AppMercadoBasico.Models;
using AppMercadoBasico.Repository;
using AppMercadoBasico.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppMercadoBasico.Controllers
{
    public class ProductsController : Controller
    {
        private ProductsRepository _repo { get; set; }
        private ViewModel _viewModel { get; set; }
        public ProductsController(ProductsRepository repo, ViewModel viewModel)
        {
            _repo = repo;
            _viewModel = viewModel;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            var result = _repo.Save(product);
            _viewModel.result = result.result;
            _viewModel.ProductsList = _repo.GetAll().ProductsList;

            return View("Index", _viewModel);
        }

        [HttpGet]
        public ViewResult GetAll()
        {
            _viewModel.ProductsList = _repo.GetAll().ProductsList;

            return View("Index", _viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = _repo.Delete(id);
            _viewModel.result = result.result;
            _viewModel.ProductsList = _repo.GetAll().ProductsList;

            return View("Index", _viewModel);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            return View(_repo.Get(id).product);
        }

        [HttpPost]
        public IActionResult Update(Guid id, Product product)
        {
            var result = _repo.Update(id, product);
            _viewModel.result = result.result;
            _viewModel.ProductsList = _repo.GetAll().ProductsList;

            return View("Index", _viewModel);
        }
        
    }
}
