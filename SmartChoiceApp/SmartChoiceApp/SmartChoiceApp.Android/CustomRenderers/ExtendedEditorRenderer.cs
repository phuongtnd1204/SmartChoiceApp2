using Android.Content;
using SmartChoiceApp.Controls;
using SmartChoiceApp.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendEditor), typeof(ExtendedEditorRenderer))]
namespace SmartChoiceApp.Droid.Custom_Renderers
{
    class ExtendedEditorRenderer : EditorRenderer
    {
        public ExtendedEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = (ExtendEditor)e.NewElement;

                if (element.Borderless)
                    Control.Background = null;

                Control.SetPadding((int)element.Padding.Left, (int)element.Padding.Top,
                    (int)element.Padding.Right, (int)element.Padding.Bottom);
            }
        }
    }
}