var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
//para guardar en memoria 30 minutos
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tiempo de inactividad permitido
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

var app = builder.Build();
/*
if (app.Environment.IsDevelopment())
{
    // GET: /dev/bcrypt?password=admin123
    app.MapGet("/dev/bcrypt", (string password) =>
        BCrypt.Net.BCrypt.HashPassword(password));
}*/

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Principal}/{action=Index}/{id?}");

app.Run();
