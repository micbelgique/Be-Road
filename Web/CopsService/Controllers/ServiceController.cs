using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using PublicService.Dal;
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
            ViewBag.FirstAndLastNames = GetFirstAndLastName();
            return View();
        }

        [HttpPost]
        public ActionResult Index(String name)
        {
            throw new NotImplementedException("Search users by name is not yet implemented");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadToAzure()
        {
            /*var json = JsonConvert.SerializeObject(db.MicTrainees.ToList());
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("files");
            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                if (rootDir.Exists())
                {
                    CloudFile file = rootDir.GetFileReference("mic.json");
                    file.UploadTextAsync(json);
                }
            }
            ViewBag.copsdata = json;*/
            return View();
        }


        [HttpPost]
        public ActionResult AddAccessInfo(int? dataId, string reason, string ip)
        {
            var data = db.Datas.Find(dataId);
            if (data == null || String.IsNullOrWhiteSpace(reason))
            {
                return HttpNotFound();
            }

            var name = GetFirstAndLastName();
            data.AccessInfos.Add(new Models.AccessInfo()
            {
                Date = DateTime.Now,
                Name = $"{name};{ip}",
                Reason = reason
            });
            db.SaveChanges();
            return Content(JsonConvert.SerializeObject(data));
        }

        [HttpPost]
        public async Task<ActionResult> Details(string id, int? dataId, string reason, string ip)
        {
            AddAccessInfo(dataId, reason, ip);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstAndLastNames = GetFirstAndLastName();
            return View(user);
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var strings = searchString.Split(' ');
            var foundUsers = new List<ApplicationUser>();
            List<ApplicationUser> all;
            if (strings.Length > 1)
            {
                var firstName = strings[0];
                string concat = "";
                for (int i = 1; i < strings.Length; i++)
                    concat += "." + strings[i];

                all = db.Users.Where(u => u.FirstName.Value == firstName || u.LastName.Value == concat).ToList();

                foreach (ApplicationUser user in all)
                {
                    foundUsers.Add(user);
                }
            }
            else
            {
                var name = strings[0];
                all = db.Users.Where(u => u.FirstName.Value == name || u.LastName.Value == name).ToList();

                foreach (ApplicationUser user in all)
                {
                    foundUsers.Add(user);
                }
            }
            ViewBag.FirstAndLastNames = GetFirstAndLastName();
            return View("Index", foundUsers);
        }

        public String GetFirstAndLastName()
        {
            var connectedUser = UserManager.FindById(User.Identity.GetUserId());
            return connectedUser.FirstName.Value + " " + connectedUser.LastName.Value;
        }
    }
}