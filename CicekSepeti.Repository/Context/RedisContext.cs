using CicekSepeti.Repository;
using Common.Api;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

public class RedisContext :NoSQLContext
    {

    ConnectionMultiplexer connectionMultiplexer;

    public RedisContext(IConnection Connection) : base(Connection)
    {
        Connect(Connection);
    }
    public IDatabase GetDb() => connectionMultiplexer.GetDatabase();

    protected override void Connect(IConnection connection) => connectionMultiplexer = ConnectionMultiplexer.Connect(connection.ConnectionString);

}
