using BlazorState.ViewModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorState.Shared
{
    public class StateService
    {
        private IHealthModel _model;
        private bool _initializing;
        private HttpClient _client;
        private IStateServiceConfig _config;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IgnoreReadOnlyProperties = true,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,            
        };

        public StateService(
            IHealthModel model,
            IStateServiceConfig config,
            HttpClient client)
        {
            _model = model;
            _config = config;
            _client = client;
            _model.PropertyChanged += Model_PropertyChanged;
        }
        
        public async Task InitAsync()
        {
            _initializing = true;
            var vmJson = await _client.GetStringAsync(_config.Url);
            var vm = JsonSerializer.Deserialize<HealthModel>(vmJson, _options);
            _model.AgeYears = vm.AgeYears;
            _model.HeightInches = vm.HeightInches;
            _model.IsFemale = vm.IsFemale;
            _model.IsMetric = vm.IsMetric;
            _model.WeightPounds = vm.WeightPounds;
            _initializing = false;
        }

        private async void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_initializing || _config == null)
            {
                return;
            }
            var vm = JsonSerializer.Serialize(_model);
            var content = new StringContent(vm);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await _client.PostAsync(_config.Url, content);
        }
    }
}