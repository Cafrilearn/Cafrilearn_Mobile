using AfriLearn.Constants;
using AfriLearn.Models;
using AfriLearn.ViewModels;
using Microsoft.WindowsAzure.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AfriLearn.Services
{
    class MediaService :  SignUpViewModel
    {

        #region fields
        private ImageSource takenImage;
        private string imageFullUri;
        private bool takenImageVisibility;
        #endregion

        
        #region properties
        public ImageSource TakenImage
        {
            get { return takenImage; }
            set
            {
                takenImage = value;
                OnPropertyChanged(nameof(TakenImage));
            }
        }
        public string ImageFullUri
        {
            get { return imageFullUri; }
            set
            {
                imageFullUri = value;
                OnPropertyChanged(nameof(ImageFullUri));
            }
        }
        public bool TakenImageVisibility
        {
            get { return takenImageVisibility; }
            set
            {
                takenImageVisibility = value;
                OnPropertyChanged(nameof(TakenImageVisibility));
            }
        }
        #endregion

        public async Task PickUploadImage()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                NavigationService.DisplayAlert("Error", "Your phone can not pick a photo", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium,
                RotateImage = true
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                NavigationService.DisplayAlert("Error", "Could not get the photo, please try again.", "Ok");
                return;
            }

            TakenImage = ImageSource.FromStream(() => selectedImageFile.GetStream());

            UploadImage(selectedImageFile.GetStream());
        }
        public async void UploadImage(Stream image)
        {
            IsBusy = true;
            var account = CloudStorageAccount.Parse(AppConstants.ProfileImagesConnString);
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(AppConstants.ContainerName);
            string uniqueName = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{uniqueName}.jpg");
            await blockBlob.UploadFromStreamAsync(image);
            IsBusy = false;
            ImageFullUri = blockBlob.Uri.OriginalString;
        }
    }
}
