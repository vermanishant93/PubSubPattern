# PubSubPattern
	• Publish events to multiple interested subscribers asynchronously
	• Publishers and subscribers are decoupled
	• Recommended:
		○ Rabbit Mq
		○ Azure Service Bus
		○ Kafka
	• Benefits:
		○ Improves responsiveness of the Publisher
		○ Subscribers can perform better under load (messages throttled)
		○ Deferred or scheduled processing of events
		○ Potential integration benefits between disparate systems

# Commands
For Message Broker
- dotnet new webapi -minimal -n MessageBroker
- dotnet ef migrations add AddMigrations
- dotnet ef database update

For Subscriber Project
- dotnet new console -n Subscriber

If you don't have access to above commands or you want to update dot net ef tools version
- dotnet tool install --global dotnet-ef
- dotnet tool update --global dotnet-ef
