using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartChoiceApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FloatingEntry : ContentView
	{
		public FloatingEntry ()
		{
			InitializeComponent ();
            
		}
        #region Placeholder

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(FloatingEntry),
                string.Empty,
                BindingMode.TwoWay,
                propertyChanged: OnPlaceholderChanged);

        private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var floatingEntry = (FloatingEntry)bindable;
            floatingEntry.MainEntry.Placeholder = (string)newValue;
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        #endregion


        #region Title

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(FloatingEntry),
                string.Empty,
                BindingMode.TwoWay);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        #endregion

        //#region PlaceholderColor

        //public static readonly BindableProperty PlaceholderColorProperty =
        //    BindableProperty.Create(
        //        nameof(PlaceholderColor),
        //        typeof(Color),
        //        typeof(FloatingEntry),
        //        BindingMode.TwoWay);

        //public Color PlaceholderColor
        //{
        //    get => (Color)GetValue(TitleProperty);
        //    set => SetValue(TitleProperty, value);
        //}

        //#endregion

        //#region TextColor

        //public static readonly BindableProperty TextColorProperty =
        //    BindableProperty.Create(
        //        nameof(TextColor),
        //        typeof(Color),
        //        typeof(FloatingEntry),
        //        string.Empty,
        //        BindingMode.TwoWay);

        //public Color TextColor
        //{
        //    get => (Color)GetValue(TitleProperty);
        //    set => SetValue(TitleProperty, value);
        //}

        //#endregion

        #region Text

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(FloatingEntry),
                string.Empty,
                BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region Background

        public static readonly BindableProperty BackgroundProperty =
            BindableProperty.Create(
                nameof(Background),
                typeof(string),
                typeof(FloatingEntry),
                string.Empty,
                BindingMode.TwoWay);

        public string Background
        {
            get => (string)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        #endregion

        #region FocusedBackground

        public static readonly BindableProperty FocusedBackgroundProperty =
            BindableProperty.Create(
                nameof(FocusedBackground),
                typeof(string),
                typeof(FloatingEntry),
                string.Empty,
                BindingMode.TwoWay);

        public string FocusedBackground
        {
            get => (string)GetValue(FocusedBackgroundProperty);
            set => SetValue(FocusedBackgroundProperty, value);
        }

        #endregion

        #region IsPassword

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(
                nameof(IsPassword),
                typeof(bool),
                typeof(FloatingEntry),
                false,
                BindingMode.TwoWay,
                propertyChanged: OnIsPasswordChanged);

        private static void OnIsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var floatingEntry = (FloatingEntry)bindable;
            floatingEntry.MainEntry.IsPassword = (bool)newValue;
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        #endregion

        #region IconSpacing

        public static readonly BindableProperty IconSpacingProperty =
            BindableProperty.Create(
                nameof(IconSpacing),
                typeof(int),
                typeof(FloatingEntry),
                20,
                BindingMode.TwoWay);

        public int IconSpacing
        {
            get => (int)GetValue(IconSpacingProperty);
            set => SetValue(IconSpacingProperty, value);
        }

        #endregion

        #region Focus/Unfocus changed
        private void MainEntry_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(MainEntry.Text))
            {
                MainTitle.IsVisible = true;
                MainEntry.Placeholder = "";
            }

            // Change background here
        }

        private void MainEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(MainEntry.Text))
            {
                MainTitle.IsVisible = false;
                MainEntry.Placeholder = this.Placeholder;
            }

            // Change background here
        }

        private void MainEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(MainEntry.Text))
            {
                MainTitle.IsVisible = true;
                MainEntry.Placeholder = "";
            }
        }

        #endregion
    }
}