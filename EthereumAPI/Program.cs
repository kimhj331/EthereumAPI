using EthereumAPI.Contracts;
using EthereumAPI.Logger;
using EthereumAPI.Scheduler;
using EthereumAPI.Services;
using EthereumAPI.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//�α�
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
//���񽺵��
builder.Services.AddScoped<IEthereumClient, EthereumClient>();
//��׶��� ����
builder.Services.AddSingleton<IHostedService, GetNewBlocks>();
//jsonRe
builder.Services.AddControllers(jsonOptions => jsonOptions.ReturnHttpNotAcceptable = true)
               .AddNewtonsoftJson(jsonOptions =>
               {
                   jsonOptions.SerializerSettings.ContractResolver = new OriginalPropertyContractResolver();
                   jsonOptions.SerializerSettings.Converters.Add((JsonConverter)new StringEnumConverter());
                   jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               });


builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
