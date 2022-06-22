using AppMercadoBasico.Contracts;
using AppMercadoBasico.Data;
using AppMercadoBasico.Models;
using AppMercadoBasico.ViewModels;
using System.Net;

namespace AppMercadoBasico.Repository
{
    public class CustomersRepository : IEntityContract<Customer>
    {
        private AppDbContext _context;
        private ViewModel _viewModel;

        public CustomersRepository(AppDbContext context, ViewModel viewModel)
        {
            _context = context;
            _viewModel = viewModel;
        }

        public ViewModel Delete(Guid id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

                if (customer != null)
                {
                    try
                    {
                        _context.Customers.Remove(customer);
                        _context.SaveChanges();

                        
                        _viewModel.result.Add("StatusCode", HttpStatusCode.Created);
                        _viewModel.result.Add("Message", $"Cliente {customer.Name} excluído com sucesso!");

                        return _viewModel;
                    }
                    catch (Exception)
                    {
                        _viewModel.result.Clear();
                        _viewModel.result.Add("StatusCode", HttpStatusCode.BadRequest);
                        _viewModel.result.Add("Message", "Ocorreu um erro ao tentar excluir o cliente, tente novamente mais tarde ou acione o administrator");

                        return _viewModel;
                    }
                }
                else
                {
                    _viewModel.result.Clear();
                    _viewModel.result.Add("StatusCode", HttpStatusCode.NoContent);
                    _viewModel.result.Add("Message", $"Não foi encontrado nenhum cliente na base com os dados fornecidos");

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

        public ViewModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ViewModel GetAll()
        {
            var customerList = _context.Customers.ToList();

            if (customerList.Count == 0)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.NoContent);
                _viewModel.result.Add("Message", "Nenhuma informação encontrada para apresentar! :(");

                return _viewModel;
            }
            else
            {
                _viewModel.CustomerList = customerList;
                return _viewModel;
            }
        }

        public ViewModel Save(Customer model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();

                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.Created);
                _viewModel.result.Add("Message", "Cliente criado com sucesso!");

                return _viewModel;
                
            }catch(Exception e)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.BadRequest);
                _viewModel.result.Add("Message", $"Ocorreu um erro ao salvar o cliente: {e.Message.ToUpper()}");

                return _viewModel;
            }
        }

        public ViewModel Update(Guid id, Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
