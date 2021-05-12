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
    public partial class AddBookPage : ContentPage
    {
        private BookManager manager = new BookManager();
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void addBookButton_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var book = manager.AddBook(titleEntry.Text, isbnEntry.Text, descriptionEntry.Text);
                Navigation.PopAsync();
            }
            else
                DisplayAlert("No Connection", "No internet connection. Connection is needed to apply changes", "OK");
        }
    }
}