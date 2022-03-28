using System;
using System.Collections.Generic;
using System.ComponentModel;
using UploadFiles.Models;
using UploadFiles.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UploadFiles.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}