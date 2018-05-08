using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Azure;
using Microsoft.Owin.Security;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Newtonsoft.Json;
using PublicService.Dal;
using PublicService.Managers;
using PublicService.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PublicService.Controllers
{
    [Authorize]
    public class ServiceController : Controller
    {

        #region Properties
        private PSContext db = new PSContext();

        private ADSCallService acs = new ADSCallService();

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

        public ServiceController()
        {

        }

        public ServiceController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.FirstAndLastNames = GetFirstAndLastNameAsync();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Details(string id, string reason)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await acs.GetUser(id, reason);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstAndLastNames = await GetFirstAndLastNameAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> SearchAsync(string searchString)
        {
            var strings = searchString.Split(' ');
            var users = await acs.GetAllUsers($"Searching for {searchString}");
            var foundUsers = new List<ManageViewModel>();
            List<ManageViewModel> all;
            if (strings.Length > 1)
            {
                var firstName = strings[0];
                string concat = "";
                for (int i = 1; i < strings.Length; i++)
                {
                    if(i != strings.Length-1)
                        concat += strings[i] + " ";
                }
                all = users.Where(u => u.FirstName.ToLower() == firstName.ToLower() || u.LastName.ToLower() == concat.ToLower()).ToList();

                foreach (ManageViewModel user in all)
                {
                    foundUsers.Add(user);
                }
            }
            else
            {
                var name = strings[0];
                all = users.Where(u => u.FirstName == name || u.LastName == name).ToList();

                foreach (ManageViewModel user in all)
                {
                    foundUsers.Add(user);
                }
            }
            ViewBag.FirstAndLastNames = await GetFirstAndLastNameAsync();
            return View("Index", foundUsers);
        }

        public async Task<string> GetFirstAndLastNameAsync()
        {
            var connectedUserMin = UserManager.FindById(User.Identity.GetUserId());
            var connectedUser = await acs.GetUser(connectedUserMin.UserName, "User first and last name display");
            return connectedUser.FirstName + " " + connectedUser.LastName;
        }
    }
}