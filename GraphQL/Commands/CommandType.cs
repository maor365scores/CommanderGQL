using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Commands;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Field(command => command.Platform)
                  .ResolveWith<Resolvers>(p => p.GetPlatform(default!, default!))
                  .UseDbContext<AppDbContext>();
    }

    private class Resolvers
    {
        public Platform GetPlatform(Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
        }
    }
}