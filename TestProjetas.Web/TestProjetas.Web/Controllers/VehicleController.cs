using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            var ret = new VehicleModel();

            using (var testClient = new TestProjetasAPIService(new MediaTypeWithQualityHeaderValue("application/json")))
            {
                var url = string.Format("{0}{1}/{2}", _testProjetasAPIConfig.Value.Url, _testProjetasAPIConfig.Value.VehicleEndPoint, id);
                ret = Newtonsoft.Json.JsonConvert.DeserializeObject<VehicleModel>(await testClient.GetAsync(url));
            }

            return View("Index", ret);
        }

        [HttpPost]
        public async Task<RedirectResult> Save(VehicleModel _vehicle)
        {
            if (_vehicle.Id > 0)
            {
                // Edição
                _vehicle.UpdateDate = DateTime.Now;
                await EditVehicle(_vehicle);
            }
            else
            {
                // Cadastro
                _vehicle.RegistrationDate = DateTime.Now;
                await RegisterVehicle(_vehicle);
            }

            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public async Task<RedirectResult> Delete(int id)
        {
            using (var testClient = new TestProjetasAPIService(new MediaTypeWithQualityHeaderValue("application/json")))
            {
                var url = string.Format("{0}{1}/{2}", _testProjetasAPIConfig.Value.Url, _testProjetasAPIConfig.Value.VehicleEndPoint, id);
                await testClient.DeleteAsync(url);
            }

            return Redirect("~/Home/Index");
        }

        private async Task EditVehicle(object model)
        {
            using (var testClient = new TestProjetasAPIService(new MediaTypeWithQualityHeaderValue("application/json")))
            {
                var url = string.Format("{0}{1}", _testProjetasAPIConfig.Value.Url, _testProjetasAPIConfig.Value.VehicleEndPoint);
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await testClient.PutAsync(url, content);
            }
        }

        private async Task RegisterVehicle(object model)
        {
            using (var testClient = new TestProjetasAPIService(new MediaTypeWithQualityHeaderValue("application/json")))
            {
                var url = string.Format("{0}{1}", _testProjetasAPIConfig.Value.Url, _testProjetasAPIConfig.Value.VehicleEndPoint);
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await testClient.PostAsync(url, content);
            }
        }
    }
}