# BlazorState

Examples of how to manage state in Blazor WebAssembly and Blazor Server apps.

[![Free Azure](https://camo.githubusercontent.com/943435dedf206fce581c5363ba8946078394e2c8/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f465245452d417a7572652d303037376666)](https://azure.microsoft.com/free/?WT.mc_id=blazorstate-github-jeliknes)
Get your [free Azure account](https://azure.microsoft.com/free/?WT.mc_id=blazorstate-github-jeliknes)

This project is intended to illustrate how to better manage state in Blazor applications. It is based on the official documentation: [ASP.NET Core Blazor state management](https://docs.microsoft.com/aspnet/core/blazor/state-management?view=aspnetcore-3.1&WT.mc_id=blazorstate-github-jeliknes).

To get started, fork the repo (optional) then clone it.

## Stating the Problem

The project contains several variations of a Blazor app for tracking health statistics. (For the background behind the sample app, read: [From Angular to Blazor: The Health App](https://blog.jeremylikness.com/blog/2019-01-03_from-angular-to-blazor-the-health-app/)). This version follows a layered architecture:

* The `BlazorState.ViewModel` project can be shared across .NET Core apps including Xamarin and WPF.
* The `BlazorState.Shared` Razor class library contains views, styles, and JavaScript code that can be shared between Blazor WebAssembly and Blazor Server projects. If you're not familiar with the difference, read [ASP.NET Core Blazor hosting models](https://docs.microsoft.com/aspnet/core/blazor/hosting-models?view=aspnetcore-3.1&WT.mc_id=blazorstate-github-jeliknes).
* The other projects use the shared code.

Set the `BlazorState.Wasm` project as the startup project or navigate to the folder if you are running from the command line. Launch the app. In the first page, modify the inputs to see how BMI, BMR, and target heart rate change.

Next, instead of using the built-in navigation, change the URL to `/results` and refresh. Notice you lose the state you entered.

Try the same thing with `BlazorState.Server`. For this demo, you can "break the circuit" by stopping the web server. The app will show disconnected. Restart the web server, and the app will refresh but it will lose any state.

## Solution 1: Preserve State with Local Storage

Launch `BlazorState.WasmLocal` (the local is for "_local_ storage") and try the same exercise as before. This time, you should see the state is preserved. You can view the local storage for the app and see the serialized view model. In fact, you can close the browser and reopen it to see the state continues to persist.

Run `BlazorState.ServerLocal`. This demonstrates the same code for tracking local storage works for the server hosting model as well. It will pre-render with defaults, then re-render with the state.

A few notes:

* The text is stored "in the clear." For sensitive information, encryption should be added, for example by adding [ASP.NET Core Data Protection](https://docs.microsoft.com/aspnet/core/security/data-protection/introduction?view=aspnetcore-3.1&WT.mc_id=blazorstate-github-jeliknes).
* The relevant code is contained in the `StorageHelper.razor` view. This view wraps the routing (`App.razor`) in both client and server projects. This enables it to plug into the overall app lifecycle.
* After it is initialized, it attempts to deserialize the state from local storage via JavaScript interop (the simple operations are in `wwwroot\stateManagement.js`). The operation is wrapped in a `try...catch` block because storage isn't available when pre-rendering in Blazor Server. The code will be fired a second time once the rendering is complete.
* Property change notification is used to save state when the model changes.
* This example uses `localStorage` which means state will be preserved across tabs. To keep it scoped to a single tab, so each tabbed instance has its own copy of state, use `sessionStorage` instead.

## Solution 2: Preserve State with Server Storage

For this example, launch the server side of the `BlazorState.WasmRemote` project. This stands up an API to simulate storing state in a database server and also hosts a Blazor WebAssembly project. The same approach will work for Blazor Server.

The API contains an in-memory dictionary that stores state by IP address. This is meant as an illustration only. In production a more likely scenario would be to have the user authenticate then store state in a database with the user as the key.

For this example, a service called `StateService` is registered. This relies on an instance of `IStateServiceConfig` to determine what the API endpoint is. The solution has this hard-coded as a local class in `Startup.cs`. In production this would be a dynamic class that pulls from standard configuration. If the example isn't working, ensure the configuration class has the proper port number.

The service takes a dependency on the view model and is able to immediately register for property change notification. Because it is making HTTP calls to an API, it will work on the server or the client and doesn't require local storage availability. It posts an updated state to the server any time a property changes and exposes an `InitAsync` method to retrieve the state. It acts like middleware and starts working after it is registered, but the main pages `Index.razor` and `Results.razor` make a call when initialized to set up initial state.

I prefer to run this from the command line (`dotnet run` from the `BlazorState.WasmRemote\Server` folder) so I can show the serialization calls come in as I update the form.

## Conclusion

The goal of this project is to illustrate good practices for preserving state in Blazor apps. It demonstrates an approach to using shared code to maximize reuse across project types. It also takes an approach of using property change notification and service registration to minimize code changes necessary to implement the state management. As always, your feedback is welcome and appreciated.

[![Jeremy Likness](https://blog.jeremylikness.com/images/jeremylikness.gif)](https://twitter.com/JeremyLikness)