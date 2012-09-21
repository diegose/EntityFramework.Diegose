using System.Data.Common;
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

        public int ExecuteNonQuery(string sql, object parameters)
        {
            var parameterFactory = Database.Connection.CreateCommand();
            return Database.ExecuteSqlCommand(sql, ConvertParameters(parameters, parameterFactory));
        }

        private static object[] ConvertParameters(object parameters, DbCommand parameterFactory)
        {
            return parameters.GetType().GetProperties()
                .Select(x =>
                            {
                                var parameter = parameterFactory.CreateParameter();
                                parameter.ParameterName = x.Name;
                                parameter.Value = x.GetValue(parameters, null);
                                return (object)parameter;
                            })
                .ToArray();
        }
    }
}