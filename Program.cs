using io.harness.cfsdk.client.api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Harness feature flag client as a singleton
builder.Services.AddSingleton<ICfClient>(provider => {
    var apiKey = builder.Configuration.GetValue<string>("HarnessKey");
    var client = new CfClient(apiKey, Config.Builder().Build());
    client.InitializeAndWait().Wait();
    return client;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



//Target target = io.harness.cfsdk.client.dto.Target.builder()
//                .Name("User1") //can change with your target name
//                .Identifier("user1@example.com") //can change with your target identifier
//                .build();
