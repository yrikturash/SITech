using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin.Security.Providers.LinkedIn;
using Owin;
using System;
using SITech.App_Start;
using SITech.Models;

namespace SITech
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                 Provider = new CookieAuthenticationProvider
            {
                OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                    validateInterval: TimeSpan.FromMinutes(30),
                    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "0000000040158923",
            //    clientSecret: "sE6QRiyGuABjiFeIi69LfkTiF6gk4KY6");

            //app.UseTwitterAuthentication(
            // consumerKey: "JVZ6x92YPvMaByJUhP0nPCTJh",
            // consumerSecret: "mUDt7mxw98BldcZl2ZwxQbXE8BLSMuNEl63KfM21CzmQQyB2rl");

            app.UseFacebookAuthentication(
               appId: "943974702327036",
               appSecret: "0866a4f56a3caa7d214aecd569ade207");

            //app.UseGoogleAuthentication(
            //  clientId: "529516396609-q4alm6p1u7d0ih2hp1thpbfojoi3js3m.apps.googleusercontent.com",
            //  clientSecret: "8hxAAOqOtQOOC8BvOKV81gne");

            //app.UseLinkedInAuthentication(
            //   clientId: "77mzc5mnyissi7",
            //   clientSecret: "CLSu6XeJRKUEC03y");
        }
    }
}