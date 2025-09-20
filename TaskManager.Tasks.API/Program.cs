using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskManager.Common.Infrastructure.Http;
using TaskManager.Common.Persistence.EF.FrameworkDbContext;
using TaskManager.Tasks.API.BackgroundServices;
using TaskManager.Tasks.Application.CQRS.Commands.CreateTask;
using TaskManager.Tasks.Application.CQRS.Commands.UpdateTaskStatus;
using TaskManager.Tasks.Application.Interfaces.Persistence;
using TaskManager.Tasks.Persistence.EF.Contexts;
using TaskManager.Tasks.Persistence.EF.Repositories;
using TaskManager.Tasks.Query.Contract.IServices;
using TaskManager.Tasks.Query.Queries.GetTasks;
using TaskManager.Tasks.Query.Services;
using TaskManager.Common.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("HistoryService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5211/"); // آدرس سرویس HistoryService
})
.AddHttpMessageHandler<HttpLoggingHandler>();


builder.Services.AddTransient<HttpLoggingHandler>();

builder.Services.AddHostedService<EventDispatcher>();

builder.Services.AddMediatR(cfg =>
{ 
    cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(ChangeTaskStatusCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetTasksQueryHandler).Assembly);
});

#region Write Repo
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
#endregion

#region Reead Repo
builder.Services.AddScoped<ITaskQueryService, TaskQueryService>();
#endregion

AddTaskDbContext(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRequestResponseLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddTaskDbContext(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<FrameworkContext, TaskCommandContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnectionString"));

        if (builder.Environment.IsProduction()) return;

        options.LogTo(Console.WriteLine);
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.ConfigureWarnings(warningLog =>
        {
            warningLog.Log(CoreEventId.FirstWithoutOrderByAndFilterWarning,
                CoreEventId.RowLimitingOperationWithoutOrderByWarning);
        });
    });

    builder.Services.AddDbContext<TaskQueryContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnectionString"),
            sqlOptions => { sqlOptions.MigrationsAssembly(typeof(TaskQueryContext).Assembly.FullName); }
        );
    });
}

