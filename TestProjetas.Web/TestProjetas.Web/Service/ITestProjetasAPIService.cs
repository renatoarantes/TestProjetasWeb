using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestProjetas.Web.Service
{
    public interface ITestProjetasAPIService
    {
        Task<string> GetAsync(string url);

        Task<string> PostAsync(string url, FormUrlEncodedContent content);

        Task<string> PutAsync(string url, HttpContent content);

        Task<string> DeleteAsync(string url);
    }
}
