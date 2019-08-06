using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.WPF.UI.Models;

namespace WebApiManager.WPF.UI.Helpers
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; set; }

        Task<AuthenticatedUser> Authenticate(string userName, string password);
    }
}
