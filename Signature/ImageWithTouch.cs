using System;
using Xamarin.Forms;

namespace Signature
{
	public class ImageWithTouch : Image 
	{
		public static readonly BindableProperty CurrentLineColorProperty = 
			BindableProperty.Create ((ImageWithTouch w) => w.CurrentLineColor, Color.Default);

		public static readonly BindableProperty CurrentLineWidthProperty = 
			BindableProperty.Create ((ImageWithTouch w) => w.CurrentLineWidth, 1);

		public static readonly BindableProperty CurrentImageProperty = 
			BindableProperty.Create ((ImageWithTouch w) => w.CurrentImagePath, "");

		public static readonly BindableProperty ClearImagePathProperty = 
			BindableProperty.Create ((ImageWithTouch w) => w.ClearPath, false);

		public static readonly BindableProperty SavedImagePathProperty = 
			BindableProperty.Create ((ImageWithTouch w) => w.SavedImagePath, "");

		public Color CurrentLineColor {
			get {
				return (Color)GetValue (CurrentLineColorProperty);
			}
			set {
				SetValue (CurrentLineColorProperty, value);
			}
		}

		public int CurrentLineWidth {
			get {
				return (int)GetValue (CurrentLineWidthProperty);
			}
			set {
				SetValue (CurrentLineWidthProperty, value);
			}
		}

		public string CurrentImagePath {
			get {
				return (string)GetValue (CurrentImageProperty);
			}
			set {
				SetValue (CurrentImageProperty, value);
			}
		}

		public bool ClearPath {
			get {
				return (bool)GetValue (ClearImagePathProperty);
			}
			set {
				SetValue (ClearImagePathProperty, value);
			}
		}

		public string SavedImagePath {
			get {
				return (string)GetValue (SavedImagePathProperty);
			}
			set {
				SetValue (SavedImagePathProperty, value);
			}
		}
	}


}

