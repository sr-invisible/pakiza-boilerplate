using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakiza.Persistence.Data;

public class DbContextHelper
{
    private readonly AppDbContext _context;

    public DbContextHelper(AppDbContext context)
    {
        _context = context;
    }

    public string GetTableNameForEntity<T>()
    {
        var entityType = _context.Model.FindEntityType(typeof(T));
        return entityType!.GetTableName()!;
    }
}
