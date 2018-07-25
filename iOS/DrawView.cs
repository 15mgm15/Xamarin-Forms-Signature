using System;
using UIKit;
using System.Drawing;
using CoreGraphics;
using System.Collections.Generic;
using Foundation;

namespace Signature.iOS
{
	public class DrawView : UIView
	{
		public DrawView (RectangleF frame) : base (frame)
		{
			DrawPath = new CGPath ();
			CurrentLineColor = UIColor.Black;
			PenWidth = 3.0f;
			Lines = new List<VESLine> ();
		}

		PointF PreviousPoint;
		CGPath DrawPath;
		byte IndexCount;
		UIBezierPath CurrentPath;
		List<VESLine> Lines;

		public UIColor CurrentLineColor { get; set; }
		public String ImageFilePath { get; set; }
		public float PenWidth { get; set; }

		UIImage _image;

		public void Clear ()
		{
			Lines.Clear ();
			DrawPath.Dispose ();
			DrawPath = new CGPath ();
			SetNeedsDisplay ();
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			IndexCount++;

			var path = new UIBezierPath
			{
				LineJoinStyle = CGLineJoin.Round,
                		LineCapStyle = CGLineCap.Round,
				LineWidth = PenWidth
			};

			var touch = (UITouch)touches.AnyObject;
			PreviousPoint = (PointF)touch.PreviousLocationInView (this);

			var newPoint = touch.LocationInView (this);
			path.MoveTo (newPoint);

			InvokeOnMainThread (SetNeedsDisplay);

			CurrentPath = path;

			var line = new VESLine
			{
				Path = CurrentPath, 
				Color = CurrentLineColor,
				Index = IndexCount 
			};

			Lines.Add (line);
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			var touch = (UITouch)touches.AnyObject;
			var currentPoint = touch.LocationInView (this);

			if (Math.Abs (currentPoint.X - PreviousPoint.X) >= 4 ||
				Math.Abs (currentPoint.Y - PreviousPoint.Y) >= 4) {

				var newPoint = new PointF((float)(currentPoint.X + PreviousPoint.X) / 2, (float)(currentPoint.Y + PreviousPoint.Y) / 2);

				CurrentPath.AddQuadCurveToPoint (newPoint, PreviousPoint);
				PreviousPoint = (PointF)currentPoint;
			} else {
				CurrentPath.AddLineTo (currentPoint);
			}

			InvokeOnMainThread (SetNeedsDisplay);
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			InvokeOnMainThread (SetNeedsDisplay);
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			InvokeOnMainThread (SetNeedsDisplay);
		}

		public override void Draw(CGRect rect)
		{
			foreach (VESLine p in Lines)
			{
				p.Color.SetStroke();
				p.Path.Stroke();
			}
		}

		public void LoadImageFromFile()
		{
			if (ImageFilePath != null && ImageFilePath != "") {
				_image = ImageHelper.GetRotateImage (ImageFilePath);
			}
		}

		public UIImage GetImageFromView()
		{
			RectangleF rect;
			rect = (RectangleF)Frame;

			UIGraphics.BeginImageContext (rect.Size);

			CGContext context = UIGraphics.GetCurrentContext ();
			if (_image != null)
            {
                context.DrawImage(Frame, _image.CGImage);
            }
				
			Layer.RenderInContext (context);

			UIImage image = UIGraphics.GetImageFromCurrentImageContext();

			UIGraphics.EndImageContext ();

			return image;
		}
	}
}

