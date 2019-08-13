using SmartChoiceApp.Views;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using SmartChoiceApp.Models;
using System.Collections.ObjectModel;

namespace SmartChoiceApp.ViewModels
{
    public class ManufacturerDetailPageViewModel : ViewModelBase
    {
        #region Properties
        private ObservableCollection<data> imageSources;
        public ObservableCollection<data> ImageSources
        {
            get => imageSources;
            set => SetProperty(ref imageSources, value);
        }

        public Manufacturer _manufacturer;
        public Manufacturer Manufacturer
        {
            get => _manufacturer;
            set => SetProperty(ref _manufacturer, value);
        }
        public bool isLoading { get; set; }
        public int ManufactuereID { get; set; }

        public Database.Database database;
        #endregion

        public ManufacturerDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            database = new Database.Database();
            Manufacturer = new Manufacturer();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

            ManufactuereID = parameters.GetValue<int>("ID");
            Init();
        }

        #region Function
        private async void Init()
        {
            await PopupNavigation.Instance.PushAsync(new ErrorPopup(), true);
            Manufacturer = await database.GetManufacturerDetail(ManufactuereID);
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion
    }
}
