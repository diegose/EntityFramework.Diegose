using System.Linq;
using NUnit.Framework;

namespace EntityFramework.Diegose.Test.Issue2
{
    public class ExecutingAParameterizedSQL
    {
        [TestFixtureSetUp]
        public void CreateData()
        {
            using (var context = new MyContext())
            {
                context.Database.Delete();
                context.Database.Create();
                context.Set<Foo>().Add(new Foo { Bar = 1, Baz = 2 });
                context.Set<Foo>().Add(new Foo { Bar = 2, Baz = 1 });
                context.Set<Foo>().Add(new Foo { Bar = 1, Baz = 2 });
                context.SaveChanges();
            }
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            using (var context = new MyContext())
            {
                context.Database.Delete();
            }
        }

        [Test]
        public void PassesParametersCorrectly()
        {
            using (var context = new MyContext())
            {
                var recordsAffected = context.ExecuteNonQuery("delete Foos where Bar = @Bar and Baz = @Baz",
                                                              new {Baz = 1, Bar = 2});
                Assert.AreEqual(1, recordsAffected);
            }
            using (var context = new MyContext())
            {
                Assert.False(context.Foos.Any(x => x.Bar == 2 && x.Baz == 1));
            }
        }
    }
}