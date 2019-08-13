using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using SmartChoiceApp.Models;
using SmartChoiceApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartChoiceApp.ViewModels
{
	public class CommentPageViewModel : ViewModelBase
    {

        #region Properties
        public bool isLoading { get; set; }
        public Database.Database database { get; set; }
        public int ProductID { get; set; }
        public ICommand ReviewCommand { get; set; }
        INavigationService navigation;

        private ProductDetail product;
        public ProductDetail Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }


      
        private ProductInfo productInfo;
        public ProductInfo ProductInfo
        {
            get => productInfo;
            set => SetProperty(ref productInfo, value);
        }

        private ObservableCollection<Review> reviews;
        public ObservableCollection<Review> Reviews
        {
            get => reviews;
            set => SetProperty(ref reviews, value);
        }

        private bool informationLayout;
        public bool InformationLayout
        {
            get => informationLayout;
            set => SetProperty(ref informationLayout, value);
        }

        private bool noInformationLayout;
        public bool NoInformationLayout
        {
            get => noInformationLayout;
            set => SetProperty(ref noInformationLayout, value);
        }
        #endregion

        public CommentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ReviewCommand = new Command(ReviewAction);
            navigation = navigationService;
            Product = new ProductDetail();
            database = new Database.Database();
        }

        private async void Init()
        {
            hasProductIformation(false);
            await PopupNavigation.Instance.PushAsync(new ErrorPopup(), true);
            Product = await database.GetProductDetail(ProductID);
            if (Product.comments.Count > 0)
            {
                hasProductIformation(true);
                Reviews = new ObservableCollection<Review>(Product.comments);
                //(string)Reviews[0].NgayBinhLuan = Reviews[0].NgayBinhLuan.ToLongTimeString();
            }
            else
            {
                hasProductIformation(false);
            }
            await PopupNavigation.Instance.PopAsync();
        }

        private async void ReviewAction()
        {
            NavigationParameters parameter = new NavigationParameters();
            parameter.Add("MaLoaiSanPham", ProductID);
            await navigation.NavigateAsync(new System.Uri("ReviewPage", UriKind.Relative), parameter);
        }



        #region Function
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            ProductID = parameters.GetValue<int>("MaLoaiSanPham");
            Init();
        }

        private void hasProductIformation(bool b)
        {
            if (b)
            {
                InformationLayout = true;
                NoInformationLayout = false;
            }
            else
            {
                InformationLayout = false;
                NoInformationLayout = true;
            }
        }

        #endregion
    }
}
