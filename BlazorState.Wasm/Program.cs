using BlazorState.ViewModel;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorState.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<IHealthModel, HealthModel>();
            await builder.Build().RunAsync();
        }
    }
}
