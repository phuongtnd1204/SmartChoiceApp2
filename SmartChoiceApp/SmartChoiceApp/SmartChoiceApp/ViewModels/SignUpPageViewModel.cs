using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartChoiceApp.ViewModels
{
    public class SignUpPageViewModel : ViewModelBase
    {
        #region Properties
        public ICommand SignUpCommand { get; set; }
        INavigationService navigation { get; set; }
        IPageDialogService dialog { get; set; }
        public bool _isWaiting = false;
        public bool IsWaiting
        {
            get => _isWaiting;
            set => SetProperty(ref _isWaiting, value);
        }
        public User _userInfo;
        public User UserInfo
        {
            get => _userInfo;
            set => SetProperty(ref _userInfo, value);
        }
        public string _tenNguoiDung;
        public string TenNguoiDung
        {
            get => _tenNguoiDung;
            set => SetProperty(ref _tenNguoiDung, value);
        }
        public string _SDT;
        public string SDT
        {
            get => _SDT;
            set => SetProperty(ref _SDT, value);
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
        public string _matKhau2;
        public string MatKhau2
        {
            get => _matKhau2;
            set => SetProperty(ref _matKhau2, value);
        }
        private Database.Database database { get; set; }
        #endregion
        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            database = new Database.Database();
            IsWaiting = false;
            UserInfo = new User();
            SignUpCommand = new Command(SignUpAction);
            navigation = navigationService;
            dialog = pageDialogService;
        }
        private async void SignUpAction()
        {
            if(string.IsNullOrEmpty(UserInfo.MatKhau) || string.IsNullOrEmpty(UserInfo.SDT.ToString()) ||
                string.IsNullOrEmpty(UserInfo.TenDangNhap) ||
                string.IsNullOrEmpty(UserInfo.TenNguoiDung) || string.IsNullOrEmpty(_matKhau2))
            {
                await dialog.DisplayAlertAsync("Thông báo", "Vui lòng điền đầy đủ thông tin!", "OK");
                return;
            }

            if (_matKhau2 != UserInfo.MatKhau)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Xác nhận mật khẩu không đúng", "OK");
                return;
            }

            if (SDT.Contains("-") || SDT.Contains(","))
            {
                await dialog.DisplayAlertAsync("Thông báo", "Số điện thoại chứa ký tự không cho phép!", "OK");
                return;
            }

            IsWaiting = true;
             if(await database.CheckExist(UserInfo.TenDangNhap))
              {
                        IsWaiting = false;
                        await dialog.DisplayAlertAsync("Thông báo", "Tên đăng nhập đã tồn tại", "OK");
                        return; 
             }

             _userInfo.SDT = Int32.Parse(SDT);
              if (await database.SignUp(_userInfo))
              {
                                IsWaiting = false;
                                await dialog.DisplayAlertAsync("Thông báo", "Đăng ký thành công", "OK");
                                await navigation.GoBackAsync();
              }
              else
              {
                                IsWaiting = false;
                                await dialog.DisplayAlertAsync("Thông báo", "Đăng ký không thành công", "OK");
              }
        }
    }
}
