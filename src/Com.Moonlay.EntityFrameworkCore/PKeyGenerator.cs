using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Moonlay.EntityFrameworkCore
{
    public class StringPKeyGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;
        public override string Next(EntityEntry entry)
        {
            return Guid.NewGuid().ToString("N");
        }
    }

    public class GuidPKeyGenerator : ValueGenerator<Guid>
    {
        public override bool GeneratesTemporaryValues => false;

        public override Guid Next(EntityEntry entry)
        {
            return Guid.NewGuid();
        }
    }
}
