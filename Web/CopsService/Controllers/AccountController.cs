using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.RelyingParty;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PublicService.Dal;
using PublicService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PublicService.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Properties
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion

        public AccountController()
        {

        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            openid.SecuritySettings.AllowDualPurposeIdentifiers = true;
            var response = openid.GetResponse();
            //First we need to ask the ID card for a user
            if (response == null)
            {
                IAuthenticationRequest request = openid.CreateRequest("https://www.e-contract.be/eid-idp/endpoints/openid/ident");

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
            //This is never happening
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            OpenIdRelyingParty openid = new OpenIdRelyingParty();
            openid.SecuritySettings.AllowDualPurposeIdentifiers = true;
            var response = openid.GetResponse();
            //Get back the response of the open id provider
            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        FetchResponse fetchResponse = response.GetExtension<FetchResponse>();
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
                        //Save the eid in the session
                        Session["eid"] = eid;
                        ViewBag.FirstTry = true;
                        return View(model);
                    case AuthenticationStatus.Canceled:
                        ViewBag.Extra = "Log in cancel";
                        break;
                    case AuthenticationStatus.Failed:
                        ViewBag.Extra = response.Exception.Message;
                        break;
                }
                //TODO: Redirect to some error page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Get back the data of the register form
                if (ModelState.IsValid)
                {
                    EidCard eid = (EidCard)Session["eid"];
                    //TODO: Redirect to some error page
                    if (eid == null)
                        return RedirectToAction("Index", "Home");

                    var user = new ApplicationUser
                    {
                        UserName = model.Username,
                        FirstName = new Data() { Value = eid.FirstName },
                        LastName = new Data() { Value = eid.LastName }
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return await Login(new LoginViewModel() { Username = model.Username, Password = model.Password }, "/Account/Manage");
                    }
                    AddErrors(result);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Manage()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return View(user);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }

}