using Contracts;
using Contracts.Dal;
using Contracts.Logic;
using Contracts.Models;
using Newtonsoft.Json;
using Proxy.Dal;
using Proxy.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Proxy.Controllers
{
    [RoutePrefix("api/contract")]
    public class ContractController : ApiController
    {
        private ContractManager cm;

        public ContractController()
        {
            cm = new ContractManager()
            {
                AsService = new AdapterServerServiceImpl(),
                BcService = new BeContractServiceImpl(),
                AuthService = new AuthorisationServerServiceImpl()
            };
        }

        [HttpPost]
        [Route("call")]
        public async Task<HttpResponseMessage> CallContractAsync(BeContractCall call)
        {
            try
            {
                var callRes = await cm.CallAsync(call);
                await CallToMLAsync(new { ContractId = call.Id, UserName = call.ISName, UserType = "TEMP", Response = false, call.Inputs }, "api/Contract/Call");
                return Request.CreateResponse(HttpStatusCode.OK, callRes);
            }
            catch(BeContractException ex)
            {
                await CallToMLAsync(new { UserName = call.ISName ?? "Undefined username", UserType = "TEMP", ex.Message}, "api/Exception/Call");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("ads")]
        public async Task<List<String>> GetContractNamesByIsName([FromUri]string isName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(ConfigHelper.GetServiceUrl("centralserver"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/central/contract/ads?isName={isName}");
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsAsync<List<String>>();
                        return res;
                    } 
                    else
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(response.ReasonPhrase),
                            ReasonPhrase = "Error in CentralServer"
                        };
                        throw new HttpResponseException(resp);
                    }
                }
                catch (Exception ex)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(ex.Message),
                        ReasonPhrase = "Error in CentralServer"
                    };
                    throw new HttpResponseException(resp);
                }
            }
        }

        [HttpGet]
        [Route("justification")]
        public async Task<List<dynamic>> GetJustificationAboutContract([FromUri]string contractId, [FromUri]string nrid)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(ConfigHelper.GetServiceUrl("messagelog"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync($"api/contract/justification?contractId={contractId}&nrid={nrid}");
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsAsync<List<dynamic>>();
                        return res;
                    }
                    else
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(response.ReasonPhrase),
                            ReasonPhrase = "Error in MessageLog"
                        };
                        throw new HttpResponseException(resp);
                    }
                }
                catch (Exception ex)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(ex.Message),
                        ReasonPhrase = "Error in MessageLog"
                    };
                    throw new HttpResponseException(resp);
                }
            }
        }

        private async Task CallToMLAsync(dynamic args, string route)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(ConfigHelper.GetServiceUrl("messagelog"));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpContent = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(route, httpContent);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
