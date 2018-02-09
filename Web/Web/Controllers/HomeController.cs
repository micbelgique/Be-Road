using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["connected"] == null)
                Session["connected"] = false;
            return View();
        }

        public ActionResult LogIn()
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            openid.SecuritySettings.AllowDualPurposeIdentifiers = true;
            var response = openid.GetResponse();
            if (response == null)
            {
                IAuthenticationRequest request = openid.CreateRequest(
                    "https://www.e-contract.be/eid-idp/endpoints/openid/ident"
                    //"https://www.e-contract.be/eid-idp/endpoints/openid/auth"
                    );
                Debug.WriteLine("Sending to EID");

                // attribute query
                FetchRequest fetchRequest = new FetchRequest();
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson");
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson/middle");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/card-validity/end");
                fetchRequest.Attributes.AddRequired("http://axschema.org/person/gender");
                fetchRequest.Attributes.AddRequired("http://axschema.org/contact/postalAddress/home");
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson/first");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/photo");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/card-validity/begin");
                fetchRequest.Attributes.AddRequired("http://axschema.org/contact/city/home");
                fetchRequest.Attributes.AddRequired("http://axschema.org/contact/postalCode/home");
                fetchRequest.Attributes.AddRequired("http://openid.net/schema/birthDate/birthYear");
                fetchRequest.Attributes.AddRequired("http://openid.net/schema/birthDate/birthMonth");
                fetchRequest.Attributes.AddRequired("http://openid.net/schema/birthDate/birthday");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/pob");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/card-number");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/nationality");
                fetchRequest.Attributes.AddRequired("http://axschema.org/namePerson/last");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/rrn");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/cert/auth");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/age");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/documentType");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/nobleCondition");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/card-delivery-municipality");
                fetchRequest.Attributes.AddRequired("http://axschema.org/eid/chip-number");
                request.AddExtension(fetchRequest);

                return request.RedirectingResponse.AsActionResultMvc5();
            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        FetchResponse fetchResponse = response.GetExtension<FetchResponse>();
                        Debug.WriteLine(fetchResponse.Attributes["http://axschema.org/eid/photo"].Values[0]);
                        EidCard eid = new EidCard
                        {
                            ChipNumber = fetchResponse.Attributes["http://axschema.org/eid/chip-number"].Values[0],
                            DocumentType = fetchResponse.Attributes["http://axschema.org/eid/documentType"].Values[0],
                            NobleCondition = fetchResponse.Attributes["http://axschema.org/eid/nobleCondition"].Values[0],
                            CardDeliveryMunicipality = fetchResponse.Attributes["http://axschema.org/eid/card-delivery-municipality"].Values[0],
                            AddressCity = fetchResponse.Attributes["http://axschema.org/contact/city/home"].Values[0],
                            AddressStreet = fetchResponse.Attributes["http://axschema.org/contact/postalAddress/home"].Values[0],
                            AddressPostal = fetchResponse.Attributes["http://axschema.org/contact/postalCode/home"].Values[0],
                            Age = Convert.ToInt32(fetchResponse.Attributes["http://axschema.org/eid/age"].Values[0]),
                            CardNumber = fetchResponse.Attributes["http://axschema.org/eid/card-number"].Values[0],
                            CertificateAuth = fetchResponse.Attributes["http://axschema.org/eid/cert/auth"].Values[0],
                            Nationality = fetchResponse.Attributes["http://axschema.org/eid/nationality"].Values[0],
                            LastName = fetchResponse.Attributes["http://axschema.org/namePerson/last"].Values[0],
                            RNN = fetchResponse.Attributes["http://axschema.org/eid/rrn"].Values[0],
                            //We receive a bad base64 encoded image from e-contract.be
                            //We need to replace "_" to "/" and "-" to "+"
                            Photo = fetchResponse.Attributes["http://axschema.org/eid/photo"].Values[0].Replace('_', '/').Replace("-", "+"),
                            FirstName = fetchResponse.Attributes["http://axschema.org/namePerson/first"].Values[0],
                            MiddleName = fetchResponse.Attributes["http://axschema.org/namePerson/middle"].Values[0],
                            Gender = fetchResponse.Attributes["http://axschema.org/person/gender"].Values[0],
                            POB = fetchResponse.Attributes["http://axschema.org/eid/pob"].Values[0],
                            ValidityEnd = DateTime.Parse(fetchResponse.Attributes["http://axschema.org/eid/card-validity/end"].Values[0]),
                            ValidityBegin = DateTime.Parse(fetchResponse.Attributes["http://axschema.org/eid/card-validity/begin"].Values[0]),
                            BirthDay = new DateTime(
                                Convert.ToInt32(fetchResponse.Attributes["http://openid.net/schema/birthDate/birthYear"].Values[0]),
                                Convert.ToInt32(fetchResponse.Attributes["http://openid.net/schema/birthDate/birthMonth"].Values[0]),
                                Convert.ToInt32(fetchResponse.Attributes["http://openid.net/schema/birthDate/birthday"].Values[0]))
                        };
                        Session["eid"] = eid;
                        return RedirectToAction("Index", "PublicServices");
                    case AuthenticationStatus.Canceled:
                        ViewBag.Extra = Resources.Global.Error_log_in_cancel;
                        break;
                    case AuthenticationStatus.Failed:
                        ViewBag.Extra = Resources.Global.Error_log_in_failed;
                        break;
                }
                return View("Error");
            }
        }
    }
}