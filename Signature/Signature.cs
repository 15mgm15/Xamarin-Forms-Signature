using System;

using Xamarin.Forms;

namespace Signature
{
	public class App : Application
	{
		public App ()
		{
			//If you want to use the code behind test page
			//MainPage = new NavigationPage(new TestPage());

			//If you want to use the XAML test page
			MainPage = new NavigationPage(new TestXamlPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

