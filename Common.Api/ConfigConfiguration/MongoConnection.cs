using System;

namespace Common.Api
{
    public class MongoConnection : IConnection
    {
        public string Database { get; set; }
    }
}
