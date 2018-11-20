using System;
using SmallAssistant.Models;
using Xamarin.Forms;

namespace SmallAssistant.ViewModels
{
    public partial class MainPage: MasterDetailPage
    {
        public MainPage()
        {
            try
            {
                InitializeComponent();
                menuPage.mainMenu.ItemSelected += OnItemSelected;

                if (Device.RuntimePlatform == Device.UWP)
                {
                    MasterBehavior = MasterBehavior.Popover;
                }

            }
            catch (Exception ex)
            {

                DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                menuPage.mainMenu.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}