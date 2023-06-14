var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDo(build => build
    .Service()
);

var app = builder.Build();

app.UseDo();

app.Run();
