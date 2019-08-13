

using Prism.Navigation;
using SmartChoiceApp.Service;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartChoiceApp.ViewModels
{
    public class QRScanPageViewModel : ViewModelBase
    {
        #region Properties
        public ICommand ScanCommand { get; set; }

        private string _result;
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        INavigationService _navigationService;
        #endregion
        public QRScanPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;        
            ScanCommand = new Command(Scan);
        }
        private async void Scan()
        {
            var scanner = DependencyService.Get<IQrScanningService>();
            var result = await scanner.ScanAsync();
            if (result != null)
            {
                Result = result;
                NavigationParameters param = new NavigationParameters();
                param.Add("ID", Result);
                await _navigationService.NavigateAsync("ProductPage", param);
            }
            //var x = _result.Text;
            //NavigationParameters param = new NavigationParameters();
            //param.Add("ID", 1);
            //await _navigationService.NavigateAsync("ProductPage", param);

        }
    }
}
