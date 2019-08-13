using Xamarin.Forms;

namespace SmartChoiceApp.Views
{
    public partial class AccountPage : ContentPage
    {
        static bool isOpen = false;
        public AccountPage()
        {
            InitializeComponent();
        }

        private void ChangePassword_Click(object sender, System.EventArgs e)
        {
            if(isOpen)
            {
                PassWordLayout.IsVisible = false;
                passWordButton.IsVisible = false;
                saveButton.IsVisible = true;
                isOpen = false;
            }
            else
            {
                PassWordLayout.IsVisible = true;
                passWordButton.IsVisible = true;
                saveButton.IsVisible = false;
                isOpen = true;
            }
        }
    }
}
