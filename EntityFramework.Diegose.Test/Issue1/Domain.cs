using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Diegose.Test.Issue1
{
    public class Foo
    {
        public virtual int Id { get; set; }
        public virtual int FooData { get; set; }
        [Required]
        public virtual Bar Bar { get; set; }
    }

    public class Bar
    {
        public virtual int Id { get; set; }
        public virtual int BarData { get; set; }
    }
}