using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Heilsunudd.Data.Data;
using Heilsunudd.Data.Data.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heilsunudd.Intranet.Controllers
{
    public class MetadataController(HeilsunuddDbContext context) : ControllerBase
    {
        
        public static List<string> GetModelNames()
        {
            var modelNames = typeof(HeilsunuddDbContext).GetProperties().Where(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).Select(p => p.Name).ToList();
            
            return modelNames;
        }
        public IActionResult GetControllers()
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => 
                    !type.IsAbstract && 
                    (typeof(Controller).IsAssignableFrom(type) || 
                     typeof(ControllerBase).IsAssignableFrom(type)) &&
                    type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var controllerInfo = controllerTypes.Select(type => new
            {
                Name = type.Name.Replace("Controller", ""),
                FullName = type.FullName,
                Actions = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Select(m => m.Name)
                    .ToList()
            }).ToList();

            return Ok(controllerInfo);
        }


        public IActionResult GetDbEntities()
        {
            var dbSetProperties = typeof(HeilsunuddDbContext)
                .GetProperties()
                .Where(p => p.PropertyType.IsGenericType && 
                           p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            var entityInfo = dbSetProperties.Select(prop => new
            {
                Name = prop.Name,
                EntityType = prop.PropertyType.GetGenericArguments()[0].Name,
                Properties = prop.PropertyType.GetGenericArguments()[0]
                    .GetProperties()
                    .Select(p => new
                    {
                        Name = p.Name,
                        Type = p.PropertyType.Name
                    })
                    .ToList()
            }).ToList();

            return Ok(entityInfo);
        }


        public IActionResult GetDbSchema()
        {
            try
            {
                var tables = new List<object>();
                
                foreach (var entityType in context.Model.GetEntityTypes())
                {
                    var tableName = entityType.GetTableName();
                    var properties = entityType.GetProperties()
                        .Select(p => new
                        {
                            Name = p.Name,
                            Type = p.ClrType.Name,
                            IsKey = p.IsKey(),
                            IsForeignKey = p.IsForeignKey()
                        })
                        .ToList();
                    
                    var navigationProperties = entityType.GetNavigations()
                        .Select(n => new
                        {
                            Name = n.Name,
                            TargetEntity = n.TargetEntityType.Name,
                            IsCollection = n.IsCollection
                        })
                        .ToList();
                    
                    tables.Add(new
                    {
                        TableName = tableName,
                        EntityName = entityType.Name,
                        Properties = properties,
                        NavigationProperties = navigationProperties
                    });
                }
                
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}