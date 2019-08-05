using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.Library.Internal.DataAccess;
using WebApiManager.Library.Models;

namespace WebApiManager.Library.DataAccess
{
    public class UserData
    {
        private string _spuserLookup = "sp_User_Lookup";
        private string _defaultConnection = "DefaultConnection";
        private string _managerDataConnection = "WebApiManager.Data";
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>(_spuserLookup, p, _managerDataConnection);

            return output;
        }
    }
}
