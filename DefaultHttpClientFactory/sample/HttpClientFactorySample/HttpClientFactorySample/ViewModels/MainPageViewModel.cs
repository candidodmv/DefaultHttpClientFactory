using HttpClientFactorySample.Services;
using Plugin.DefaultHttpClientFactory;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpClientFactorySample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IGithubApiService _githubApiService;

        public MainPageViewModel(INavigationService navigationService, IGithubApiService githubApiService)
            : base(navigationService)
        {
            Title = "Main Page";
            _githubApiService = githubApiService;
        }
    }
}
