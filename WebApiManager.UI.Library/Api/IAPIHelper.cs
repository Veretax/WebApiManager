using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.WPF.UI.Library.Models;

namespace WebApiManager.WPF.UI.Library.Api
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<AuthenticatedUser> Authenticate(string userName, string password);
        Task GetLoggedInUserInfo(string token);
    }
}
