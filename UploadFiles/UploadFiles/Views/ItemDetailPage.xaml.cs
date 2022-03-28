using System.ComponentModel;
using UploadFiles.ViewModels;
using Xamarin.Forms;

namespace UploadFiles.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}