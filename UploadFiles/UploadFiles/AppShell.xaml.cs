using System;
using System.Collections.Generic;
using UploadFiles.ViewModels;
using UploadFiles.Views;
using Xamarin.Forms;

namespace UploadFiles
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
