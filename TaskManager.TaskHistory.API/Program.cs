using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskManager.Common.Persistence.EF.FrameworkDbContext;
using TaskManager.TaskHistory.Application.EventHandlers;
using TaskManager.TaskHistory.Application.Interfaces.Persistence;
using TaskManager.TaskHistory.Persistence.EF.Contexts;
using TaskManager.TaskHistory.Persistence.EF.Repositories;
using TaskManager.TaskHistory.Query.Contract.IServices;
using TaskManager.TaskHistory.Query.Queries.GetTaskHistoriesByTaskId;
using TaskManager.TaskHistory.Query.Services;
using TaskManager.Common.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetTaskHistoryByTaskIdQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(TaskStatusChangedEventHandler).Assembly);
});

builder.Services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();

builder.Services.AddScoped<ITaskHistoryQueryService, TaskHistoryQueryService>();

AddTaskHistoiryDbContext(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
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

void AddTaskHistoiryDbContext(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<FrameworkContext, TaskHistoryCommandContext>(options =>
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

    builder.Services.AddDbContext<TaskHistoryQueryContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("TestConnectionString"),
            sqlOptions => { sqlOptions.MigrationsAssembly(typeof(TaskHistoryQueryContext).Assembly.FullName); }
        );
    });
}