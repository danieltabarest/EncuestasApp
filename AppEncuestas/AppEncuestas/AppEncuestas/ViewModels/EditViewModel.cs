using AppEncuestas.Models;
using AppEncuestas.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using AppEncuestas.Classes;

namespace AppEncuestas.ViewModels
{
    public class EditViewModel : Poll, INotifyPropertyChanged
    {
        #region Attributes
        private DialogService dialogService;
        private ApiService apiService;
        private NavigationService navigationService;
        private bool isRunning;
        private bool isEnabled;
        

        #endregion

        #region Properties
        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        public EditViewModel(Poll poll)
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            ContactId = poll.ContactId;
            FirstName = poll.FirstName;
            LastName = poll.LastName;
            BathroomsNumber = poll.BathroomsNumber;
            Independentworkers = poll.Independentworkers;
            PollDate = poll.PollDate;
            RelativesAbroad = poll.RelativesAbroad;
            NumberPeople = poll.NumberPeople;
            EmailAddress = poll.EmailAddress;
            PhoneNumber = poll.PhoneNumber;

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand DeletePollCommand { get { return new RelayCommand(DeletePoll); } }

        private async void DeletePoll()
        {
            var answer = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");
            if (!answer)
            {
                return;
            }

            var Poll = new Poll
            {
                EmailAddress = EmailAddress,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                BathroomsNumber = BathroomsNumber,
                RelativesAbroad = RelativesAbroad,
                Independentworkers = Independentworkers,
                PollDate = PollDate,
                NumberPeople = NumberPeople,
                ContactId = ContactId,
            };

            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.Delete("http://hogaresmedellin.somee.com", "/api", "/Contacts", Poll);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await navigationService.Back();
        }

        public ICommand SavePollCommand { get { return new RelayCommand(SavePoll); } }

        private async void SavePoll()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowMessage("Error", "You must enter a first name");
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowMessage("Error", "You must enter a last name");
                return;
            }


            var Poll = new Poll
            {
                EmailAddress = EmailAddress,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
                BathroomsNumber = BathroomsNumber,
                RelativesAbroad = RelativesAbroad,
                Independentworkers = Independentworkers,
                PollDate = PollDate,
                NumberPeople = NumberPeople,
                ContactId = ContactId,
            };

            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.Put("http://hogaresmedellin.somee.com", "/api", "/Contacts", Poll);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            await navigationService.Back();
        }


        #endregion
    }
}
