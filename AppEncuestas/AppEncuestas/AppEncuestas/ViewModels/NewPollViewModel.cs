using AppEncuestas.Classes;
using AppEncuestas.Models;
using AppEncuestas.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppEncuestas.ViewModels
{
    public class NewPollViewModel : Poll, INotifyPropertyChanged
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
        public NewPollViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();

            IsEnabled = true;
        }
        #endregion

        #region Commands
       

        public ICommand NewPollCommand { get { return new RelayCommand(NewPoll); } }

        private async void NewPoll()
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
            if (string.IsNullOrEmpty(EmailAddress))
            {
                await dialogService.ShowMessage("Error", "You must enter a email address");
                return;
            }

            if (!ValidateEmail(EmailAddress))
            {
                await dialogService.ShowMessage("Error", "You must enter a valid email");
                return;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await dialogService.ShowMessage("Error", "You must enter Phone Number");
                return;
            }
            if (PhoneNumber.Length <= 7)
            {
                await dialogService.ShowMessage("Error", "Minimal Phone Number is 7 numbers");
                return;
            }

            if (Convert.ToInt32(BathroomsNumber) < 0 )
            {
                await dialogService.ShowMessage("Error", "You must enter number bathrooms");
                return;
            }

        
            
            if (Convert.ToInt32(NumberPeople) <= 0)
            {
                await dialogService.ShowMessage("Error", "You must enter number people");
                return;
            }

            var Poll = new Poll
            {
                EmailAddress = EmailAddress,
                FirstName = FirstName,
                BathroomsNumber = BathroomsNumber,
                RelativesAbroad = RelativesAbroad,
                Independentworkers = Independentworkers,
                PollDate = PollDate,
                NumberPeople = NumberPeople,
                LastName = LastName,
                PhoneNumber = PhoneNumber,
            };

            IsRunning = true;
            IsEnabled = false;
            var response = await apiService.Post("http://hogaresmedellin.somee.com", "/api", "/Contacts", Poll);
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


        Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return EmailRegex.IsMatch(email);
        }
    }
}
