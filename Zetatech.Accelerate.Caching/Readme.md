# Zetatech.Accelerate.Caching
## Table of contents
- [Overview](#overview)
- [Caching](#caching)
  - [MemoryCachingService](#memorycachingservice)
  - [MemoryCachingServiceOptions](#memorycachingserviceoptions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the custom components for caching capabilities.  
## Caching
### MemoryCachingService
Represents an implementation for a custom memory-based cache service implementation.  
**Assembly:** Zetatech.Accelerate.Caching.dll  
**Namespace**: Zetatech.Accelerate.Caching.Services  
```csharp
public sealed class MemoryCachingService : BaseCacheService<MemoryCachingServiceOptions>
```
#### Constructors
| Name                           | Description                              |
|:-------------------------------|:-----------------------------------------|
| MemoryCachingService(IOptions) | Initializes a new instance of the class. |
#### Properties
| Name          | Type                        | Description                                              |
|:--------------|:----------------------------|:---------------------------------------------------------|
| Logger        | ILogger                     | Gets the instance of the logger.                         |
| LoggerFactory | ILoggerFactory              | Gets or sets the factory to create instances of loggers. |
| Options       | MemoryCachingServiceOptions | Gets the options for the messages publisher.             |
#### Methods
| Name                                  | Description                                                           |
|:--------------------------------------|:----------------------------------------------------------------------|
| Add<TValue>(String, TValue)           | Adds a value to the cache with the specified key.                     |
| Add<TValue>(String, TValue, DateTime) | Adds a value to the cache with the specified key and expiration date. |
| Clear()                               | Removes all entries from the cache.                                   |
| Contains(String)                      | Determines whether the cache contains a value with the specified key. |
| Get<TValue>(String)                   | Retrieves the value associated with the specified key from the cache. |
| Remove(String)                        | Removes the value with the specified key from the cache.              |
#### Example
```csharp
var options = Options.Create(new MemoryCachingServiceOptions
{
    DefaultExpirationTime = 10,
    MaxSize = 100
});
var cacheService = new MemoryCachingService(options);
cacheService.Add("key1", "value1");
var value = cacheService.Get<string>("key1");
```
### MemoryCachingServiceOptions
Represents the options for configuring the memory-based caching service.  
**Assembly:** Zetatech.Accelerate.Caching.dll  
**Namespace**: Zetatech.Accelerate.Caching.Services  
```csharp
public sealed class MemoryCachingServiceOptions : BaseCacheServiceOptions
```
#### Properties
| Name                  | Type  | Description |
|:----------------------|:------|:------------|
| DefaultExpirationTime | Int32 | Gets or sets the default expiration time (in minutes) for cache entries. |
| MaxSize               | Int32 | Gets or sets the maximum number of entries allowed in the cache. |
## Feedback & Contributing
Zetatech.Accelerate.Caching is released as open source under the [GNU General Public License](../License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes a custom memory-based caching service.

```
Zeta Technologies
```