using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpTest.Data;
using HttpTest.View;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace HttpTest
{
    public partial class MainPage : ContentPage
    {
        private BookManager manager = new BookManager();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var books = await manager.GetAllBooks();
                BooksListView.ItemsSource = books;
            }
            else
                await DisplayAlert("No Connection", "No internet connection. Connection is needed to apply changes", "OK");
        }

        private void AddBook_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddBookPage());
        }

        private void BooksListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender == null) return;

            var selectedItem = (ListView)sender;
            Book book = (Book)selectedItem.SelectedItem;

            Navigation.PushAsync(new UpdateOrDeleteBookPage(book));
        }
    }
}
