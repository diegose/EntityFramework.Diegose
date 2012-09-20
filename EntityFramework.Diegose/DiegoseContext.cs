using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace EntityFramework.Diegose
{
    public class DiegoseContext : DbContext
    {
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var property in from result in ex.EntityValidationErrors
                                         from error in result.ValidationErrors
                                         select new { error.PropertyName, result.Entry })
                {
                    try { property.Entry.Reference(property.PropertyName).Load(); }
                    catch { }
                }
                return base.SaveChanges();
            }
        }
    }
}