using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;
using GraphQL.Server.Ui.Voyager;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPooledDbContextFactory<AppDbContext>(
                                                         opt =>
                                                             opt.UseSqlServer(builder.Configuration
                                                                                     .GetConnectionString("CommandConStr")));
builder.Services.AddGraphQLServer()
       .AddQueryType<Query>()
       .AddType<PlatformType>()
       .AddType<CommandType>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapGraphQL());
app.UseGraphQLVoyager(new VoyagerOptions
{
    GraphQLEndPoint = "/graphql",
}, "/graphql-voyager");
app.Run();