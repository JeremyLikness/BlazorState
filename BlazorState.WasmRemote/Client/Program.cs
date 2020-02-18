using BlazorState.Shared;
using BlazorState.ViewModel;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorState.WasmRemote.Client
{
    public class Program
    {
        public class StateServiceConfig : IStateServiceConfig
        {
            public string Url => "http://localhost:49935/State";
        }
        
        public static async Task Main(string[] _)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<IHealthModel, HealthModel>();
            builder.Services.AddSingleton<IStateServiceConfig, StateServiceConfig>();
            builder.Services.AddSingleton<StateService, StateService>();
            await builder.Build().RunAsync();
        }        
    }
}
