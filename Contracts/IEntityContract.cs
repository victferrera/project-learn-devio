using AppMercadoBasico.ViewModels;

namespace AppMercadoBasico.Contracts
{
    public interface IEntityContract<T> where T : class
    {
        ViewModel Save(T model);
        ViewModel Delete(Guid id);
        ViewModel GetAll();
        ViewModel Get(Guid id);
        ViewModel Update(Guid id, T model);
    }
}
