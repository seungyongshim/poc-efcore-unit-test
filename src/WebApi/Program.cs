using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendMail.EfCore;
using SendMail.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<EfCoreOptions>().BindConfiguration("EfCore");

builder.Services.AddEfCore((sp, options) =>
{
    var opt = sp.GetService<IOptions<EfCoreOptions>>().Value;

    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();

    switch (opt.DatabaseType)
    {
        case DatabaseType.Memory:
            options.UseInMemoryDatabase("SendMailDb");
            break;
        case DatabaseType.Mysql:
            break;
        case DatabaseType.SqliteMemory:
            options.UseSqlite("DataSource=:memory:");
            break;
            //options.UseSqlServer(connectionString);
    }

});

var app = builder.Build();

using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetService<SendMailContext>()
     .Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.MapGet("/Sendmail", async (SendMailContext db) =>
{
    var y = db.ServicePermissions;

    var i = new ServicePermission(Guid.NewGuid(), new ServicePermissionData (new[]
    {
        RoleType.Admin
    }));

    y.Add(i);

    await db.SaveChangesAsync();

    return new { };
});

app.Run();
