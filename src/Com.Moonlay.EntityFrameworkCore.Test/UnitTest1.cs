using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using Test;
using Xunit;

namespace Com.Moonlay.EntityFrameworkCore.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            new ModelBuilder(new ConventionSet()).ConfigAllEntities(typeof(UnitDbContext).Assembly);

        }
    }
}
