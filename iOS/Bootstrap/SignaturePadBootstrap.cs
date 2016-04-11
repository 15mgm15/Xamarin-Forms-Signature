// HACK: this is to deal with the linker nuking the assembly
using Acr.XamForms.SignaturePad.iOS;

namespace Signature.iOS.Bootstrap
{
    public class SignaturePadBootstrap 
    {
        public SignaturePadBootstrap() 
        {
            new SignaturePadRenderer();
        }
    }
}