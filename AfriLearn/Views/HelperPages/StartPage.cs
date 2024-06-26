﻿using AfriLearn.Services;
using AfriLearnMobile.Models;
using Akavache;
using System;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace AfriLearn.Views.Helpers
{
    public class StartPage : ContentPage
    {
        public StartPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var appUser = await BlobCache.UserAccount.GetObject<AppUser>("appUser");
                NavigationService.PushAsync(new HomePage());
            }
            catch (Exception)
            {
                NavigationService.PushAsync(new IntroPageOne());
            }
        }
    }
}