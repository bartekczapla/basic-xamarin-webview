using BasicXamarin.Contract;
using BasicXamarin.Contract.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicXamarin.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IRepository repository;
        public MainPageViewModel(INavigationService navigationService, IRepository repository)
            : base(navigationService)
        {
            Title = "Main Page";
            this.repository = repository;
        }

        public async override void Initialize(INavigationParameters parameters) {
            base.Initialize(parameters);

            var tests = await repository.GetManyAsync<Test>();
        }

    }
}
