using EVDMS.Api.Configure;
using EVDMS.BusinessLogicLayer.Configure;
using EVDMS.BusinessLogicLayer.Dto.Response;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(
        new RouteTokenTransformerConvention(
            new KebabCaseParameterTransformer()
        )
    );
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<HidePagingParametersOperationFilter>();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token as: Bearer {your token here}",
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });

});
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureAuthentication(builder.Configuration).ConfigureAuthorization();
builder.Services.AddDataAccessLayer_Wrap(builder.Configuration).AddBusinessLogicLayer().AddOptions(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.Services.CreateScope().AddSeedData();
}
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    if (response.StatusCode == StatusCodes.Status404NotFound)
    {
        response.ContentType = "application/json";

        var result = Response.Failed("The requested resource was not found.");

        await response.WriteAsJsonAsync(result);
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
