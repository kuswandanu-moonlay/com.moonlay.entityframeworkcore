using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace Com.Moonlay.EntityFrameworkCore
{

    public static class EntityConfigExtension
    {
        public static void ConfigAllEntities(this ModelBuilder builder, Assembly assembly)
        {
            builder.AddModelBuilder(assembly);
        }

        private static IEnumerable<Type> GetDerivedClass(this Assembly assembly, Type baseType)
        {
            if (baseType.IsInterface)
                return assembly.DefinedTypes.Where(x => x.GetInterfaces().Contains(baseType));
            else
                return assembly.DefinedTypes.Where(x => !x.IsAbstract && x.IsSubclassOf(baseType));
        }

        private static void DefineKeys(this Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder builder, object entity)
        {
            var keyProperties = builder.Metadata.ClrType.GetProperties().Where(p => p.GetCustomAttribute(typeof(KeyAttribute)) != null).Select(p => p.Name);
            if (keyProperties.Count() > 0)
            {
                builder.HasKey(keyProperties.ToArray());
            }
            else
            {
                builder.HasKey("Id");

                if (entity is IEntity<string>)
                {
                    builder.Property("Id").HasMaxLength(32).HasValueGenerator<StringPKeyGenerator>();
                }
                else if (entity is IEntity<Guid>)
                {
                    builder.Property("Id").HasValueGenerator<GuidPKeyGenerator>();
                }
            }
        }

        private static void AddModelBuilder(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var entities = assembly.GetDerivedClass(typeof(IEntity)).Select(Activator.CreateInstance);

            foreach (var entity in entities)
            {
                var builder = modelBuilder.Entity(entity.GetType());

                builder.DefineKeys(entity);

                if (entity is IAuditEntity)
                {
                    builder.Property("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    builder.Property("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    builder.Property("CreatedBy")
                       .IsRequired()
                       .HasMaxLength(255);

                    builder.Property("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);
                }

                if (entity is ISoftEntity)
                {
                    var parameter = Expression.Parameter(builder.Metadata.ClrType, "IsDeleted");
                    var member = Expression.Property(parameter, "IsDeleted");
                    var constant = Expression.Constant(false);
                    var filterDeleted = Expression.Equal(member, constant);

                    builder.HasQueryFilter(Expression.Lambda(filterDeleted, parameter));

                    builder.Property("DeletedBy")                        
                        .IsRequired()                        
                        .HasMaxLength(255);

                    builder.Property("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);
                }

            }
        }
    }
}
