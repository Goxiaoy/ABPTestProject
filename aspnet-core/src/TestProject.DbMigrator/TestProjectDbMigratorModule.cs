using TestProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace TestProject.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(TestProjectEntityFrameworkCoreDbMigrationsModule),
        typeof(TestProjectApplicationContractsModule)
        )]
    public class TestProjectDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<BackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
