﻿using System.Data.Entity;

namespace EntityFramework.Diegose.Test.Issue2
{
    public class MyContext : DiegoseContext
    {
        public IDbSet<Foo> Foos { get; set; }
    }
}