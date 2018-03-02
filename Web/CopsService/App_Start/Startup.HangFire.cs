using Hangfire;
using Owin;
using PublicService.Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PublicService
{
    public partial class Startup
    {
        public void ConfigureHangfire(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("PSContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate("UploadToAzureCron",
                () => UploadToAzureCronAsync(),
                Cron.Daily);
        }

        public async Task UploadToAzureCronAsync()
        {
            using (var db = new PSContext())
            {
                var au = new AzureUpload();
                var now = DateTime.Now;
                Debug.WriteLine($"CronStart V4 : {now}, there are currently {db.Users.Count()} users");
                await au.UploadToAzureAsync(db);
                var timeLapse = DateTime.Now - now;
                Debug.WriteLine($"CronEnd V4 : Successfully uploaded after {timeLapse.TotalSeconds} seconds");
            }
        }
    }
}