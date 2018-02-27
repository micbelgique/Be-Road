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
            var users = db.Users.ToList();
            return View(users);
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
            return View();
        }

        [HttpPost]
        public ActionResult Index(String name)
        {
            throw new NotImplementedException("Search users by name is not yet implemented");
        }

        [HttpPost]
        public ActionResult AddAccessInfo(int? dataId, string name, string reason, string ip)
        {
            var data = db.Datas.Find(dataId);
            if (data == null || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(reason))
            {
                return HttpNotFound();
            }

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
        public async Task<ActionResult> Details(string id, int? dataId, string name, string reason, string ip)
        {
            AddAccessInfo(dataId, name, reason, ip);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}