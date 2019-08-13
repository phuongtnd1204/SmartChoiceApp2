using Prism;
using Prism.Ioc;
using SmartChoiceApp.ViewModels;
using SmartChoiceApp.Views;
using Xamarin.Forms;
using Plugin.Media;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SmartChoiceApp
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public static User mainUser = null;
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //navigationparameters parameter = new navigationparameters();
            //parameter.add("id", 1);
            //await navigationservice.navigateasync("navigationpage/productpage", parameter);

            await CrossMedia.Current.Initialize();
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
            mainUser = new User();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<QRScanPage, QRScanPageViewModel>();
            containerRegistry.RegisterForNavigation<AccountPage, AccountPageViewModel>();
            containerRegistry.RegisterForNavigation<ManufacturerDetailPage, ManufacturerDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<PestilentInsectDetailPage, PestilentInsectDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductPage, ProductPageViewModel>();
            containerRegistry.RegisterForNavigation<ReviewPage, ReviewPageViewModel>();
            containerRegistry.RegisterForNavigation<CommentPage, CommentPageViewModel>();
        }
    }
}
