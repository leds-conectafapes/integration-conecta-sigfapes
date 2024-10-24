# Source-Transform-Sink Pattern with Hangfire in ASP.NET Core

## Introduction

This project demonstrates the implementation of the **Source-Transform-Sink** pattern in an ASP.NET Core application using **Hangfire** for background job scheduling.

The **Source-Transform-Sink** pattern is a common design pattern in data processing systems, where:
- **Source**: Fetches data from an external or internal source (e.g., API, database).
- **Transform**: Processes, transforms, or modifies the data according to business logic.
- **Sink**: Sends the transformed data to its final destination (e.g., console, database, file).
- **Process**: a process that uses sources, transformations, and sink to load data from a Data Source and save on data repository (Sink).

In this project, we implement a system that reads user data from an API (Source), processes it (Transform), and prints the data to the console (Sink).

## How the Process Works

The process follows these three steps:

1. **Source**: Fetches data from an API.
   - The `UserSource` class is responsible for retrieving user data from an API. This can be a real API or simulated JSON data, depending on the environment.
   
2. **Transform**: Modifies the fetched data.
   - The `UserTransformer` class takes the raw JSON data and transforms it into a structured format (e.g., a list of `UserDto` objects).
   
3. **Sink**: Outputs the transformed data to the final destination.
   - The `ConsoleSink` class takes the transformed data and prints it to the console as the final step.

This entire process is orchestrated using **Hangfire**, which allows the process to run as a background job at regular intervals (e.g., every minute).

## Project Structure

- **UserSource**: Responsible for fetching data from an external source (API).
- **UserTransformer**: Transforms raw data (JSON) into structured objects.
- **ConsoleSink**: Prints the transformed data to the console.
- **ProcessFromAPIToConsole**: Orchestrates the whole process by combining Source, Transform, and Sink.

## Running the Application

To run this application locally:

1. Clone the repository and navigate to the project directory.
2. Install Hangfire dependencies by running `dotnet restore`.
3. Start the application by running `dotnet run`.
4. The **Hangfire Dashboard** will be available at [http://localhost:5000/](http://localhost:5000/).
5. Hangfire will execute the **Source-Transform-Sink** process every minute.

## Conclusion

This project demonstrates how to implement the **Source-Transform-Sink** pattern in an ASP.NET Core application. It also shows how to use **Hangfire** for background job scheduling to automate the process of fetching, transforming, and processing data.

