using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Com.Moonlay.EntityFrameworkCore.Test;
using Com.Moonlay.EntityFrameworkCore.Test.Models;
using Xunit;
using System.Collections.Generic;

namespace Com.Moonlay.EntityFrameworkCore.Test
{
    public class UnitTest1
    {
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        [Fact(DisplayName = "StandardCompositeEntity: CRUD Test")]
        public async Task TestStandardCompositeEntityCrud()
        {
            var context = new UnitDbContext();
            var StandardCompositeSet = context.StandardCompositeEntities;

            var code = GetUniqueKey(10);
            var identity = GetUniqueKey(12);

            var entity = new StandardCompositeEntity { Code = code, Identity = identity };
            entity.FlagForCreate("system", "unittest");
            StandardCompositeSet.Add(entity);
            context.SaveChanges();

            entity.FlagForUpdate("system", "unittest");
            context.SaveChanges();

            entity.FlagForDelete("system", "unittest");
            context.SaveChanges();

            var record = await context.StandardCompositeEntities.FirstOrDefaultAsync(e => e.Code == code && e.Identity == identity);

            var recordFind = await context.StandardCompositeEntities.FindAsync(code, identity);
            Assert.Equal(null, record);
        }

        [Fact(DisplayName = "StandardEntity: CRUD Test")]
        public async Task TestStandardEntityCrud()
        {
            var context = new UnitDbContext();
            var StandardCompositeSet = context.StandardEntities;

            var code = GetUniqueKey(10);
            var stamp = DateTime.UtcNow;

            var entity = new Models.StandardEntity { Code = code, Stamp = stamp };

            entity.FlagForCreate("system", "unittest");
            StandardCompositeSet.Add(entity);
            context.SaveChanges();

            entity.FlagForUpdate("system", "unittest");
            context.SaveChanges();

            entity.FlagForDelete("system", "unittest");
            context.SaveChanges();

            var record = await context.StandardCompositeEntities.FirstOrDefaultAsync(e => e.Id == entity.Id);
            Assert.Equal(null, record);
        }

        [Fact(DisplayName = "Builder Entity Types")]
        public void Test2()
        {
            new ModelBuilder(new ConventionSet { }).ConfigAllEntities(typeof(UnitDbContext).Assembly);
        }
    }
}
