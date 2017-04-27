using AppEncuestas.Models;
using AppEncuestas.Services;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace AppEncuestas.ViewModels
{
    public class PollItemViewModel : Poll
    {
        #region Attributes
        private NavigationService navigationService;
        #endregion

        #region Constructors

        public PollItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand PollCommand { get { return new RelayCommand(Poll); } }

        private async void Poll()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Poll = new EditViewModel(this);
            await navigationService.Navigate("EditPollPage");
        }
        #endregion
    }
}
