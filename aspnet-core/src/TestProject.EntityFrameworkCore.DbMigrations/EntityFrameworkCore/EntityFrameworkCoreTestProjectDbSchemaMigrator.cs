using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.Data;
using Volo.Abp.DependencyInjection;

namespace TestProject.EntityFrameworkCore
{
    [Dependency(ReplaceServices = true)]
    public class EntityFrameworkCoreTestProjectDbSchemaMigrator 
        : ITestProjectDbSchemaMigrator, ITransientDependency
    {
        private readonly TestProjectMigrationsDbContext _dbContext;

        public EntityFrameworkCoreTestProjectDbSchemaMigrator(TestProjectMigrationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}