# Zetatech.Accelerate.Messaging
## Table of contents
- [Overview](#overview)
- [Messaging](#messaging)
  - [RabbitMqPublisherService](#rabbitmqpublisherservice)
  - [RabbitMqPublisherServiceOptions](#rabbitmqpublisherserviceoptions)
  - [RabbitMqSubscriberService](#rabbitmqsubscriberservice)
  - [RabbitMqSubscriberServiceOptions](#rabbitmqsubscriberserviceoptions)
- [Feedback & Contributing](#feedback--contributing)
- [Changelog](#changelog)
  - [v8.0.0](#v800)
## Overview
This library provides the custom components for messaging capabilities.  
## Messaging
### RabbitMqPublisherService
Represents an implementation for a custom RabbitMQ-based message publisher.  
**Assembly:** Zetatech.Accelerate.Messaging.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public abstract class RabbitMqPublisherService<TMessage, TOptions> : BasePublisherService<TMessage, TOptions> where TMessage : BaseMessage
                                                                                                              where TOptions : RabbitMqPublisherServiceOptions
```
#### Constructors
| Name                                               | Description                              |
|:---------------------------------------------------|:-----------------------------------------|
| RabbitMqPublisherService(IOptions, ILoggerFactory) | Initializes a new instance of the class. |
### RabbitMqPublisherServiceOptions
Represents the options for configuring RabbitMQ-based message publisher.  
**Assembly:** Zetatech.Accelerate.Messaging.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public class RabbitMqPublisherServiceOptions : BasePublisherServiceOptions
```
#### Properties
| Name             | Type   | Description                                              |
|:-----------------|:-------|:---------------------------------------------------------|
| Exchange         | String | Gets or sets the exchange name.                          |
| ConnectionString | String | Gets or sets the connection string with RabbitMQ server. |
| RoutingKey       | String | Gets or sets the routing key.                            |
### RabbitMqSubscriberService
Represents an implementation for a custom RabbitMQ-based message subscriber.  
**Assembly:** Zetatech.Accelerate.Messaging.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public abstract class RabbitMqSubscriberService<TMessage, TOptions> : BaseSubscriberService<TMessage, TOptions>, IAsyncBasicConsumer where TMessage : BaseMessage where TOptions : RabbitMqSubscriberServiceOptions
```
#### Constructors
| Name                                                | Description                              |
|:----------------------------------------------------|:-----------------------------------------|
| RabbitMqSubscriberService(IOptions, ILoggerFactory) | Initializes a new instance of the class. |
### RabbitMqSubscriberServiceOptions
Represents the options for configuring RabbitMQ-based message subscriber.  
**Assembly:** Zetatech.Accelerate.Messaging.dll  
**Namespace**: Zetatech.Accelerate.Messaging  
```csharp
public class RabbitMqSubscriberServiceOptions : BaseSubscriberServiceOptions
```
#### Properties
| Name             | Type   | Description                                              |
|:-----------------|:-------|:---------------------------------------------------------|
| ConnectionString | String | Gets or sets the connection string with RabbitMQ server. |
| Queue            | String | Gets or sets the name of the queue.                      |
## Feedback & Contributing
Zetatech.Accelerate.Messaging is released as open source under the [GNU General Public License](../License.txt).  
Bug reports and contributions are welcome at the [GitHub repository](https://github.com/josemaria-toro/accelerate.git).  
## Changelog
### v8.0.0
- Includes a custom RabbitMQ-based message subscribers.
- Includes a custom RabbitMQ-based message publisher.

```
Zeta Technologies
```