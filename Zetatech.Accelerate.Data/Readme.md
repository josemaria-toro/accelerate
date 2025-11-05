# Zetatech.Accelerate.Data Documentation
## Table of contents
- [Overview](#overview)
- [Data](#data)
  - [PostgreSqlRepository](#postgresqlrepository)
  - [PostgreSqlRepositoryContext](#postgresqlrepositorycontext)
  - [PostgreSqlRepositoryOptions](#postgresqlrepositoryoptions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the custom components for data access components.  
### PostgreSqlRepository
Represents an implementation for a custom PostgreSQL-based repository.  
**Assembly:** Zetatech.Accelerate.Data.dll  
**Namespace**: Zetatech.Accelerate.Data.Repositories  
```csharp
public abstract class PostgreSqlRepository<TEntity, TOptions> : BaseRepository<TEntity, TOptions, PostgreSqlRepositoryContext<TEntity, TOptions>> where TEntity : BaseEntity
                                                                                                                                                  where TOptions : PostgreSqlRepositoryOptions
```
#### Constructors
| Name                                           | Description                              |
|:-----------------------------------------------|:-----------------------------------------|
| PostgreSqlRepository(IOptions, ILoggerFactory) | Initializes a new instance of the class. |
#### Properties
| Name          | Type           | Description                                              |
|:--------------|:---------------|:---------------------------------------------------------|
| Entities      | DbSet          | Gets the set of entities managed by this repository.     |
| Logger        | ILogger        | Gets the instance of the logger.                         |
| Options       | TOptions       | Gets the configuration options of this repository.       |
#### Methods
| Name                               | Description                                       |
|:-----------------------------------|:--------------------------------------------------|
| Select(String, IDbDataParameter[]) | Execute a custom query string to select entities. |
### PostgreSqlRepositoryContext
Represents an implementation for a custom PostgreSQL-based repository context.  
**Assembly:** Zetatech.Accelerate.Data.dll  
**Namespace**: Zetatech.Accelerate.Data.Repositories  
```csharp
public sealed class PostgreSqlRepositoryContext<TEntity, TOptions> : BaseRepositoryContext<TEntity, TOptions> where TEntity : BaseEntity
                                                                                                              where TOptions : PostgreSqlRepositoryOptions
```
#### Constructors
| Name                                               | Description                              |
|:---------------------------------------------------|:-----------------------------------------|
| PostgreSqlRepositoryContext(IRepository, TOptions) | Initializes a new instance of the class. |
#### Properties
| Name    | Type     | Description                                                |
|:--------|:---------|:-----------------------------------------------------------|
| Options | TOptions | Gets the configuration options of this repository context. |
#### Methods
| Name                                   | Description                                                 |
|:---------------------------------------|:------------------------------------------------------------|
| OnConfiguring(DbContextOptionsBuilder) | Configures the database and other options for this context. |
### PostgreSqlRepositoryOptions
Represents the options for configuring the PostgreSQL-based repositories.  
**Assembly:** Zetatech.Accelerate.Data.dll  
**Namespace**: Zetatech.Accelerate.Data.Repositories  
```csharp
public class PostgreSqlRepositoryOptions : BaseRepositoryOptions
```
#### Constructors
| Name                          | Description                              |
|:------------------------------|:-----------------------------------------|
| PostgreSqlRepositoryOptions() | Initializes a new instance of the class. |
#### Properties
| Name                 | Type    | Description                                                                                     |
|:---------------------|:--------|:------------------------------------------------------------------------------------------------|
| ConnectionString     | String  | Gets or sets the connection string used to connect with the data source.                        |
| DetailedErrors       | Boolean | Gets or sets a value indicating whether detailed error messages are enabled for the repository. |
| LazyLoading          | Boolean | Gets or sets a value indicating whether lazy loading is enabled for related entities.           |
| Schema               | String  | Gets or sets the schema where database is stored.                                               |
| SensitiveDataLogging | Boolean | Gets or sets a value indicating whether sensitive data logging is enabled for the repository.   |
| Timeout              | Int32   | Gets or sets the timeout value, in seconds, for repository operations.                          |
| TrackChanges         | Boolean | Gets or sets a value indicating whether change tracking is enabled for entities.                |
#### Example
```csharp
public class MyEntity : BaseEntity { }
public class MyOptions : PostgreSqlRepositoryOptions { }
public class MyRepository : PostgreSqlRepository<MyEntity, MyOptions>
{
    public MyRepository(IOptions<MyOptions> options) : base(options) { }
}

var myOptions = new MyOptions
{
    ConnectionString = "Host=localhost;Database=mydb;Username=user;Password=pass",
    Timeout = 30,
    LazyLoading = true,
    TrackChanges = true
};
var options = Options.Create(myOptions);
var myRepository = new MyRepository(options);
var entities = myRepository.Select("SELECT * FROM MyEntities");
```
## Feedback & Contributing
Zetatech.Accelerate.Data is released as open source under the [GNU General Public License](./License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes a custom PostgreSQL-based data access repository.

```
Zeta Technologies
```