using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PublicService.Dal;
using System;
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
            var users = db.Users.ToList();
            return View(users);
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
            return Content("Successfully saved");
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

        public String GetFirstAndLastName()
        {
            var connectedUser = UserManager.FindById(User.Identity.GetUserId());
            return connectedUser.FirstName.Value + " " + connectedUser.LastName.Value;
        }
    }
}