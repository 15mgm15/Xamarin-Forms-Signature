using System;
using Xamarin.Forms;

namespace Signature
{
	public class SignaturePage  : ContentPage
	{
		private ImageWithTouch DrawingImage;


		public SignaturePage()
		{
			StackLayout stack = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = 0,
				Spacing = 0
			};

			Grid gridDrawing = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition {
						Height = new GridLength (7, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (3, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (25, GridUnitType.Star)
					},
					new RowDefinition {
						Height = new GridLength (1.5, GridUnitType.Star)
					}
				},
				ColumnDefinitions = {
					new ColumnDefinition {
						Width = new GridLength (0.07, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (1, GridUnitType.Star)
					},
					new ColumnDefinition {
						Width = new GridLength (0.07, GridUnitType.Star)
					},
				},
				Children =
				{
					{new ContentView {
							Padding = 0,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.CenterAndExpand
						}, 1, 0},
					{new ContentView {
							Padding = 0,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.CenterAndExpand
						}, 1, 1},
					{new ContentView {
							Content = BuildDrawingFrame(),
							Padding = 0,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.FillAndExpand,
						}, 1, 2},
					{new ContentView {
							HorizontalOptions = LayoutOptions.Start,
							VerticalOptions = LayoutOptions.Start,
							Padding = new Thickness(10, 35, 0, 0)
						}, 1, 2},
					{new ContentView {
							HorizontalOptions = LayoutOptions.End,
							VerticalOptions = LayoutOptions.Start,
							Padding = new Thickness(0, 35, 10, 0)
						}, 1, 2},
				}
				};

			Image imgPhoto = new Image 
			{
				Source = "bg_signature_portrait.png", 
				Aspect = Aspect.Fill,
			};

			var absoluteLayout = new AbsoluteLayout();

			absoluteLayout.Children.Add(imgPhoto, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
			absoluteLayout.Children.Add(gridDrawing, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
			absoluteLayout.VerticalOptions = LayoutOptions.FillAndExpand;
			absoluteLayout.HorizontalOptions = LayoutOptions.FillAndExpand;

			StackLayout mainStack = new StackLayout { 
				Children = { absoluteLayout }, 
				Orientation = StackOrientation.Vertical, 
				VerticalOptions = LayoutOptions.FillAndExpand 
			};
			stack.Children.Add(mainStack);

			Content = stack;

			DrawingImage.CurrentLineWidth = 5;
			DrawingImage.CurrentLineColor = Color.Black;
		}

		private View BuildDrawingFrame()
		{
			DrawingImage = new ImageWithTouch
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Transparent,
				CurrentLineColor = Color.Black,
			};

			return DrawingImage;
		}

	}

}

