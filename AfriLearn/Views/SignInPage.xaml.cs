﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AfriLearn.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }
        protected  override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}