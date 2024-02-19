﻿using MauiApp1.ViewModels;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm;
        int count = 0;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
            _vm = vm;
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Call your async method here
            await _vm.LoadAsync();
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            await _vm.LoadAsync();
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}
