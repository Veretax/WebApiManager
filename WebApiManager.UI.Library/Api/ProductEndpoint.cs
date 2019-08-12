using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.WPF.UI.Library.Models;

namespace WebApiManager.WPF.UI.Library.Api
{
    public class ProductEndpoint : IProductEndpoint
    {
        IAPIHelper _apiHelper;

        public ProductEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var results = await response.Content.ReadAsAsync<List<ProductModel>>();

                    return results;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
