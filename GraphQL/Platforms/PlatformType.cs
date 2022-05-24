using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Platforms;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Nice description");

        descriptor
            .Field(platform => platform.LicenseKey)
            .Ignore();

        descriptor.Field(p => p.Commands)
                  .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                  .UseDbContext<AppDbContext>();
    }

    private class Resolvers
    {
        public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
        {
            return context.Commands.Where(command => command.PlatformId == platform.Id);
        }
    }
}