# events-aggregation-system

Reqirements:
Windows, VisualStudio 2022, .net 8 SDK, .netFramework 4.8 SDK

How to run the project:

- Download and install
    - latest Erlang from https://www.erlang.org/downloads
    - latest RabbitMq service from https://www.rabbitmq.com/docs/install-windows

- Enable Management plugin from this documentation - https://www.rabbitmq.com/docs/management
- Login at http://localhost:15672/#/ and upload new broker definitions using the file - rabbit_definitions.json from this repo

- Install windows service
    - Open src/All.sln in VisualStudio and build it.
    - Open cmd.exe as Administrator, navigate to this repo folder and execute InstallWindowsService.bat
    - Locate the new service in Services.msc and start it.

- Web api may be launched with F5 in VisualStudio in dedug mode, Or by executing - dotnet run in src\EventsWebService folder

Api is explorable at - http://localhost:5083/swagger/index.html
Authentication key is in the configuration file.

- Basic functionality of the system:

Web api is accepting different type of events.
When event is received it is pushed to the rabbitMQ queue.
The windows service is listening for messages in the queue and when a message is received it than stores the data in a local database file - \src\EventsProcessWindowsService\eventsdb.db

There is second queue - eventsQueue.tests - which also receives the same messages, but they are not consumed, it is to be used in tests for validations of the features in the web api.