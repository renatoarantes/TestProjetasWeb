using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestProjetas.Web.Service
{
    public class TestProjetasAPIService : HttpClient
    {
        #region Properties
        private MediaTypeWithQualityHeaderValue _mediaTypeWithQualityHeaderValue;
        #endregion
        public TestProjetasAPIService(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue)
        {
            _mediaTypeWithQualityHeaderValue = mediaTypeWithQualityHeaderValue;
        }

        public new async Task<string> GetAsync(string url)
        {
            string result = "";
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var _httpClient = new HttpClient(httpClientHandler, false))
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(_mediaTypeWithQualityHeaderValue);

                    var httpResponse = await _httpClient.GetAsync(url);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        result = httpResponse.Content.ReadAsStringAsync().Result;
                    }
                   
                    return result;
                }
            }
        }

        public async Task<string> PostAsync(string url, StringContent content)
        {
            string result = "";
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var _httpClient = new HttpClient(httpClientHandler, false))
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(_mediaTypeWithQualityHeaderValue);
                    
                    var response = await _httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    
                    return result;
                }
            }
        }

        public new async Task<string> PutAsync(string url, StringContent content)
        {
            string result = "";
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var _httpClient = new HttpClient(httpClientHandler, false))
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(_mediaTypeWithQualityHeaderValue);
                    var response = await _httpClient.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }

                    return result;
                }
            }
        }

        public new async Task<string> DeleteAsync(string url)
        {
            string result = "";
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var _httpClient = new HttpClient(httpClientHandler, false))
                {
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(_mediaTypeWithQualityHeaderValue);
                    var response = await _httpClient.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    
                    return result;
                }
            }
        }
    }
}
