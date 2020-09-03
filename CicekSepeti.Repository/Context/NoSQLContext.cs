using Common.Api;
using Microsoft.Extensions.Options;

public abstract class NoSQLContext
{
    public IConnection Connection { get; set; }
    public NoSQLContext(IConnection Connection)
    {
        this.Connection = Connection;
    }


    protected abstract void Connect(IConnection connection);
}