using Android.Content;
using SmartChoiceApp.Controls;
using SmartChoiceApp.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace SmartChoiceApp.Droid.Custom_Renderers
{
    class ExtendedEntryRenderer : EntryRenderer
    {
        public ExtendedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = (ExtendedEntry)e.NewElement;

                if (element.Borderless)
                    Control.Background = null;

                Control.SetPadding((int)element.Padding.Left, (int)element.Padding.Top,
                    (int)element.Padding.Right, (int)element.Padding.Bottom);
            }
        }
    }
}