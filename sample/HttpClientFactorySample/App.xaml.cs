using Prism;
using Prism.Ioc;
using HttpClientFactorySample.ViewModels;
using HttpClientFactorySample.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using HttpClientFactorySample.Services;
using HttpClientFactorySample.Helpers;
using Mobile.BuildTools.Configuration;

namespace HttpClientFactorySample
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IMarvelApiService, MarvelApiService>();
            containerRegistry.RegisterInstance(Plugin.DefaultHttpClientFactory.CrossDefaultHttpClientFactory.Current);
            containerRegistry.RegisterInstance<IConfigurationManager>(ConfigurationManager.Current);
            
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
