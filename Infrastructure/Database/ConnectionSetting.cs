using Npgsql;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class ConnectionSetting
    {
        public static string CreateConnectionString()
        {
            return "";
            /*var sshHost = "aqartop.com";
            var sshPort = 22;
            var sshUsername = "elsayedabdo31";
            var sshKeyPath = "C:\\Users\\engel\\.ssh\\id_rsa";

            var dbHost = "localhost";
            var dbPort = 53980;
            var dbName = "testuser";
            var dbUsername = "postgres";
            var dbPassword = "";

            var connectionInfo = new PrivateKeyConnectionInfo(sshHost, sshPort, sshUsername,
                                                               new PrivateKeyFile(sshKeyPath, passwordkey));

            using var sshClient = new SshClient(connectionInfo);
            sshClient.Connect();

            var forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)dbPort, dbHost, (uint)5432);
            sshClient.AddForwardedPort(forwardedPort);
            forwardedPort.Start();

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "127.0.0.1",
                Port = (int)forwardedPort.BoundPort,
                Database = dbName,
                Username = dbUsername,
                Password = dbPassword
            };

            return connectionStringBuilder.ConnectionString;*/
        }

    }
}
