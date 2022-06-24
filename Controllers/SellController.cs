using AppMercadoBasico.Repository;
using AppMercadoBasico.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppMercadoBasico.Controllers
{
    public class SellController : Controller
    {
        private SellRepository _repository;
        private ViewModel _viewModel;
        public SellController(SellRepository repository, ViewModel viewModel)
        {
            _repository = repository;
            _viewModel = viewModel;
        }
        public IActionResult Index()
        {
            _viewModel.CustomerProductsList = _repository.GetAll().CustomerProductsList;

            return View(_viewModel);
        }
    }
}
