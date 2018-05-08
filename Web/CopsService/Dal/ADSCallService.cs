using Contracts.Models;
using Newtonsoft.Json;
using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PublicService.Dal
{
    public class ADSCallService
    {
        PSContext db = new PSContext();

        public async Task<List<ManageViewModel>> GetAllUsers(string justification)
        {
            var users = new List<ManageViewModel>();
            foreach(var user in db.Users.ToList())
            {
                users.Add(await GetUser(user.UserName, justification));
            }

            return users;
        }

        public async Task<ManageViewModel> GetUser(string nrid, string justification)
        {
            var userPop = await GetUserPop(nrid, justification);
            var userFunny = await GetUserFunny(nrid, justification);
            var user = new ManageViewModel()
            {
                UserName = nrid,
                FirstName = userPop.FirstName,
                LastName = userPop.LastName,
                Locality = userPop.Locality,
                Nationality = userPop.Nationality,
                BirthDateD = userPop.BirthDateD,
                BirthDateM = userPop.BirthDateM,
                BirthDateY = userPop.BirthDateY,
                ExtraInfo = userFunny.ExtraInfo,
                EmailAddress = userFunny.EmailAddress,
                PhotoUrl = db.Users.FirstOrDefault(u => u.UserName.Equals(nrid)).PhotoUrl
            };

            return user;
        }

        public async Task<CarViewModel> GetCar(string nrid, string justification)
        {
            using (var client = new HttpClient())
            {
                var car = new CarViewModel();

                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var call = new BeContractCall
                    {
                        Id = "GetDivContract",
                        ISName = $"Public Service/{nrid}",
                        Inputs = new Dictionary<string, dynamic>()
                        {
                            { "NRID", nrid },
                            { "Justification", justification}
                        }
                    };

                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                        var outputs = resp.Values.Select(ret => ret.Outputs).ToList();
                        var carValues = outputs.SelectMany(d => d)
                                            .ToDictionary(t => t.Key, t => t.Value);
                        car.Owner = db.Users.FirstOrDefault(u => u.UserName.Equals(nrid));
                        car.Brand = carValues["Brand"];
                        car.NumberPlate = carValues["NumberPlate"];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return car;
            }
        }

        private async Task<ManageViewModel> GetUserPop(string nrid, string justification)
        {
            using (var client = new HttpClient())
            {
                var user = new ManageViewModel();

                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var call = new BeContractCall
                    {
                        Id = "GetPopulationContract",
                        ISName = $"Public Service/{nrid}",
                        Inputs = new Dictionary<string, dynamic>()
                        {
                            { "NRID", nrid },
                            { "Justification", justification}
                        }
                    };

                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                        var outputs = resp.Values.Select(ret => ret.Outputs).ToList();
                        var userValues = outputs.SelectMany(d => d)
                                            .ToDictionary(t => t.Key, t => t.Value);
                        user.FirstName = userValues["FirstName"];
                        user.LastName = userValues["LastName"];
                        user.Locality = userValues["Locality"];
                        user.Nationality = userValues["Nationality"];
                        var birthDate = userValues["Birthday"].Split(' ');
                        user.BirthDateD = birthDate[0];
                        user.BirthDateM = birthDate[1];
                        user.BirthDateY = birthDate[2];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return user;
            }
        }

        private async Task<ManageViewModel> GetUserFunny(string nrid, string justification)
        {
            using (var client = new HttpClient())
            {
                var user = new ManageViewModel();

                try
                {
                    client.BaseAddress = new Uri("http://proxy/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var call = new BeContractCall
                    {
                        Id = "GetFunnyContract",
                        ISName = $"Public Service/{nrid}",
                        Inputs = new Dictionary<string, dynamic>()
                        {
                            { "NRID", nrid },
                            { "Justification", justification}
                        }
                    };

                    var httpContent = new StringContent(JsonConvert.SerializeObject(call), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/contract/call", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = await response.Content.ReadAsAsync<Dictionary<int, BeContractReturn>>();
                        var outputs = resp.Values.Select(ret => ret.Outputs).ToList();
                        var userValues = outputs.SelectMany(d => d)
                                            .ToDictionary(t => t.Key, t => t.Value);
                        user.ExtraInfo = userValues["ExtraInfo"];
                        user.EmailAddress = userValues["Email"];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return user;
            }
        }
    }
}