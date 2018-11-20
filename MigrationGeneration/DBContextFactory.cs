using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SmallAssistant.DBModels;

namespace MigrationGeneration
{
    public class DBContextFactory : IDesignTimeDbContextFactory<SmallAssistantDBContext>
    {
        public SmallAssistantDBContext CreateDbContext(string[] args)
        {
            return SmallAssistantDBContext.GetContext();
        }
    }
}