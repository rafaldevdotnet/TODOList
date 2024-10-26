# TODOList API

TODOList API is a .NET 8 Minimal API application designed to manage a to-do list. This application supports typical operations such as creating, retrieving, updating, and deleting tasks, and it leverages PostgreSQL as the database for data persistence.

## Features
- CRUD operations for to-do items
- Percent completion tracking
- Filtering by date range for task due dates
- MediatR pattern and CQRS support
- PostgreSQL integration
- Docker support for simplified deployment

## Prerequisites

Before running the application, ensure you have the following installed:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/)

## Running the Application

You can run this application either using Docker or directly from the .NET CLI.

---

### 1. Running with Docker

1. **Clone the repository**:
    ```bash
    https://github.com/rafaldevdotnet/TODOList.git
    cd todolist
    ```

2. **Build and start the services**:
    In the project root directory, run the following command to start the application and PostgreSQL database using Docker Compose:

    ```bash
    docker-compose up -d --build
    ```

3. **Verify the services are running**:
    - The API will be available at: `http://localhost:5000`
    - PostgreSQL will be running on port `5432`

4. **Stopping the services**:
    To stop and remove the running containers, execute:
    ```bash
    docker-compose down
    ```

---

### 2. Running without Docker

If you prefer to run the application locally without Docker, follow these steps:

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/todolist.git
    cd todolist
    ```

2. **Configure PostgreSQL**:
    - Install PostgreSQL on your machine if it is not already installed.
    - Create a database named `todolist_db`, and set up a user with a password.
    - Update the connection string in `appsettings.json` or your environment variables with the appropriate PostgreSQL credentials:
      ```json
      "ConnectionStrings": {
          "ToDoDb": "Host=localhost;Port=5432;Database=todolist_db;Username=yourusername;Password=yourpassword"
      }
      ```

3. **Run database migrations**:
    Apply migrations to create the required database tables:
    ```bash
    dotnet ef database update --project TODOList.Infrastructure --startup-project TODOList
    ```

4. **Run the application**:
    Start the application with the following command:
    ```bash
    dotnet run --project TODOList
    ```

5. **Access the API**:
    The API will be available at `http://localhost:5000`.

---

## Endpoints

The API provides the following endpoints:

- **GET /todos**: Retrieve all to-do items.
- **GET /todo/{id}**: Retrieve a specific to-do item by ID.
- **POST /todo**: Create a new to-do item.
- **PUT /todo**: Update an existing to-do item.
- **DELETE /todo/{id}**: Delete a to-do item by ID.

Refer to the codebase for full endpoint details.

## License

This project is licensed under the MIT License.
