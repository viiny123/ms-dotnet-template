using Template.Domain.Base;

namespace Template.Data.Base;

public class UnitOfWork : IUnitOfWork
{
    private readonly TemplateDbContext context;

    public UnitOfWork(TemplateDbContext context)
    {
        this.context = context;
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
