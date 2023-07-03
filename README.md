
# Game.Messaging

Required feature may be implemented using different approches. The most widely used are **Websockets** and **Push Notifications**. Websockets is more about real time two way communication and Push Notifications is more about delivering messages even for clients that are not currently connected. Also Push Notifications need external notifications provider like Firebase.

From task description it looks like only connected clients should receive notifications, so we can use Websockets to implement required feature.

# Implementation Considerations
- SignalR used as common library for Websockets Implementation and easy to use tool for demonstration purposes. SignalR can be used in production with AzureSignalR or just redis backplane and load balancer with sticky sessions. For AWS production environment custom implementation with WebSocket API Gateway, SNS/SQS and lambdas would be the right choice as SignalR is not currently compatible with AWS services.
- Users are randomly assigned to predefined teams (Lionsn|Bears|Crocodiles) in custom server middleware to demonstrate sending offers and events to some users only instead of broadcasting them to all users. For real feature notifications can be send to certain user based on any user characteristic or other business logic. SignalR can address user based on ClaimTypes.NameIdentifier claim. For custom implementation additional mapping of connections to users will be required.
- As we are adding events and offers via api we already know about the action and can react (send notification). If db/file new event/offer detection is still required we can add it as a background task (e.g hangfire worker). The improvement that needs to be added for real feature is saving event or offer and notification(to additional notification table) in a single transaction (for consistency reasons) and then sending saved notifications in some background process.
- Maybe using DbContext directly in commands and queries would be just enough for demonstration purposes. Specification and GenericRepository is maybe a little overcomplication but used as a personal preference :)
## Host to test
- Open solution in VS and run server.
- Add some events and offers.
- Run a few clients. All of them should read all events and offers added previously.
- Keep adding new offers and events specifying what team to send them to. Some clients should receive new offers and events based on team. Sorry you should guess to what team client belongs.


Maybe I've missed something - fill free to ask!
