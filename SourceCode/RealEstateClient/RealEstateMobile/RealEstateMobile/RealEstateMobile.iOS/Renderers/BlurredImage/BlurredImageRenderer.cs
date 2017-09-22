using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using RealEstateMobile.iOS.Renderers.BlurredImage;
using System.Threading.Tasks;
using CoreImage;
using RealEstateMobile.Controls;

[assembly: ExportRenderer(typeof(BlurredImage), typeof(BlurredImageRenderer))]
namespace RealEstateMobile.iOS.Renderers.BlurredImage
{
    public class BlurredImageRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            if (Control == null)
            {
                SetNativeControl(new BlurredImageView
                {
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    ClipsToBounds = true
                });
            }

            base.OnElementChanged(e);
        }

        private class BlurredImageView : UIImageView
        {
            public override UIImage Image
            {
                get { return base.Image; }
                set
                {
                    // This may take up to a second so don't block the UI thread.
                    Task.Run(() =>
                    {
                        using (var context = CIContext.Create())
                        using (var inputImage = CIImage.FromCGImage(value.CGImage))
                        using (var filter = new CIGaussianBlur() { Image = inputImage, Radius = 5 })
                        using (var resultImage = context.CreateCGImage(filter.OutputImage, inputImage.Extent))
                        {
                            InvokeOnMainThread(() => base.Image = new UIImage(resultImage));
                        }
                    });
                }
            }
        }
    }
}