using AppEncuestas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEncuestas.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollPage : ContentPage
    {
        public PollPage()
        {
            InitializeComponent();

            var mainViewModel = MainViewModel.GetInstance();
            base.Appearing += (object sender, EventArgs e) =>
            {
                mainViewModel.RefreshCommand.Execute(this);
            };
        }
    }
}
