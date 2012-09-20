using System.Data.Entity;

namespace EntityFramework.Diegose.Test.Issue1
{
    public class MyContext : DiegoseContext
    {
        public IDbSet<Foo> Foos { get; set; }
    }
}