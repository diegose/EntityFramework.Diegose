using System.Data.Entity;
using System.Linq;

namespace EntityFramework.Diegose.Test.Issue2
{
    public class MyContext : DiegoseContext
    {
        public IDbSet<Foo> Foos { get; set; }

        public int ExecuteNonQuery(string sql, object parameters)
        {
            var parameterFactory = Database.Connection.CreateCommand();
            return Database.ExecuteSqlCommand(sql, parameters.GetType().GetProperties()
                             .Select(x =>
                                         {
                                             var parameter = parameterFactory.CreateParameter();
                                             parameter.ParameterName = x.Name;
                                             parameter.Value = x.GetValue(parameters, null);
                                             return (object) parameter;
                                         })
                             .ToArray());
        }
    }
}