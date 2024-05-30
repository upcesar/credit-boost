# Credit Top-Up Service

## Overview
The Credit Top-Up Service is a backend web application designed to facilitate the recharging of prepaid mobile accounts and other top-up services. It integrates with various APIs to provide seamless and efficient top-up functionalities.

## Features
- Prepaid mobile top-up
- Gift card recharges
- Digital wallet recharges
- Transport card top-ups
- Subscription service renewals

## Technologies Used
- **ASP.NET Core WebAPI**: For building the backend application.
- **Entity Framework Core**: For database interactions.
- **RESTful APIs**: For integrating with external services.
- **SQL Server**: As the database.
- **Identity Framework**: For user authentication and authorization.
- **RabbitMQ**: For message queuing and handling asynchronous tasks.
- **MassTransit**: For abstracting RabbitMQ, simplifying message-based application development.
- **CQRS with MediatR**: For handling commands and queries in the Services Layer.
- **Dependency Injection**: For managing service lifetimes and dependencies.

## Getting Started
1. Clone the repository: `git clone https://github.com/upcesar/credit-topup.git`
2. Navigate to the project directory
3. Restore dependencies: `dotnet restore`
4. Update the database: `dotnet ef database update`
5. Run the application: `dotnet run`

## Architecture

The architecture of the Credit Top-Up Service is designed to be modular and scalable, consisting of the following components:

### Components
1. **Client Layer**: Browser or mobile app interacting with the service.
2. **API Gateway**: ASP.NET Core WebAPI application handling incoming HTTP requests.
3. **Services Layer**: Implementing business logic using CQRS with MediatR and interacting with external APIs and message queues.
4. **Database Layer**: SQL Server storing user data and transaction history.
5. **Message Queue**: RabbitMQ abstracted by MassTransit for asynchronous processing.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

## License
This project is licensed under the MIT License.
