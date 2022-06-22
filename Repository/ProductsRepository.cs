using AppMercadoBasico.Contracts;
using AppMercadoBasico.Data;
using AppMercadoBasico.Models;
using AppMercadoBasico.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AppMercadoBasico.Repository
{
    public class ProductsRepository : IEntityContract<Product>
    {
        private AppDbContext _context;
        private ViewModel _viewModel;
        public ProductsRepository(AppDbContext context, ViewModel viewModel)
        {
            _context = context;
            _viewModel = viewModel;
        }

        public ViewModel Save(Product model)
        {
            try
            {
                _context.Products.Add(model);
                _context.SaveChanges();

                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.Created);
                _viewModel.result.Add("Message", $"Produto {model.Name} salvo com sucesso!");

                return _viewModel;

            }
            catch (Exception)
            {
                _viewModel.result.Add("StatusCode", HttpStatusCode.BadRequest);
                _viewModel.result.Add("Message", "Não foi possível criar o produto, tente novamente mais tarde ou entre em contato com o administrador");

                return _viewModel;
            }
        }

        public ViewModel Delete(Guid id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);

                if (product != null)
                {
                    try
                    {
                        _context.Products.Remove(product);
                        _context.SaveChanges();

                        _viewModel.result.Clear();
                        _viewModel.result.Add("StatusCode", HttpStatusCode.OK);
                        _viewModel.result.Add("Message", $"Produto {product.Name} excluído com sucesso!");

                        return _viewModel;
                    }catch(Exception)
                    {
                        _viewModel.result.Add("StatusCode", HttpStatusCode.BadRequest);
                        _viewModel.result.Add("Message","Ocorreu um erro ao tentar excluir o produto, tente novamente mais tarde ou acione o administrator");

                        return _viewModel;
                    }
                }
                else
                {
                    _viewModel.result.Add("StatusCode", HttpStatusCode.NoContent);
                    _viewModel.result.Add("Message", "Não foi encontrado nenhuma informação na base com os dados fornecidos");

                    return _viewModel;
                }
            }
            catch (Exception e)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.InternalServerError);
                _viewModel.result.Add("Message", $"Ocorreu um erro desconhecido, contacte o administrador : {e.Message.ToUpper()}");

                return _viewModel;
            }
        }

        public ViewModel GetAll()
        {
            var productsList = _context.Products.ToList();

            if (productsList.Count == 0)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.NoContent);
                _viewModel.result.Add("Message","Nenhuma informação encontrada para apresentar! :(");
                return _viewModel;
            }
            else
            {
                _viewModel.ProductsList = productsList;
                return _viewModel;
            }
        }

        public ViewModel Get(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.NotFound);
                _viewModel.result.Add("Message", "Produto não encontrado");
                return _viewModel;
            }
            else
            {
                _viewModel.product = product;
                return _viewModel;
            }
        }

        public ViewModel Update(Guid id, Product model)
        {
            if (id != model.Id)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.BadRequest);
                _viewModel.result.Add("Message", "Ocorreu um erro inesperado, tente novamente mais tarde ou contate o administrador!");
                return _viewModel;
            }
            else
            {
                var product = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (product is null)
                {
                    _viewModel.result.Clear();
                    _viewModel.result.Add("StatusCode", HttpStatusCode.NotFound);
                    _viewModel.result.Add("Message", "O produto não foi encontrado para edição! :(");
                    return _viewModel;
                }
                else
                {
                    _context.Update(model);
                    _context.SaveChanges();
                    model = null;

                    _viewModel.result.Clear();
                    _viewModel.result.Add("StatusCode", HttpStatusCode.OK);
                    _viewModel.result.Add("Message", $"Informações do produto {product.Name} alteradas com sucesso!");

                    return _viewModel;
                }
            }
        }
    }
}
