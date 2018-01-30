using System.Data.Entity;

namespace DataLibrary
{
    public class CommonContext : DbContext
    {
        public CommonContext() : base("name=DBAccount")
        {
            Database.SetInitializer<CommonContext>(null);
        }
    }
}
