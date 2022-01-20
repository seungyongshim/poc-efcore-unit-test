namespace SendMail.EfCore;

public readonly record struct ApiKey(Guid Value)
{
    public static implicit operator ApiKey(Guid guid) => new(guid);
}
