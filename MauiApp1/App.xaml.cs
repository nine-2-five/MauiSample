namespace MauiApp1
{
    public partial class App : Application
    {
        public App(ViewModels.MainPageViewModel vm)
        {
            InitializeComponent();

            MainPage = new MainPage(vm);
        }
    }
}
