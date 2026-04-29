using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
 var sdf = Directory.GetCurrentDirectory();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile("AllDroDown.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
// Add services to the container.
builder.Services.AddCors(p => p.AddPolicy("Gateway", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddRazorPages();
var app = builder.Build();
app.UseCors("Gateway");
app.Map("/", () => "Hello Word");
app.MapControllers();
await app.UseOcelot();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.Run();
