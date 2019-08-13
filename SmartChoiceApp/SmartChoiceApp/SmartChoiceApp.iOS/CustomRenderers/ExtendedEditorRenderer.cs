using nopCommerceApp.iOS.Custom_Renderers;
using SmartChoiceApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendEditor), typeof(ExtendedEditorRenderer))]
namespace nopCommerceApp.iOS.Custom_Renderers
{
    class ExtendedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var element = (ExtendEditor)e.NewElement;

                if (element.Borderless)
                    Control.Layer.BorderWidth = 0;
            }
        }
    }
}
