using TodoTask.GeneralKernel.Database.Abstracts;
using TodoTask.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// // Заполнение базы данных тестовыми данными
// using (var scope = app.Services.CreateScope())
// {
//     var seeders = scope.ServiceProvider.GetServices<ISeeder>();
//
//     foreach (var seeder in seeders)
//     {
//         await seeder.SeedAsync(CancellationToken.None);
//     }
// }

app.ConfigurePipeline();
app.Run();
