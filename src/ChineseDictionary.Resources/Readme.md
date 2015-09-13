Note: You will have to run migrations with a seperate project. Just create a temporary one in ASP.NET 4 and move the models, DictionaryContext to it. 
Add the connection string with the MySQL provider to the web.config
Add	SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator()); to Configuration
