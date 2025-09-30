using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(config => 
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"));
});
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHangfireDashboard("/dashbord");

app.MapControllers();
app.Run();
