using System.Configuration;
using System.Data;
using System.Data.Entity;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace ChineseDictionary.Resources.Configuration
{
    public class MySqlConfiguration : DbConfiguration
    {
        public MySqlConfiguration()
        {
            var dataSet = (DataSet) ConfigurationManager.GetSection("system.data");
            var row = dataSet.Tables[0].Rows.Find("MySql.Data.MySqlClient");
            dataSet.Tables[0].Rows.Remove(row);
            dataSet.Tables[0].Rows.Add(
                "MySQL Data Provider",
                ".Net Framework Data Provider for MySQL",
                "MySql.Data.MySqlClient",
                typeof(MySqlClientFactory).AssemblyQualifiedName
            );

            // Register Entity Framework provider
            SetProviderServices("MySql.Data.MySqlClient", new MySqlProviderServices());
            SetDefaultConnectionFactory(new MySqlConnectionFactory());

        }
    }
}
