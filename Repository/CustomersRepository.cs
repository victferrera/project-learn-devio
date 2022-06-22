using AppMercadoBasico.Contracts;
using AppMercadoBasico.Data;
using AppMercadoBasico.Models;
using AppMercadoBasico.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            var customer = _context.Customers.Include(c => c.Address).FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {
                _viewModel.customer = customer;

                return _viewModel;
            }
            else
            {
                _viewModel.result.Add("StatusCode", HttpStatusCode.NoContent);
                _viewModel.result.Add("Message", $"Nenhuma cliente encontrado!");

                return _viewModel;
            }
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

            }
            catch (Exception e)
            {
                _viewModel.result.Clear();
                _viewModel.result.Add("StatusCode", HttpStatusCode.BadRequest);
                _viewModel.result.Add("Message", $"Ocorreu um erro ao salvar o cliente: {e.Message.ToUpper()}");

                return _viewModel;
            }
        }

        public ViewModel Update(Guid id, Customer model)
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
                var customer = _context.Customers.AsNoTracking().FirstOrDefault(x => x.Id == id);

                if (customer is null)
                {
                    _viewModel.result.Clear();
                    _viewModel.result.Add("StatusCode", HttpStatusCode.NotFound);
                    _viewModel.result.Add("Message", "O cliente não foi encontrado para edição! :(");
                    return _viewModel;
                }
                else
                {
                    _context.Update(model);
                    _context.SaveChanges();
                    model = null;

                    _viewModel.result.Clear();
                    _viewModel.result.Add("StatusCode", HttpStatusCode.OK);
                    _viewModel.result.Add("Message", $"Informações do cliente {customer.Name} alteradas com sucesso!");

                    return _viewModel;
                }
            }
        }
    }
}
