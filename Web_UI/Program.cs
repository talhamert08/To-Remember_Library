using Core.Entities;
using Entity.Concrete;
using Web_UI.API_Access_Manager;
using Web_UI.API_SERVÝCE;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ApiAccessManager>(opt =>
{

    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});

/////////////////////////////////////////////////////////////////// TEST
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Businesse paket indirmeye gerek kalmýyor
builder.Services.AddScoped<BookApiManager>();
builder.Services.AddScoped<AuthApiManager>();
builder.Services.AddScoped<ApiManager>();
//////
builder.Services.AddSession(option =>
{
    //Süre 1 dk olarak belirlendi
    option.IdleTimeout = TimeSpan.FromMinutes(7); //Verilen süre içerisinde eðer bir aksiyon olmazsa sessiondaki token datasý yok olur sebebi kullanýcýnýn
                                                  //ekran baþýnda gitmesi halinde onun adýna baþka kiþilerin iþlem yapmasýný engellemek sessiondaki bilgi
                                                  //her kullanýldýðýnda bu süre sýfýrlanýyor yani 1 dakika kala istek yapýlýrsa sayaç tekrar baþlýyor
});
///////////////////////////////////////////////////////////////////

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

///////
app.UseSession();
///////

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
