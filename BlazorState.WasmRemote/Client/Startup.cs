using BlazorState.Shared;
using BlazorState.ViewModel;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorState.WasmRemote.Client
{
    public class StateServiceConfig : IStateServiceConfig
    {
        public string Url => "http://localhost:49935/State";
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHealthModel, HealthModel>();
            services.AddSingleton<IStateServiceConfig, StateServiceConfig>();
            services.AddSingleton<StateService, StateService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
