using nopCommerceApp.iOS.Custom_Renderers;
using SmartChoiceApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace nopCommerceApp.iOS.Custom_Renderers
{
    class ExtendedEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = (ExtendedEntry)e.NewElement;

                if (element.Borderless)
                    Control.Background = null;
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
                Control.BackgroundColor = new UIKit.UIColor(1, 1, 1, 1);
            }

        }
    }
}
