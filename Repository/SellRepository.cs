using AppMercadoBasico.Contracts;
using AppMercadoBasico.Data;
using AppMercadoBasico.Models;
using AppMercadoBasico.ViewModels;
using System.Net;

namespace AppMercadoBasico.Repository
{
    public class SellRepository : IEntityContract<CustomerProduct>
    {
        private ViewModel _viewModel;
        private AppDbContext _context;
        public SellRepository(ViewModel viewModel, AppDbContext context)
        {
            _viewModel = viewModel;
            _context = context;
        }
        public ViewModel Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ViewModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ViewModel GetAll()
        {
            try
            {
                _viewModel.CustomerProductsList = _context.CustomersProducts.ToList();

                if(_viewModel.CustomerProductsList.Count == 0)
                {
                    _viewModel.result.Add("StatusCode", HttpStatusCode.NoContent);
                    _viewModel.result.Add("Message", "Nenhuma informação encontrada na base de dados!");
                    return _viewModel;
                }else
                {
                    return _viewModel;
                }
            }catch(Exception e)
            {
                _viewModel.result.Add("StatusCode", HttpStatusCode.InternalServerError);
                _viewModel.result.Add("Message", e.Message);
                return _viewModel;
            }
        }

        public ViewModel Save(CustomerProduct model)
        {
            throw new NotImplementedException();
        }

        public ViewModel Update(Guid id, CustomerProduct model)
        {
            throw new NotImplementedException();
        }
    }
}
