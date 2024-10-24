# Source-Transform-Sink Pattern with Hangfire in ASP.NET Core

## Introduction

This project demonstrates the implementation of the **Source-Transform-Sink** pattern in an ASP.NET Core application using **Hangfire** for background job scheduling.

The **Source-Transform-Sink** pattern is a common design pattern in data processing systems, where:
- **Source**: Fetches data from an external or internal source (e.g., API, database).
- **Transform**: Processes, transforms, or modifies the data according to business logic.
- **Sink**: Sends the transformed data to its final destination (e.g., console, database, file).

In this project, we implement a system that reads user data from an API (Source), processes it (Transform), and prints the data to the console (Sink).

## How the Process Works

The process follows these three steps:

1. **Source**: Fetches data from an API.
   - The `ApiSource` class is responsible for retrieving user data from an API. This can be a real API or simulated JSON data, depending on the environment.
   
2. **Transform**: Modifies the fetched data.
   - The `DataTransformer` class takes the raw JSON data and transforms it into a structured format (e.g., a list of `UserDto` objects).
   
3. **Sink**: Outputs the transformed data to the final destination.
   - The `ConsoleSink` class takes the transformed data and prints it to the console as the final step.

This entire process is orchestrated using **Hangfire**, which allows the process to run as a background job at regular intervals (e.g., every minute).

## Project Structure

- **ApiSource**: Responsible for fetching data from an external source (API).
- **DataTransformer**: Transforms raw data (JSON) into structured objects.
- **ConsoleSink**: Prints the transformed data to the console.
- **SourceTransformSinkService**: Orchestrates the whole process by combining Source, Transform, and Sink.

### Source (ApiSource)

The `ApiSource` class simulates fetching data from an external API. The class can fetch data from a real API or return hardcoded data. Here is the `ApiSource` class:

```csharp
public class ApiSource
{
    public async Task<string> FetchDataFromApiAsync(string apiUrl)
    {
        // Simulated API response
        string jsonResponse = @"
        [
            {
                'id': '1',
                'name': 'John Doe',
                'email': 'john.doe@example.com'
            },
            {
                'id': '2',
                'name': 'Jane Smith',
                'email': 'jane.smith@example.com'
            },
            {
                'id': '3',
                'name': 'Robert Brown',
                'email': 'robert.brown@example.com'
            }
        ]";

        await Task.Delay(1000); // Simulates a delay to mimic API latency
        return jsonResponse;
    }
}

Explanation:

FetchDataFromApiAsync: This method is responsible for fetching user data from an API. In this example, we simulate the API response with hardcoded JSON data.
Task.Delay(1000): This delay is added to simulate the latency of a real API request.
