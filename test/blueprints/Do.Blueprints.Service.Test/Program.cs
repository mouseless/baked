var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDo(with => with.Service());

var app = builder.Build();
app.UseDo();
app.Run();
