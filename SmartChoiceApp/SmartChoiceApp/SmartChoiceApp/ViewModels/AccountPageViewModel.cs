using Prism.Navigation;
using Prism.Services;
using SmartChoiceApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

using System.Net.Http;
using System.Net.Http.Headers;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace SmartChoiceApp.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        #region Properties
        private Database.Database database { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SavePassWordCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand UpdateAvatarCommand { get; set; }
        private bool _isWaiting = false;
        public bool IsWaiting
        {
            get => _isWaiting;
            set => SetProperty(ref _isWaiting, value);
        }
        private MediaFile imageFile;
        public MediaFile ImageFile
        {
            get => imageFile;
            set => SetProperty(ref imageFile, value);
        }

        private User _userInfo;
        public User UserInfo
        {
            get => _userInfo;
            set => SetProperty(ref _userInfo, value);
        }

        private string _oldPass;
        public string OldPass
        {
            get => _oldPass;
            set => SetProperty(ref _oldPass, value);
        }

        private string _anhDaiDien;
        public string AnhDaiDien
        {
            get => _anhDaiDien;
            set => SetProperty(ref _anhDaiDien, value);
        }

        private string _newPass;
        public string NewPass
        {
            get => _newPass;
            set => SetProperty(ref _newPass, value);
        }

        private string _confirmPass;
        public string ConfirmPass
        {
            get => _confirmPass;
            set => SetProperty(ref _confirmPass, value);
        }



        INavigationService navigation;
        IPageDialogService dialog;
        #endregion
        public AccountPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            database = new Database.Database();
            SaveCommand = new Command(SaveAction);
            SavePassWordCommand = new Command(SavePassWordAction);
            LogoutCommand = new Command(LogoutAction);
            UpdateAvatarCommand = new Command(UpdateAvatarAction);
            navigation = navigationService;
            dialog = pageDialogService;
            ReceiveData();
        }

        private void ReceiveData()
        {
            _userInfo = App.mainUser;
            AnhDaiDien = _userInfo.AnhDaiDien;
        }

        private async void SaveAction()
        {
            var stringNumber = UserInfo.SDT.ToString();
            if (string.IsNullOrEmpty(stringNumber) == true)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
                return;
            }
            else if (stringNumber.Contains("-") || stringNumber.Contains(","))
            {
                await dialog.DisplayAlertAsync("Thông báo", "Số điện thoại chứa ký tự không cho phép!", "OK");
                return;
            }
            else
            {
                IsWaiting = true;
                ShowNotification(await database.UpdateUser(_userInfo));
            }
        }

        private async void SavePassWordAction()
        {
            if (string.IsNullOrEmpty(OldPass) == true || string.IsNullOrEmpty(NewPass) == true
                || string.IsNullOrEmpty(ConfirmPass) == true)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
                return;
            }
            else if (OldPass != UserInfo.MatKhau)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Mật khẩu không đúng", "OK");
                return;
            }
            else if (NewPass != ConfirmPass)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Xác nhận mật khẩu không đúng", "OK");
                return;
            }         
            else
            {
                UserInfo.MatKhau = NewPass;
                IsWaiting = true;
                ShowNotification(await database.UpdateUser(_userInfo));
            }
               
            }

        private async void UpdateAvatarAction()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Thao tác không được hỗ trợ", "OK");
                return;
            }

            //var options = new StoreCameraMediaOptions();
            //options.SaveToAlbum = true;
            //options.PhotoSize = PhotoSize.Small;

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;
            imageFile = file;
            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    return stream;
            //});
            if(await database.UpdateAvatar(imageFile, App.mainUser.MaNguoiDung))
            {
                App.mainUser = await database.GetUser(App.mainUser.MaNguoiDung);
                ReceiveData();
                await dialog.DisplayAlertAsync("Thông báo", "Cập nhật thành công", "OK");
            }
            else
            {
                await dialog.DisplayAlertAsync("Thông báo", "Cập nhật không thành công", "OK");
            }


        }

        private void LogoutAction()
        {
            App.mainUser = null;
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        #region Function
        private async void ShowNotification(bool b)
        {
            if (b)
            {
                IsWaiting = false;
                await dialog.DisplayAlertAsync("Thông báo", "Cập nhật thành công", "OK");
            }
            else
            {
                IsWaiting = false; ;
                await dialog.DisplayAlertAsync("Thông báo", "Lỗi cập nhật, thử lại!", "OK");
            }
        }
        #endregion
    }
}
