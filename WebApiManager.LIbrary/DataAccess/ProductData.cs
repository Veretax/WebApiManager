using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.Library.Internal.DataAccess;
using WebApiManager.Library.Models;

namespace WebApiManager.Library.DataAccess
{
    public class ProductData
    {
        private string _spProductGetAll = "dbo.sp_Product_GetAll";
        private string _defaultConnection = "DefaultConnection";
        private string _managerDataConnection = "WebApiManager.Data";

        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { };

            var output = sql.LoadData<ProductModel, dynamic>(_spProductGetAll, p, _managerDataConnection);

            return output;
        }
    }
}
