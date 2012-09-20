using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using NUnit.Framework;

namespace EntityFramework.Diegose.Test.Issue1
{
    public abstract class WhenARequiredReferenceIsNotLoaded<TContext> where TContext : DbContext, new()
    {
        [TestFixtureSetUp]
        public void CreateData()
        {
            using (var context = CreateContext())
            {
                context.Database.Delete();
                context.Database.Create();
                context.Set<Foo>().Add(new Foo { Bar = new Bar() });
                context.SaveChanges();
            }
        }

        protected TContext CreateContext()
        {
            return new TContext();
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            using (var context = CreateContext())
            {
                context.Database.Delete();
            }
        }
    }

    [TestFixture]
    public class WhenARequiredReferenceIsNotLoadedWithStandardContext : WhenARequiredReferenceIsNotLoaded<StandardContext>
    {
        [Test]
        public void DbContextSaveChangesFails()
        {
            using (var context = CreateContext())
            {
                var foo = context.Set<Foo>().Single();
                foo.FooData = 1;
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }
    }

    [TestFixture]
    public class WhenARequiredReferenceIsNotLoadedWithMyContext : WhenARequiredReferenceIsNotLoaded<MyContext>
    {
        [Test]
        public void DbContextSaveChangesSucceeds()
        {
            using (var context = CreateContext())
            {
                var foo = context.Set<Foo>().Single();
                foo.FooData = 1;
                context.SaveChanges();
            }
        }
    }
}