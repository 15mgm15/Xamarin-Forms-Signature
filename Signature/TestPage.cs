using System;

using Xamarin.Forms;

namespace Signature
{
	public class TestPage : ContentPage
	{
		public TestPage()
		{
			ImageWithTouch DrawingImage = new ImageWithTouch
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent,
				CurrentLineColor = Color.Black,
			};

			Button saveImageBtn = new Button
			{
				Text = "Save the Image"
			};

			Button seeTheImageBtn = new Button
			{
				Text = "See the Image",
				IsEnabled = false
			};

			saveImageBtn.Clicked += (sender, e) =>
			{
				string savedFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/temp_" + DateTime.Now.ToString("yyyy_mm_dd_hh_mm_ss") + ".jpg";
				//If this property is set the Image is stored in the folder path.
				DrawingImage.SavedImagePath = savedFileName;
				seeTheImageBtn.IsEnabled = true;
			};

			seeTheImageBtn.Clicked += (sender, e) =>
			{
				Navigation.PushAsync(new SignatureImagePage(DrawingImage.SavedImagePath));
			};

			DrawingImage.CurrentLineWidth = 5;
			DrawingImage.CurrentLineColor = Color.Black;

			Content = new StackLayout
			{
				Children = { DrawingImage, saveImageBtn, seeTheImageBtn }
			};
		}
	}
}


