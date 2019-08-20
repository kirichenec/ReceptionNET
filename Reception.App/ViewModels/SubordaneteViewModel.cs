using Newtonsoft.Json;
using ReactiveUI;
using Reception.App.Models;
using Reception.Model.Network;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields

        private Person _person = new Person();
        private string _searchText;

        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            SearchPersonCommand = ReactiveCommand.Create<string>(SearchPerson);
        }
        #endregion

        #region Properties

        public Person Person { get => _person; set => this.RaiseAndSetIfChanged(ref _person, value); }

        public string SearchText { get => _searchText; set => this.RaiseAndSetIfChanged(ref _searchText, value); }

        #endregion

        #region Commands
        public ReactiveCommand<string, Unit> SearchPersonCommand { get; set; }
        #endregion

        #region Methods
        private void SearchPerson(string searchText)
        {
            var client = new RestClient($"{AppSettings.ServerPath}/api/Person?searchText={searchText}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<List<Person>>>(response.Content);
                if (content.ErrorCode == ErrorCode.Ok)
                {
                    var persons = content.Data;
                    Person.CopyFrom(persons?.FirstOrDefault() ?? new Person());
                }
            }
        }
        #endregion
    }
}