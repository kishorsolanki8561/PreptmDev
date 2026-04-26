using DBInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace TranslationMicroService
{
    public class DbContextFactory : DesignTimeDbContextFactoryBase<DBPreptmContext>
    {
        protected override DBPreptmContext CreateNewInstance(DbContextOptions<DBPreptmContext> options)
        {
            return new DBPreptmContext(options);
        }
    }
}
