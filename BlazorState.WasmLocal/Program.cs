using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BlazorState.ViewModel;

namespace BlazorState.WasmLocal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton<IHealthModel, HealthModel>();
            await builder.Build().RunAsync();
        }
    }
}
