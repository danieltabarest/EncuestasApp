using AppEncuestas.Models;
using AppEncuestas.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using System.ComponentModel;

namespace AppEncuestas.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private bool isRefreshing;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public ObservableCollection<PollItemViewModel> Polls { get; set; }

        public NewPollViewModel NewPoll { get; set; }

        public EditViewModel Poll { get; set; }

        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get
            {
                return isRefreshing;
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;

            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            Polls = new ObservableCollection<PollItemViewModel>();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private async void LoadPolls()
        {
            var response = await apiService.Get<Poll>("http://hogaresmedellin.somee.com/", "/api", "/Contacts");

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            ReloadPolls((List<Poll>)response.Result);
        }

        private void ReloadPolls(List<Poll> polls)
        {
             Polls.Clear();
            foreach (var poll in polls.OrderByDescending(c => c.PollDate))
            {
                Polls.Add(new PollItemViewModel
                {
                    ContactId = poll.ContactId,
                    EmailAddress = poll.EmailAddress,
                    FirstName = poll.FirstName,
                    BathroomsNumber = poll.BathroomsNumber,
                    RelativesAbroad = poll.RelativesAbroad,
                    Independentworkers = poll.Independentworkers,
                    NumberPeople = poll.NumberPeople,
                    PollDate = poll.PollDate,
                    LastName = poll.LastName,
                    PhoneNumber = poll.PhoneNumber,
                });
            }
             
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }

        private void Refresh()
        {
            IsRefreshing = true;
            LoadPolls();
            IsRefreshing = false;
        }

        public ICommand AddPollCommand { get { return new RelayCommand(AddPoll); }  }

        private async void AddPoll()
        {
            NewPoll = new NewPollViewModel();
            await navigationService.Navigate("NewPollPage");
        }
        #endregion
    }
}