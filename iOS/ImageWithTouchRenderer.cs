using System.Drawing;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using UIKit;
using Foundation;
using Signature;
using Signature.iOS;

[assembly: ExportRenderer(typeof(ImageWithTouch), typeof(ImageWithTouchRenderer))]

namespace Signature.iOS
{
	public class ImageWithTouchRenderer : ViewRenderer<ImageWithTouch, DrawView> 
	{
		protected override void OnElementChanged(ElementChangedEventArgs<ImageWithTouch> e)
		{
			base.OnElementChanged(e);

			SetNativeControl(new DrawView(RectangleF.Empty));
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == ImageWithTouch.ClearImagePathProperty.PropertyName) {
				Control.Clear ();
			} else if (e.PropertyName == ImageWithTouch.SavedImagePathProperty.PropertyName) {
				UIImage curDrawingImage = Control.GetImageFromView ();
				NSData data = curDrawingImage.AsJPEG ();
				NSError error = new NSError ();
				bool bSuccess = data.Save (Element.SavedImagePath, true, out error);
			}
			else {
				UpdateControl(e.PropertyName == ImageWithTouch.CurrentLineColorProperty.PropertyName ||
					e.PropertyName == ImageWithTouch.CurrentImageProperty.PropertyName ||
					e.PropertyName == ImageWithTouch.CurrentLineWidthProperty.PropertyName);
			}
		}

		private void UpdateControl(bool bDisplayFlag)
		{
			Control.CurrentLineColor = Element.CurrentLineColor.ToUIColor();
			Control.PenWidth = Element.CurrentLineWidth;

			if (Control.ImageFilePath != Element.CurrentImagePath) {
				Control.ImageFilePath = Element.CurrentImagePath;
				Control.LoadImageFromFile ();
			}

			if (bDisplayFlag) {
				Control.SetNeedsDisplay ();
			}
		}
	}
		
}

