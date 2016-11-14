using System;

using Xamarin.Forms;

namespace Signature
{
	public class TestPage : ContentPage
	{
		public TestPage()
		{
			ImageWithTouch drawingImage = new ImageWithTouch
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

			Button clearTheImageBtn = new Button
			{
				Text = "Clear",
			};

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

			drawingImage.CurrentLineWidth = 5;
			drawingImage.CurrentLineColor = Color.Black;

			Content = new StackLayout
			{
				Children = { drawingImage, saveImageBtn, seeTheImageBtn, clearTheImageBtn }
			};
		}
	}
}


