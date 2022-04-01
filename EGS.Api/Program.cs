
var builder = WebApplication.CreateBuilder(args);

EGS.Application.Setup.Configure(builder.Services, builder.Configuration);
EGS.Infrastructure.Setup.Configure(builder.Services, builder.Configuration);
EGS.Api.Setup.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();

