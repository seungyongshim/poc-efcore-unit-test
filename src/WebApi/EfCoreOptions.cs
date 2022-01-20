namespace SendMail.WebApi;

public class EfCoreOptions
{
    public DatabaseType DatabaseType { get; init; }
    public string ConnectionString { get; init; }
}
