using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiManager.WPF.UI.Library.Models;

namespace WebApiManager.WPF.UI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}