using Newtonsoft.Json;
using ReactiveUI;
using Reception.App.Models;
using Reception.App.Network;
using Reception.Model.Dto;
using Reception.Model.Network;
using RestSharp;

namespace Reception.App.ViewModels
{
    public class SubordinateViewModel : BaseViewModel
    {
        #region Fields

        private Person _person = new Person();

        #endregion

        #region ctor
        public SubordinateViewModel(IScreen screen)
        {
            UrlPathSegment = nameof(SubordinateViewModel);
            HostScreen = screen;

            var client = new RestClient("https://localhost:44329/api/VisitorInfo/1");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<QueryResult<PersonDto>>(response.Content);
                var personDto = content.Data;
                Person.FromDto(personDto);
            }
        }
        #endregion

        #region Properties

        public Person Person { get => _person; set => this.RaiseAndSetIfChanged(ref _person, value); }

        #endregion
    }
}
