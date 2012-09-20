using System.Data.Entity;

namespace EntityFramework.Diegose.Test.Issue1
{
    public class StandardContext : DbContext
    {
        public IDbSet<Foo> Foos { get; set; }
    }
}