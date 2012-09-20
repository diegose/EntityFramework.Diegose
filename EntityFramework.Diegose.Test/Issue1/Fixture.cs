using System.Data.Entity.Validation;
using System.Linq;
using NUnit.Framework;

namespace EntityFramework.Diegose.Test.Issue1
{
    [TestFixture]
    public class WhenARequiredReferenceIsNotLoaded
    {
        [TestFixtureSetUp]
        public void CreateData()
        {
            using (var context = new StandardContext())
            {
                context.Database.Delete();
                context.Database.Create();
                context.Foos.Add(new Foo { Bar = new Bar() });
                context.SaveChanges();
            }
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            using (var context = new StandardContext())
            {
                context.Database.Delete();
            }
        }

        [Test]
        public void DbContextSaveChangesFails()
        {
            using (var context = new StandardContext())
            {
                var foo = context.Foos.Single();
                foo.FooData = 1;
                Assert.That(() => context.SaveChanges(), Throws.InstanceOf<DbEntityValidationException>());
            }
        }
    }
}