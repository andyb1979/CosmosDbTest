# CosmosDbTest
Sandbox for playing around with cosmos DB on Windows

## Setup instructions

1. Download the [CosmosDB Emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=windows%2Ccsharp&pivots=api-nosql#install-the-emulator)
2. Ensure the emulator is running by calling `"C:\Program Files\Azure Cosmos DB Emulator\Microsoft.Azure.Cosmos.Emulator.exe"` in command prompt
3. Ensure the emulator can be seen by going to https://localhost:8081/_explorer/index.html in browser. 
4. Run the application

### Starting the emulator

If not started already, you can start the emulator with `"C:\Program Files\Azure Cosmos DB Emulator\Microsoft.Azure.Cosmos.Emulator.exe"` in command prompt

### Stopping the emulator

If you need to stop the emulator, use `"C:\Program Files\Azure Cosmos DB Emulator\Microsoft.Azure.Cosmos.Emulator.exe" /Shutdown`

## Errors and their fixes

> InternalServerError (500); Substatus: 0; ActivityId: db79c8d3-1970-45d7-b6b2-77102860559c; Reas
> on: (Service is currently unavailable, please retry after a while. If this problem persists please contact support.

This error can be resolved by manually trusting the TLS/SSL certificate. 

Run this in command:

```shell
"C:\Program Files\Azure Cosmos DB Emulator\Microsoft.Azure.Cosmos.Emulator.exe" /installcert
```

> https://localhost:8081/_explorer/index.html is "Page cannot be found"

If this occurs, navigate in browser to https://127.0.0.1:8081/_explorer/index.html. This will force IPv4 mode. After this localhost should work
