using Android.Views;
using Android.Graphics;
using Android.Content;
using System;

namespace Signature.Droid
{
	public class DrawView : View
	{
		public DrawView(Context context)
			: base(context)
		{
			Start();
		}

		public Color CurrentLineColor { get; set; }
		public String ImageFilePath { get; set; }

		public float PenWidth { get; set; }

		private Path DrawPath;
		private Paint DrawPaint;
		private Paint CanvasPaint;
		private Canvas DrawCanvas;
		private Bitmap CanvasBitmap;

		private int w, h;
		private Bitmap _image = null;

		private void Start()
		{
			CurrentLineColor = Color.Black;
			PenWidth = 5.0f;

			DrawPath = new Path();
			DrawPaint = new Paint
			{
				Color = CurrentLineColor,
				AntiAlias = true,
				StrokeWidth = PenWidth
			};

			DrawPaint.SetStyle(Paint.Style.Stroke);
			DrawPaint.StrokeJoin = Paint.Join.Round;
			DrawPaint.StrokeCap = Paint.Cap.Round;

			CanvasPaint = new Paint
			{
				Dither = true
			};
		}

		public void Clear()
		{
			try
			{
				DrawPath = new Path();
				CanvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
				DrawCanvas = new Canvas(CanvasBitmap);
			}
			catch (Exception e)
			{

			}

			Invalidate();
		}

		protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged(w, h, oldw, oldh);
			if (w > 0 && h > 0)
			{
				try
				{
					CanvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
					DrawCanvas = new Canvas(CanvasBitmap);
					this.w = w;
					this.h = h;
				}
				catch(Exception ex)
				{
					
				}
			}
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			DrawPaint.Color = CurrentLineColor;
			DrawPaint.StrokeWidth = PenWidth;
			canvas.DrawBitmap(CanvasBitmap, 0, 0, CanvasPaint);
			canvas.DrawPath(DrawPath, DrawPaint);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			var touchX = e.GetX();
			var touchY = e.GetY();

			switch (e.Action)
			{
			case MotionEventActions.Down:
				DrawPath.MoveTo(touchX, touchY);
				break;
			case MotionEventActions.Move:
				DrawPath.LineTo(touchX, touchY);
				break;
			case MotionEventActions.Up:
				DrawCanvas.DrawPath(DrawPath, DrawPaint);
				DrawPath.Reset();
				break;
			default:
				return false;
			}

			Invalidate();

			return true;
		}

		public void LoadImageFromFile()
		{
			if (ImageFilePath != null && ImageFilePath != "")
			{
				_image = BitmapFactory.DecodeFile(ImageFilePath);
			}
		}

		public Bitmap GetImageFromView()
		{
			Bitmap tempBitmap = null;
			try
			{
				tempBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
				DrawCanvas = new Canvas(tempBitmap);

				if (_image != null)
				{
					DrawPaint.SetStyle(Paint.Style.Fill);
					DrawPaint.Color = Color.White;
					DrawCanvas.DrawRect(new Rect(0, 0, w, h), DrawPaint);

					float scaleX = (float)_image.Width / w;
					float scaleY = (float)_image.Height / h;
					Rect outRect = new Rect();

					int outWidth, outHeight;
					if (scaleX > scaleY)
					{
						outWidth = w;
						outHeight = (int)(_image.Height / scaleX);
					}
					else
					{
						outWidth = (int)(_image.Width / scaleY);
						outHeight = h;
					}

					outRect.Left = w / 2 - outWidth / 2;
					outRect.Top = h / 2 - outHeight / 2;
					outRect.Right = w / 2 + outWidth / 2;
					outRect.Bottom = h / 2 + outHeight / 2;

					DrawCanvas.DrawBitmap(_image, new Rect(0, 0, _image.Width, _image.Height), outRect, DrawPaint);
				}
				else
				{
					DrawPaint.SetStyle(Paint.Style.Fill);
					DrawPaint.Color = Color.White;
					DrawCanvas.DrawRect(new Rect(0, 0, w, h), DrawPaint);
				}

				DrawPaint.Color = CurrentLineColor;
				DrawCanvas.DrawBitmap(CanvasBitmap, 0, 0, CanvasPaint);
				DrawCanvas.DrawPath(DrawPath, DrawPaint);

			}
			catch (Exception ex)
			{
				
			}

			return tempBitmap;
		}
	}
}

