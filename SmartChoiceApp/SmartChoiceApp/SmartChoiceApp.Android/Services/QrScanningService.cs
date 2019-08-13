using System.Collections.Generic;
using System.Threading.Tasks;
using SmartChoiceApp.Service;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(implementorType: typeof(SmartChoiceApp.Droid.Services.QrScanningService))]
namespace SmartChoiceApp.Droid.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var app = new Android.App.Application();
            MobileBarcodeScanner.Initialize(app);

            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
                };

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan(options);
            if (result != null)
            {
                return result.Text;
            }
            else
            {
                return null;
            }

        }
    }
}