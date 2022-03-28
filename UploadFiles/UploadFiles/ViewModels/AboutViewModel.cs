using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UploadFiles.ViewModels
{
    public class FileList
    {
        public string Title { get; set; }
    }

   
    public class AboutViewModel : BaseViewModel
    {
        ObservableCollection<FileList> fileLists = new ObservableCollection<FileList>();
        public ObservableCollection<FileList> FileLists { get { return fileLists; } }
        private static string ConnectionString => "your blob connection string";
        private static string Container => "data";

        private FileList selectedFile;
        public FileList SelectedFile
        {
            get => selectedFile;
            set => SetProperty(ref selectedFile, value);
        }

        public AsyncCommand RefreshCommand { get; }
        private static string fileName;
        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);
        public ICommand DownloadCommand { get; }
        public ICommand SelectCommand { get; }
        public AboutViewModel()
        {
            Title = "Files";
            ShowFiles();
            RefreshCommand = new AsyncCommand(Refresh);
            DownloadCommand = new Command(DownloadFile);
            SelectCommand = new Command<FileList>(Select);
        }
        public async Task Refresh()
        {
            IsBusy = true;
            FileLists.Clear();
            ShowFiles();
            IsBusy = false;
        }
        async void ShowFiles()
        {

            await foreach (BlobItem blobItem in service.GetBlobContainerClient(Container).GetBlobsAsync())
            {
                fileLists.Add(new FileList { Title = blobItem.Name});
            }

        }

        private async void Select(FileList file)
        {
            selectedFile = file; 
            
        }

        [Obsolete]
        private async void DownloadFile()
        {

            var cloudBlockBlob = service.GetBlobContainerClient(Container).GetBlobClient(selectedFile.Title);

            try
            {
                var res = cloudBlockBlob.DownloadStreaming();
                
                string folder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

                BlobDownloadInfo response = await cloudBlockBlob.DownloadAsync();
                var stream = response.Content;


                //var blockBlob = container.GetBlobClient("mikepic.png");
                string localPath = Path.Combine(folder, selectedFile.Title);
                using (var fileStream = System.IO.File.OpenWrite(localPath))
                {
                    cloudBlockBlob.DownloadTo(fileStream);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}