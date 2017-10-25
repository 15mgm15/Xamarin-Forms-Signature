using Xamarin.Forms;

namespace Signature
{
	public class SignatureImagePage : ContentPage
	{
		public SignatureImagePage(string signaturePath)
		{
			Content = new StackLayout
			{
				Children = {
					new Image { Source = signaturePath }
				}
			};
		}
	}
}


