using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using SmartChoiceApp.Models;
using SmartChoiceApp.Views;
using System.Collections.ObjectModel;

namespace SmartChoiceApp.ViewModels
{
    public class PestilentInsectDetailPageViewModel : ViewModelBase
    {
        #region Properties
        public ObservableCollection<PestilentInsect> _insects;
        public ObservableCollection<PestilentInsect> Insects
        {
            get => _insects;
            set => SetProperty(ref _insects, value);
        }
        public int PestilentInsectID { get; set; }

        public Database.Database database;

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
        public PestilentInsectDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            database = new Database.Database();
            Insects = new ObservableCollection<PestilentInsect>();
        }
        #endregion

        #region Override & Init
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            PestilentInsectID = parameters.GetValue<int>("ID");
            Init();
        }

        private async void Init()
        {
            hasProductIformation(true);
            await PopupNavigation.Instance.PushAsync(new ErrorPopup(), true);
            var x = await database.GetPestilentInsect(PestilentInsectID);
            if(x == null)
            {
                hasProductIformation(false);
            }
            else
            {
                Insects = new ObservableCollection<PestilentInsect>(x);
                if (Insects.Count == 0)
                {
                    hasProductIformation(false);
                }
            }
           
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion

        #region Function
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
