#ASP.NET CORE project study from TEDU
##Technologies
- ASP.NET Core 3.1
- Entity Framwork Core 3.1
##Install package
- microsoft.entityframeworkcore.design\3.1.1\
- microsoft.entityframeworkcore.sqlserver\3.1.1\
- microsoft.entityframeworkcore.tools\3.1.1\
- microsoft.extensions.configuration.fileextensions\3.1.1\ (SetBasePath())
- Microsoft.Extensions.Configuration.Json (for AddJsonFile())
##Configure entity:
- attribute configuration
- Fluent API configuration
- enter: Add-Migration Initial in package manager console to build a migration
##Note: deatail research in website Learn Entity Framework core
-Create Configuration for each entity to have table: builder.toTable(""), builder.Properies(x=>x.property),.....
-Add config into OnModelCreating in EShopDBContext
-https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings