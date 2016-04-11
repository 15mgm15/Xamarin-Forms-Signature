using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
#if __IOS__
using Foundation;
#endif

#if __ANDROID__
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.Provider;
#elif __IOS__
using UIKit;
using CoreGraphics;
#endif

namespace Signature
{
	public class ImageHelper
	{
		#if __ANDROID__
		public static byte[] BitmapToBytes(Android.Graphics.Bitmap bitmap)
		{
		byte[] data = new byte[0];
		using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
		{
		bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 90, stream);
		stream.Close();
		data = stream.ToArray();
		}
		return data;
		}

		public static int CalculateInSampleSize(Android.Graphics.BitmapFactory.Options options, int reqWidth, int reqHeight)
		{
		// Raw height and width of image
		var height = (float)options.OutHeight;
		var width = (float)options.OutWidth;
		var inSampleSize = 1D;

		if (height > reqHeight || width > reqWidth)
		{
		inSampleSize = width > height
		? height / reqHeight
		: width / reqWidth;
		}

		return (int)inSampleSize;
		}

		public static Android.Graphics.Bitmap DecodeSampledBitmapFromResource(String filename, int reqWidth, int reqHeight)
		{
		// First decode with inJustDecodeBounds=true to check dimensions
		var options = new Android.Graphics.BitmapFactory.Options
		{
		InJustDecodeBounds = true,
		};
		using (var dispose = Android.Graphics.BitmapFactory.DecodeFile(filename, options))
		{
		}

		// Calculate inSampleSize
		options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

		// Decode bitmap with inSampleSize set
		options.InJustDecodeBounds = false;
		return Android.Graphics.BitmapFactory.DecodeFile(filename, options);
		}

		/**
		* Rotate an image if required.
		* @param img
		* @param selectedImage
		* @return 
		*/
		public static Bitmap rotateImageIfRequired(Android.Content.Context context, Bitmap img, Uri selectedImage)
		{

		// Detect rotation
		int rotation = getRotation(context, selectedImage);
		if (rotation != 0)
		{
		Matrix matrix = new Matrix();
		matrix.PostRotate(rotation);
		Bitmap rotatedImg = Bitmap.CreateBitmap(img, 0, 0, img.Width, img.Height, matrix, true);
		img.Recycle();
		return rotatedImg;
		}
		else
		{
		return img;
		}
		}

		/**
		* Get the rotation of the last image added.
		* @param context
		* @param selectedImage
		* @return
		*/
		private static int getRotation(Android.Content.Context context, Uri selectedImage)
		{
		int rotation = 0;
		ContentResolver content = context.ContentResolver;


		ICursor mediaCursor = content.Query(MediaStore.Images.Media.ExternalContentUri,
		new String[] { "orientation", "date_added" }, null, null, "date_added desc");

		if (mediaCursor != null && mediaCursor.Count != 0)
		{
		while (mediaCursor.MoveToNext())
		{
		rotation = mediaCursor.GetInt(0);
		break;
		}
		}
		mediaCursor.Close();
		return rotation;
		}

		#elif __IOS__

		public static UIImage GetRotateImage(String imagePath)
		{
			UIImage image = UIImage.FromFile (imagePath);
			CGImage imgRef = image.CGImage;

			float width = imgRef.Width;
			float height = imgRef.Height;

			CGAffineTransform transform = CGAffineTransform.MakeIdentity ();
			RectangleF bounds = new RectangleF(0, 0, width, height);
			SizeF imageSize = new SizeF(width, height);
			float boundHeight;
			UIImageOrientation orient = image.Orientation;
			switch(orient) {

			case UIImageOrientation.Up:
				transform = CGAffineTransform.MakeIdentity ();
				break;

			case UIImageOrientation.UpMirrored:
				transform = CGAffineTransform.MakeTranslation(imageSize.Width, 0.0f);
				transform.Scale(-1.0f, 1.0f);
				break;

			case UIImageOrientation.Down:
				transform.Rotate ((float)Math.PI);
				transform.Translate (imageSize.Width, imageSize.Height);
				break;

			case UIImageOrientation.DownMirrored:
				transform = CGAffineTransform.MakeTranslation(0.0f, imageSize.Height);
				transform.Scale(1.0f, -1.0f);
				break;

			case UIImageOrientation.LeftMirrored:
				boundHeight = bounds.Size.Height;
				bounds.Height = bounds.Size.Width;
				bounds.Width = boundHeight;
				transform.Scale(-1.0f, 1.0f);
				transform.Rotate((float)Math.PI / 2.0f);
				break;

			case UIImageOrientation.Left:
				boundHeight = bounds.Size.Height;
				bounds.Height = bounds.Size.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeRotation ((float)Math.PI / 2.0f);
				transform.Translate(imageSize.Height, 0.0f);
				break;

			case UIImageOrientation.RightMirrored:
				boundHeight = bounds.Size.Height;
				bounds.Height = bounds.Size.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeTranslation(imageSize.Height, imageSize.Width);
				transform.Scale(-1.0f, 1.0f);
				transform.Rotate(3.0f * (float)Math.PI / 2.0f);
				break;

			case UIImageOrientation.Right:
				boundHeight = bounds.Size.Height;
				bounds.Height = bounds.Size.Width;
				bounds.Width = boundHeight;
				transform = CGAffineTransform.MakeRotation (-(float)Math.PI / 2.0f);
				transform.Translate(0.0f, imageSize.Width);
				break;

			default:
				break;

			}

			UIGraphics.BeginImageContext (bounds.Size);

			CGContext context = UIGraphics.GetCurrentContext();

			context.ConcatCTM (transform);

			context = UIGraphics.GetCurrentContext ();
			context.DrawImage (new RectangleF (0, 0, width, height), imgRef);
			UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext();

			return imageCopy;
		}

		public static bool SaveRotateImage(String imagePath)
		{
			UIImage imageCopy = GetRotateImage (imagePath);

			NSData data = imageCopy.AsJPEG ();
			NSError error = new NSError ();
			bool bSuccess = data.Save (imagePath, true, out error);

			return bSuccess;
		}

		#endif
	
	}
}

