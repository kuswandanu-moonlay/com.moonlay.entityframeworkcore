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
            builder.SetSoftDeleteFilter();
        }

        private static IEnumerable<Type> GetDerivedClass(this Assembly assembly, Type baseClass)
        {
            if (baseClass.IsInterface)
                return assembly.DefinedTypes.Where(x => x.GetInterfaces().Contains(baseClass));
            else
                return assembly.DefinedTypes.Where(x => !x.IsAbstract && x.IsSubclassOf(baseClass));
        }

        private static void DefineKeys(this Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder builder)
        {
            var keyProperties = builder.Metadata.ClrType.GetProperties().Where(p => p.GetCustomAttribute(typeof(KeyAttribute)) != null).Select(o=>o.Name);
            if (keyProperties.Count() > 0)
            {
                if (!keyProperties.Contains("Id"))
                    builder.Ignore("Id");

                builder.HasKey(keyProperties.ToArray());
            }
            else
                builder.HasKey("Id");
            
        }

        private static void AddModelBuilder(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var entities = assembly.GetDerivedClass(typeof(IEntity)).Select(Activator.CreateInstance);
            
            foreach (var config in entities)
            {
                var builder = modelBuilder.Entity(config.GetType());

                builder.DefineKeys();

                if (config is IEntity<string>)
                {
                    builder.Property("Id").HasMaxLength(32).HasValueGenerator<StringPKeyGenerator>();
                }
                else if (config is IEntity<Guid>)
                {
                    builder.Property("Id").HasValueGenerator<GuidPKeyGenerator>(); 
                }

                if (config is IAuditEntity)
                {
                    builder.Property("_LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    builder.Property("_LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    builder.Property("_CreatedBy")
                       .IsRequired()
                       .HasMaxLength(255);

                    builder.Property("_CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);
                }

                if (config is ISoftEntity)
                {
                    builder.Property("_DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    builder.Property("_DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);
                }

            }
        }

        private static void SetSoftDeleteFilter(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "p");
                    var member = Expression.Property(parameter, "_IsDeleted");
                    var constant = Expression.Constant(false);
                    var filterHidden = Expression.Equal(member, constant);
                    entityType.QueryFilter = Expression.Lambda(filterHidden, parameter);
                }
            }
        }
    }
}
