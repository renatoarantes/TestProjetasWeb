using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class HomeController : Controller
    {
        private readonly IOptions<TestProjetasAPIConfig> _testProjetasAPIConfig;

        public HomeController(IOptions<TestProjetasAPIConfig> testProjetasAPIConfig)
        {
            _testProjetasAPIConfig = testProjetasAPIConfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                var start = Request.Form["start"].FirstOrDefault();

                var length = Request.Form["length"].FirstOrDefault();

                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                using (var testClient = new TestProjetasAPIService(new MediaTypeWithQualityHeaderValue("application/json")))
                {
                    var url = string.Format("{0}{1}", _testProjetasAPIConfig.Value.Url, _testProjetasAPIConfig.Value.VehicleEndPoint);
                    var vehicleData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<VehicleModel>>(await testClient.GetAsync(url));

                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    {
                        switch (sortColumn)
                        {
                            case "Brand":
                                if (sortColumnDirection == "asc")
                                {
                                    vehicleData = vehicleData.OrderBy(x => x.Brand).ToList();
                                }
                                else
                                {
                                    vehicleData = vehicleData.OrderByDescending(x => x.Brand).ToList();
                                }
                                break;
                            case "Model":
                                if (sortColumnDirection == "asc")
                                {
                                    vehicleData = vehicleData.OrderBy(x => x.Model).ToList();
                                }
                                else
                                {
                                    vehicleData = vehicleData.OrderByDescending(x => x.Model).ToList();
                                }
                                break;
                            case "Color":
                                if (sortColumnDirection == "asc")
                                {
                                    vehicleData = vehicleData.OrderBy(x => x.Color).ToList();
                                }
                                else
                                {
                                    vehicleData = vehicleData.OrderByDescending(x => x.Color).ToList();
                                }
                                break;
                            case "Price":
                                if (sortColumnDirection == "asc")
                                {
                                    vehicleData = vehicleData.OrderBy(x => x.Price).ToList();
                                }
                                else
                                {
                                    vehicleData = vehicleData.OrderByDescending(x => x.Price).ToList();
                                }
                                break;
                            case "IsZero":
                                if (sortColumnDirection == "asc")
                                {
                                    vehicleData = vehicleData.OrderBy(x => x.IsZero).ToList();
                                }
                                else
                                {
                                    vehicleData = vehicleData.OrderByDescending(x => x.IsZero).ToList();
                                }
                                break;
                            case "RegistrationDate":
                                if (sortColumnDirection == "asc")
                                {
                                    vehicleData = vehicleData.OrderBy(x => x.RegistrationDate).ToList();
                                }
                                else
                                {
                                    vehicleData = vehicleData.OrderByDescending(x => x.RegistrationDate).ToList();
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        vehicleData = vehicleData.Where(m => m.Brand.ToUpper().Contains(searchValue.ToUpper()) || m.Color.ToUpper().ToString().Contains(searchValue.ToUpper()) ||
                                                                   m.Year.ToString().Contains(searchValue) || m.Model.ToUpper().Contains(searchValue.ToUpper())).ToList();
                    }

                    recordsTotal = vehicleData.Count();

                    var data = vehicleData.Skip(skip).Take(pageSize).ToList();

                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
