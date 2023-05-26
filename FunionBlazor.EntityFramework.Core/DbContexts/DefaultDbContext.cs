using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;

namespace FunionBlazor.EntityFramework.Core
{
    [AppDbContext("DataStore")]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}