using Newtonsoft.Json;
using ReactiveUI;
using Reception.App.Models;
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
        }
        #endregion

        #region Properties

        public Person Person { get => _person; set => this.RaiseAndSetIfChanged(ref _person, value); }

        #endregion
    }
}




//var client = new RestClient("https://localhost:44329/api/Person/1");
//var request = new RestRequest(Method.GET);
//IRestResponse response = client.Execute(request);
//            if (response.IsSuccessful)
//            {
//                var content = JsonConvert.DeserializeObject<QueryResult<Person>>(response.Content);
//var person = content.Data;
//Person = new Person(person);
//            }