using SmartChoiceApp.Service;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ZXing.Mobile;

[assembly: Xamarin.Forms.Dependency(typeof(SmartChoiceApp.iOS.Services.QrScanningService))]
namespace SmartChoiceApp.iOS.Services
{
        public class QrScanningService : IQrScanningService
        {
            public async Task<string> ScanAsync()
            {
                
                var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
                options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE
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