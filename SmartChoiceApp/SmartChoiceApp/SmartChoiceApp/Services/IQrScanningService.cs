using System.Threading.Tasks;

namespace SmartChoiceApp.Service
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
