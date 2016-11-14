using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Signature
{
	public partial class TestXamlPage : ContentPage
	{
		public TestXamlPage()
		{
			InitializeComponent();

			var saveImageBtn = this.FindByName<Button>("saveImageBtn");
			var drawingImage = this.FindByName<ImageWithTouch>("drawingImage");
			var seeTheImageBtn = this.FindByName<Button>("seeTheImageBtn");
			var clearTheImageBtn = this.FindByName<Button>("clearTheImageBtn");

			saveImageBtn.Clicked += (sender, e) =>
			{
				string savedFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/temp_" + DateTime.Now.ToString("yyyy_mm_dd_hh_mm_ss") + ".jpg";
				//If this property is set the Image is stored in the folder path.
				drawingImage.SavedImagePath = savedFileName;
				seeTheImageBtn.IsEnabled = true;
			};

			seeTheImageBtn.Clicked += (sender, e) =>
			{
				Navigation.PushAsync(new SignatureImagePage(drawingImage.SavedImagePath));
			};

			clearTheImageBtn.Clicked += (sender, e) =>
			{
				drawingImage.ClearPath = !drawingImage.ClearPath;
			};
		}
	}
}
