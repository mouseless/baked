var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDo(c => c
    .Service()
);

var app = builder.Build();

app.UseDo();

app.Run();
