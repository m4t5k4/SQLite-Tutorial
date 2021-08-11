using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SQLite_Tutorial
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Subscribed = subscribed.IsChecked
                });

                nameEntry.Text = string.Empty;
                subscribed.IsChecked = false;
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }

        Person lastSelection;
        private void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Person;
            nameEntry.Text = lastSelection.Name;
            subscribed.IsChecked = lastSelection.Subscribed;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                lastSelection.Name = nameEntry.Text;
                lastSelection.Subscribed = subscribed.IsChecked;

                await App.Database.UpdatePersonAsync(lastSelection);
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.Database.DeletePersonAsync(lastSelection);
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();

                nameEntry.Text = "";
                subscribed.IsChecked = false;
            }
        }

        //Get subscribed
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.QuerySubscribedAsync();
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.LinqNotSubscribed();
        }
    }
}
