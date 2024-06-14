using System.ComponentModel.DataAnnotations;
using dotNetTesting.Payments.Model;

namespace dotNetTesting.Payments.DataAccess;

public class EntityRow
{
    [MaxLength(100)] public string Id { get; set; }
    
    [MaxLength(100)] public string Name { get; set; }
 
    [MaxLength(100)] public string Types { get; set; }

    public EntityRow(string id, string name, string types)
    {
        Id = id;
        Name = name;
        Types = types;
    }
}