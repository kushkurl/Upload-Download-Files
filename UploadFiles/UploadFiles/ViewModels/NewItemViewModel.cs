using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using UploadFiles.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UploadFiles.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private static string ConnectionString => "your blob connection string";
        private static string Container => "data";

        private static string filePath;
        private static string fileName;
        private readonly BlobServiceClient service = new BlobServiceClient(ConnectionString);
        public ICommand UploadCommand { get; }
        public ICommand SelectCommand { get; }

        private string Fname;
        public string fName
        {
            get => Fname;
            set => SetProperty(ref Fname, value);
        }
        public NewItemViewModel()
        {
            Title = "About";
            UploadCommand = new Command(UploadFile);
            SelectCommand = new Command(SelectFile);
        }

        private async void SelectFile()
        {
            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Document"

            });
            fName = file.FileName;
            fileName = file.FileName;
            filePath = file.FullPath;
            /*  var stream = new MemoryStream();

              var writer = new StreamWriter(stream);
              writer.Write(await file.OpenReadAsync());
              writer.Flush();*/

            /*var text = File.ReadAllText(filePath);


            stream.Position = 0;

            try
            {
                BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(file.FileName);
                await blob.UploadAsync(stream);
            }
            catch { }
            finally { writer.Dispose(); }*/
        }
        private async void UploadFile()
        {
            //var file = await CrossFilePicker.Current.PickFile();


            if (filePath != null)
            {
                var text = File.ReadAllBytes(filePath);
                var stream = new MemoryStream();

                var writer = new StreamWriter(stream);
                writer.Write(text);
                writer.Flush();

                stream.Position = 0;

                try
                {
                    BlobClient blob = service.GetBlobContainerClient(Container).GetBlobClient(fileName);

                    //var blockBlob = container.GetBlobClient("mikepic.png");


                    using (var fileStream = System.IO.File.OpenRead(filePath))
                    {
                        blob.Upload(fileStream);
                    }


                    
                    await blob.UploadAsync(stream);
                }
                catch { }
                finally { writer.Dispose(); }

            }
        }
    }
}
