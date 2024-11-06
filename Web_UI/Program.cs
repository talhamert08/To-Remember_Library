using Core.Entities;
using Entity.Concrete;
using Web_UI.API_Access_Manager;
using Web_UI.API_SERV�CE;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ApiAccessManager>(opt =>
{

    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});

/////////////////////////////////////////////////////////////////// TEST
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Businesse paket indirmeye gerek kalm�yor
builder.Services.AddScoped<BookApiManager>();
builder.Services.AddScoped<AuthApiManager>();
builder.Services.AddScoped<ApiManager>();
//////
builder.Services.AddSession(option =>
{
    //S�re 1 dk olarak belirlendi
    option.IdleTimeout = TimeSpan.FromMinutes(7); //Verilen s�re i�erisinde e�er bir aksiyon olmazsa sessiondaki token datas� yok olur sebebi kullan�c�n�n
                                                  //ekran ba��nda gitmesi halinde onun ad�na ba�ka ki�ilerin i�lem yapmas�n� engellemek sessiondaki bilgi
                                                  //her kullan�ld���nda bu s�re s�f�rlan�yor yani 1 dakika kala istek yap�l�rsa saya� tekrar ba�l�yor
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
