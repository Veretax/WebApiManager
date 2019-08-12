using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiManager.Library.DataAccess;
using WebApiManager.Library.Models;

namespace WebApiManager.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        // GET: api/Product
        [HttpGet]
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }

    }
}
