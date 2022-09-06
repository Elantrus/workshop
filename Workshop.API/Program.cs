using Workshop.API.IoC;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUsers(isDevelopment);
builder.Services.AddAuth(builder.Environment);

var app = builder.Build();

app.UseMigrateUsers(isDevelopment);
app.UseMiddleware<CustomExceptionMiddlerware>();

if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();