using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Test;
using UnitTest.Models;
using Xunit;

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

        [Fact(DisplayName ="Filter SoftDelete")]
        public async Task Test1()
        {
            var context = new UnitDbContext();
            context.Database.Migrate();

            var set = context.Set<TestEntity>();
            var code = GetUniqueKey(10);
            var identity = GetUniqueKey(12);

            var entity = new TestEntity { Code = code, Identity= identity };
            entity.FlagForCreate("system", "unittest");
            entity.FlagForUpdate("system", "unittest");
            entity.FlagForDelete("system", "unittest");
            set.Add(entity);
            context.SaveChanges();

            var record = await set.FirstOrDefaultAsync(o=>o.Code == code);
            Assert.Equal(null, record);
        }

        [Fact(DisplayName ="Builder Entity Types")]
        public void Test2()
        {
            new ModelBuilder(new ConventionSet { }).ConfigAllEntities(typeof(UnitDbContext).Assembly);
        }
    }
}
