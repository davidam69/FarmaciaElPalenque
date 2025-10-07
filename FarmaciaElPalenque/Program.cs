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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Principal}/{action=Index}/{id?}");

app.Run();

// Registra el servicio de envio de correos
builder.Services.AddScoped<IEmailSender, EmailSenderService>();
// Registra el servicio en segundo plano que ejecuta la IA
builder.Services.AddHostedService<ServicioAnalisisClientes>();