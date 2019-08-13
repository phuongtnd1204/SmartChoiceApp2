using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using SmartChoiceApp.Models;
using SmartChoiceApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartChoiceApp.ViewModels
{
    public class ProductPageViewModel : ViewModelBase
    {
        #region Properties
        public bool isLoading { get; set; }
        public Database.Database database { get; set; }
        public int ProductID { get; set; }
        public ICommand ManufacturerDetailCommand { get; set; }
        public ICommand PestilentInsectCommand { get; set; }
        public ICommand ReviewCommand { get; set; }
        public ICommand AddReviewCommand { get; set; }
        INavigationService navigation;
        private ObservableCollection<data> imageSources;
        public ObservableCollection<data> ImageSources
        {
            get => imageSources;
            set => SetProperty(ref imageSources, value);
        }
        private ProductDetail product;
        public ProductDetail Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }

        private ImageSource images;
        public ImageSource Images
        {
            get => images;
            set => SetProperty(ref images, value);
        }

        private string productName;
        public string ProductName
        {
            get => productName;
            set => SetProperty(ref productName, value);
        }

        private int scanTimes;
        public int ScanTimes
        {
            get => scanTimes;
            set => SetProperty(ref scanTimes, value);
        }

        private double averageRating;
        public double AverageRating
        {
            get => averageRating;
            set => SetProperty(ref averageRating, value);
        }

        private string manufacturerName;
        public string ManufacturerName
        {
            get => manufacturerName;
            set => SetProperty(ref manufacturerName, value);
        }

        //private string ngayThuHoach;
        //public string NgayThuHoach
        //{
        //    get => ngayThuHoach;
        //    set => SetProperty(ref ngayThuHoach, value);
        //}

        //private string ngayTrong;
        //public string NgayTrong
        //{
        //    get => ngayTrong;
        //    set => SetProperty(ref ngayTrong, value);
        //}

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

        #region Constructor
        public ProductPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ManufacturerDetailCommand = new Command(ManufacturerDetailAction);
            PestilentInsectCommand = new Command(PestilentInsectAction);
            ReviewCommand = new Command(ReviewAction);
            AddReviewCommand = new Command(AddReviewAction);
            navigation = navigationService;
            ImageSources = new ObservableCollection<data>();
            Product = new ProductDetail();
            database = new Database.Database();
        }


        private async void Init()
        {
            hasProductIformation(false);
            await PopupNavigation.Instance.PushAsync(new ErrorPopup(), true);
            Product = await database.GetProductDetail(ProductID);
            if(Product != null)
            {
                hasProductIformation(true);
                ProductInfo = Product.infor;
                //NgayThuHoach = ProductInfo.NgayThuHoach.ToShortDateString();
                //NgayTrong = ProductInfo.NgayTrong.ToShortDateString();
            }
            else
            {
                hasProductIformation(false);
            }
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion
        #region Command

        private async void ManufacturerDetailAction()
        {
            NavigationParameters parameter = new NavigationParameters();
            parameter.Add("ID", ProductInfo.MaNSX);
            await navigation.NavigateAsync(new System.Uri("ManufacturerDetailPage",UriKind.Relative), parameter);
        }

        private async void PestilentInsectAction()
        {
            NavigationParameters parameter = new NavigationParameters();
            parameter.Add("ID", ProductInfo.MaSanPham);
            await navigation.NavigateAsync("PestilentInsectDetailPage", parameter);
        }

        private async void ReviewAction()
        {
            //NavigationParameters parameter = new NavigationParameters();
            //parameter.Add("MaLoaiSanPham", ProductInfo.MaLoaiSanPham);
            //await navigation.NavigateAsync(new System.Uri("ReviewPage", UriKind.Relative), parameter);

            NavigationParameters parameter = new NavigationParameters();
            parameter.Add("MaLoaiSanPham", ProductInfo.MaSanPham);
            await navigation.NavigateAsync(new System.Uri("CommentPage", UriKind.Relative), parameter);
        }

        private async void AddReviewAction()
        {
            NavigationParameters parameter = new NavigationParameters();
            parameter.Add("MaLoaiSanPham", ProductInfo.MaLoaiSanPham);
            await navigation.NavigateAsync(new System.Uri("ReviewPage", UriKind.Relative), parameter);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            ProductID = parameters.GetValue<int>("ID");
            Init();
        }

        #endregion

        #region Function
        private void hasProductIformation(bool b)
        {
            if(b)
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
