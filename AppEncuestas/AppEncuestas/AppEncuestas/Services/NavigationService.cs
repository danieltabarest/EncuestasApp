using AppEncuestas.Pages;
using System.Threading.Tasks;

namespace AppEncuestas.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "EditPollPage":
                    await App.Current.MainPage.Navigation.PushAsync(new EditPollPage());
                    break;
                case "NewPollPage":
                    await App.Current.MainPage.Navigation.PushAsync(new NewPollPage());
                    break;
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
