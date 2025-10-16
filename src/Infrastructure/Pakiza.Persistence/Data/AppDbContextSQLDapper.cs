
namespace Pakiza.Persistence.Data;

public class AppDbContextSQLDapper
{
    private readonly string _connectionString;

    public AppDbContextSQLDapper(IConfiguration configuration)
    {
        string dbTypeName = configuration["Database:TypeName"] ?? throw new InvalidOperationException("Database type not specified in configuration.");
        string dbConnectionType = configuration["Database:ConnectionType"] ?? throw new InvalidOperationException("Database connection type not specified in configuration.");

        string connectionKey = dbTypeName switch
        {
            "MSSQL" when dbConnectionType == "LIVE" => "SQLLiveConnectionString",
            "MSSQL" when dbConnectionType == "LOCAL" => "SQLLocalConnectionString",
            _ => throw new InvalidOperationException($"Invalid database configuration: Type '{dbTypeName}' or ConnectionType '{dbConnectionType}' is incorrect.")
        };

        _connectionString = configuration.GetConnectionString(connectionKey)
            ?? throw new InvalidOperationException($"Connection string for '{connectionKey}' not found in configuration.");
    }

    public IDbConnection CreateConnection()
    {
        if (string.IsNullOrEmpty(_connectionString))
            throw new InvalidOperationException("Connection string is not set.");
        return new SqlConnection(_connectionString);
    }
}
