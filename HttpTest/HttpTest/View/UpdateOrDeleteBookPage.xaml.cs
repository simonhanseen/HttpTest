using HttpTest.Data;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HttpTest.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateOrDeleteBookPage : ContentPage
    {
        private BookManager manager = new BookManager();
        private Book _book;
        public UpdateOrDeleteBookPage(Book book)
        {
            InitializeComponent();
            _book = book;
            titleEntry.Text = book.Title;
            isbnEntry.Text = book.ISBN;
            descriptionEntry.Text = book.Description;
        }

        private void updateBookButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var book = manager.UpdateBook(_book.id, titleEntry.Text, isbnEntry.Text, descriptionEntry.Text);
                Navigation.PopAsync();
            }
            else
                DisplayAlert("No Connection", "No internet connection. Connection is needed to apply changes", "OK");
        }

        private void deleteBookButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var book = manager.DeleteBook(_book.id);
                Navigation.PopAsync();
            }
            else
                DisplayAlert("No Connection", "No internet connection. Connection is needed to apply changes", "OK");
        }
    }
}