# Zetatech.Accelerate.Exceptions
## Table of contents
- [Overview](#overview)
- [BusinessException](#businessexception)
- [ConfigurationException](#configurationexception)
- [ConflictException](#conflictexception)
- [DependencyException](#dependencyexception)
- [ForbiddenException](#forbiddenexception)
- [NotFoundException](#notfoundexception)
- [UnauthorizedException](#unauthorizedexception)
- [UnavailableException](#unavailableexception)
- [ValidationException](#validationexception)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the exceptions catalog.  
## BusinessException
Represents errors that occur during application execution related to business logic violations.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class BusinessException : Exception
```
### Constructors
| Name                                         | Description                                                                                                                                                          |
|:---------------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| BusinessException()                          | Initializes a new instance of the class.                                                                                                                             |
| BusinessException(String)                    | Initializes a new instance of the class with a specified error message.                                                                                              |
| BusinessException(String, String)            | Initializes a new instance of the class with a specified error message and business rule.                                                                            |
| BusinessException(String, Exception)         | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception.                   |
| BusinessException(String, Exception, String) | Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and a business rule. |
### Properties
| Name         | Type   | Description                                            |
|:-------------|:-------|:-------------------------------------------------------|
| BusinessRule | String | Gets the business rule that was violated, if provided. |
## ConfigurationException
Represents errors that occur when there is a configuration issue during application execution.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class ConfigurationException : Exception
```
### Constructors
| Name                                              | Description                                                                                                                                                             |
|:--------------------------------------------------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ConfigurationException()                          | Initializes a new instance of the class.                                                                                                                                |
| ConfigurationException(String)                    | Initializes a new instance of the class with a specified error message.                                                                                                 |
| ConfigurationException(String, String)            | Initializes a new instance of the class with a specified error message and parameter name.                                                                              |
| ConfigurationException(String, Exception)         | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception.                      |
| ConfigurationException(String, Exception, String) | Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and the parameter name. |
### Properties
| Name          | Type   | Description                                                                          |
|:--------------|:-------|:-------------------------------------------------------------------------------------|
| ParameterName | String | Gets the name of the configuration parameter that caused the exception, if provided. |
## ConflictException
Represents errors that occur when a conflict is detected during application execution.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class ConflictException : Exception
```
### Constructors
| Name                                 | Description                                                                                                                                        |
|:-------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| ConflictException()                  | Initializes a new instance of the class.                                                                                                           |
| ConflictException(String)            | Initializes a new instance of the class with a specified error message.                                                                            |
| ConflictException(String, Exception) | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception. |
## DependencyException
Represents errors that occur when a dependency fails or is unavailable during application execution.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class DependencyException : Exception
```
### Constructors
| Name                                           | Description                                                                                                                                                              |
|:-----------------------------------------------|:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| DependencyException()                          | Initializes a new instance of the class.                                                                                                                                 |
| DependencyException(String)                    | Initializes a new instance of the class with a specified error message.                                                                                                  |
| DependencyException(String, String)            | Initializes a new instance of the class with a specified error message and dependency name.                                                                              |
| DependencyException(String, Exception)         | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception.                       |
| DependencyException(String, Exception, String) | Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and the dependency name. |
### Properties
| Name           | Type   | Description                                                             |
|:---------------|:-------|:------------------------------------------------------------------------|
| DependencyName | String | Gets the name of the dependency that caused the exception, if provided. |
## ForbiddenException
Represents errors that occur when an operation is forbidden due to insufficient permissions.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class ForbiddenException : Exception
```
### Constructors
| Name                                  | Description                                                                                                                                        |
|:--------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| ForbiddenException()                  | Initializes a new instance of the class.                                                                                                           |
| ForbiddenException(String)            | Initializes a new instance of the class with a specified error message.                                                                            |
| ForbiddenException(String, Exception) | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception. |
## NotFoundException
Represents errors that occur when a requested resource is not found during application execution.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class NotFoundException : Exception
```
### Constructors
| Name                                 | Description                                                                                                                                        |
|:-------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| NotFoundException()                  | Initializes a new instance of the class.                                                                                                           |
| NotFoundException(String)            | Initializes a new instance of the class with a specified error message.                                                                            |
| NotFoundException(String, Exception) | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception. |
## UnauthorizedException
Represents errors that occur when an operation is attempted without proper authorization.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class UnauthorizedException : Exception
```
### Constructors
| Name                                     | Description                                                                                                                                        |
|:-----------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| UnauthorizedException()                  | Initializes a new instance of the class.                                                                                                           |
| UnauthorizedException(String)            | Initializes a new instance of the class with a specified error message.                                                                            |
| UnauthorizedException(String, Exception) | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception. |
## UnavailableException
Represents errors that occur when a required resource or service is unavailable during application execution.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class UnavailableException : Exception
```
### Constructors
| Name                                    | Description                                                                                                                                        |
|:----------------------------------------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| UnavailableException()                  | Initializes a new instance of the class.                                                                                                           |
| UnavailableException(String)            | Initializes a new instance of the class with a specified error message.                                                                            |
| UnavailableException(String, Exception) | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception. |
## ValidationException
Represents errors that occur when validation of input data fails during application execution.  
**Assembly:** Zetatech.Accelerate.Exceptions.dll  
**Namespace**: Zetatech.Accelerate  
```csharp
public class ValidationException : Exception
```
### Constructors
| Name                                           | Description                                                                                                                                                             |
|:-----------------------------------------------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ValidationException()                          | Initializes a new instance of the class.                                                                                                                                |
| ValidationException(String)                    | Initializes a new instance of the class with a specified error message.                                                                                                 |
| ValidationException(String, String)            | Initializes a new instance of the class with a specified error message and parameter name.                                                                              |
| ValidationException(String, Exception)         | Initializes a new instance of the class with a specified error message and a reference to the inner exception that is the cause of this exception.                      |
| ValidationException(String, Exception, String) | Initializes a new instance of the class with a specified error message, a reference to the inner exception that is the cause of this exception, and the parameter name. |
### Properties
| Name          | Type   | Description                                                         |
|:--------------|:-------|:--------------------------------------------------------------------|
| ParameterName | String | Gets the name of the parameter that failed validation, if provided. |
### Feedback & Contributing
Zetatech.Accelerate.Exceptions is released as open source under the [GNU General Public License](./License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes an exception for errors that occur during application execution related to business logic violations.
- Includes an exception for errors that occur when there is a configuration issue during application execution.
- Includes an exception for errors that occur when a conflict is detected during application execution.
- Includes an exception for errors that occur when a dependency fails or is unavailable during application execution.
- Includes an exception for errors that occur when an operation is forbidden due to insufficient permissions.
- Includes an exception for errors that occur when a requested resource is not found during application execution.
- Includes an exception for errors that occur when an operation is attempted without proper authorization.
- Includes an exception for errors that occur when a required resource or service is unavailable during application execution.
- Includes an exception for errors that occur when validation of input data fails during application execution.

```
Zeta Technologies
```