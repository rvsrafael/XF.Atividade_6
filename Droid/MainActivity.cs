using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static FiapAtividade6.App;
using System.Threading.Tasks;

namespace FiapAtividade6.Droid
{
	[Activity (Label = "FiapAtividade6.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate	
    {
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

			// Initialize Xamarin Forms
			Forms.Init (this, bundle);

            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);

			// Load the main application
			LoadApplication (new App ());
		}


        // Define a authenticated user.
        private MobileServiceUser user;

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
                MobileServiceAuthenticationProvider.Facebook, "fiapatividade6");
                if (user != null)
                {
                    message = string.Format("Login efetuado {0}.", user.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Login com Facebook");
            builder.Create().Show();

            return success;
        }
	}
}

