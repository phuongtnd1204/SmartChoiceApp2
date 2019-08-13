using Prism.Navigation;
using Prism.Services;
using SmartChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartChoiceApp.ViewModels
{
    public class ReviewPageViewModel : ViewModelBase
    {
        #region Properties
        Database.Database database { get; set; }
        private double _rating;
        public double Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public ICommand AddReviewCommand { get; set; }

        public List<string> starList { get; set; }
        private string MaLoaiSanPham { get; set; }
        IPageDialogService dialog { get; set; }
        #endregion
        public ReviewPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            database = new Database.Database();
            AddReviewCommand = new Command(AddReviewAction);
            dialog = pageDialogService;
            Init();
        }

        private void Init()
        {
            starList = new List<string> { "1", "2", "3", "4", "5" };
            Rating = 0;
        }

        private async void AddReviewAction()
        {
            if(Rating == 0)
            {
                await dialog.DisplayAlertAsync("Thông báo", "Vui lòng chọn số sao!", "OK");
                return;
            }
            Review userReview = new Review
            {
                MaNguoiDung = App.mainUser.MaNguoiDung,
                MaLoaiSanPham = MaLoaiSanPham,
                Rating = Rating,
                NoiDung = Comment,
            }; 
            if(await database.AddReview(userReview))
            {
                await dialog.DisplayAlertAsync("Thông báo", "Gửi đánh giá thành công!", "OK");
            }
            else
            {
                await dialog.DisplayAlertAsync("Thông báo", "Lỗi, vui lòng thử lại!", "OK");
            }
        }
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            MaLoaiSanPham = parameters.GetValue<int>("MaLoaiSanPham").ToString();
            Init();
        }
    }
}
