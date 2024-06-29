using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LibraryOfBooks.Web;
using LibraryOfBooks.Web.Services;
using LibraryOfBooks.Web.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddScoped<AuthMessageHandler>();

builder.Services.AddHttpClient("AuthAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7237/");
})
.AddHttpMessageHandler<AuthMessageHandler>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<BookService>(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = clientFactory.CreateClient("AuthAPI");
    return new BookService(client);
});

builder.Services.AddScoped<BookCategoryService>(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = clientFactory.CreateClient("AuthAPI");
    return new BookCategoryService(client);
});

builder.Services.AddScoped<IAuthService>(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = clientFactory.CreateClient("AuthAPI");
    var config = sp.GetRequiredService<IConfiguration>();
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    var authProvider = sp.GetRequiredService<AuthenticationStateProvider>();
    return new AuthService(client, config, localStorage, authProvider);
});

builder.Services.AddScoped<IUserService>(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = clientFactory.CreateClient("AuthAPI");
    return new UserService(client);
});

await builder.Build().RunAsync();
