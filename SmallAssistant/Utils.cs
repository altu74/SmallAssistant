using System.Threading.Tasks;
using Xamarin.Forms;
namespace SmallAssistant
{
    public static class Utils
    {
        public static async void ShowError(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Error", message, "Ok");
        }

    }
}