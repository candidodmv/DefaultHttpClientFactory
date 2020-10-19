using HttpClientFactorySample.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientFactorySample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IMarvelApiService _marvelApiService;

        public MainPageViewModel(INavigationService navigationService, IMarvelApiService marvelApiService)
            : base(navigationService)
        {
            Title = "Main Page";
            _marvelApiService = marvelApiService;
        }

        private DelegateCommand _invokeMarvelApiCommand;
        public DelegateCommand InvokeMarvelApiCommand =>
            _invokeMarvelApiCommand ?? (_invokeMarvelApiCommand = new DelegateCommand(async () => await ExecuteInvokeMarvelApiCommandAsync()));

        async Task ExecuteInvokeMarvelApiCommandAsync()
        {
            var items = await _marvelApiService.ApiClient.CharactersGetAsync(null, null, null, null, null, null, null, null, null, null);
            if (items.Data != null)
                Console.WriteLine("Has Data");
        }
    }
}
