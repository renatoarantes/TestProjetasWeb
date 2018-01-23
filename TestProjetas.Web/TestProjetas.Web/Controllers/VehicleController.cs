using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestProjetas.Web.Configuration;
using TestProjetas.Web.Models;
using TestProjetas.Web.Service;

namespace TestProjetas.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IOptions<TestProjetasAPIConfig> _testProjetasAPIConfig;

        public VehicleController(IOptions<TestProjetasAPIConfig> testProjetasAPIConfig)
        {
            _testProjetasAPIConfig = testProjetasAPIConfig;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ret = new VehiculeModel();

            using (var testClient = new TestProjetasAPIService(new MediaTypeWithQualityHeaderValue("application/json")))
            {
                var url = string.Format("{0}{1}/{2}", _testProjetasAPIConfig.Value.Url, _testProjetasAPIConfig.Value.VehicleEndPoint, id);
                ret = Newtonsoft.Json.JsonConvert.DeserializeObject<VehiculeModel>(await testClient.GetAsync(url));
            }

            return View("Index", ret);
        }
    }
}