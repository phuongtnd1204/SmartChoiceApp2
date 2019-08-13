using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using SmartChoiceApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartChoiceApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Properties
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        INavigationService navigation { get; set; }
        IPageDialogService dialog { get; set; }
        public bool _isWaiting = false;
        public bool IsWaiting
        {
            get => _isWaiting;
            set => SetProperty(ref _isWaiting, value);
        }
        public string _tenDangNhap;
        public string TenDangNhap
        {
            get => _tenDangNhap;
            set => SetProperty(ref _tenDangNhap, value);
        }
        public string _matKhau;
        public string MatKhau
        {
            get => _matKhau;
            set => SetProperty(ref _matKhau, value);
        }
        private Database.Database database {get;set;}
        #endregion
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            database = new Database.Database();
            IsWaiting = false;
            LoginCommand = new Command(LoginAsync);
            SignUpCommand = new Command(SignUp);
            navigation = navigationService;
            dialog = pageDialogService;
        }
        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(_tenDangNhap) || string.IsNullOrEmpty(_matKhau))
            {
                await dialog.DisplayAlertAsync("Thông báo", "Vui lòng điền đầy đủ thông tin!", "OK");
                return;
            }
            IsWaiting = true;
            if (await database.Login(_tenDangNhap, _matKhau))
            {
                IsWaiting = false;
                Application.Current.MainPage = new NavigationPage(new MainTabbedPage());
            }
            else
            {
                IsWaiting = false;
                await dialog.DisplayAlertAsync("Thông báo", "Lỗi đăng nhập", "OK");
            }
        }

        private async void SignUp()
        {
            await navigation.NavigateAsync("SignUpPage");
        }
    }
}
