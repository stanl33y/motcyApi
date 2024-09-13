# Motorcycle and Delivery Management API

This project is a **Motorcycle and Delivery Management API** built using **.NET 8**, **PostgreSQL**, **RabbitMQ**, and **Docker**. The API provides endpoints for managing motorcycles, delivery personnel, and rental services, along with Swagger documentation for testing and exploring the API.

## Project Overview

The API allows administrators to:
- Register new motorcycles.
- Update and delete motorcycle records.
- Register new delivery personnel and manage their records.
- Rent motorcycles to delivery personnel for specified periods.
- Handle returns of rented motorcycles, including calculating any applicable penalties.

The API is documented using **Swagger**, which allows users to explore and interact with the API endpoints easily.

## Requirements

Before running the project, ensure you have the following installed:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Setup and Running the Project

### 1. Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/stanl33y/motorcycle-management-api.git
cd motorcycle-management-api
```

### 2. Rename the `.env.example` File

The project includes an `.env.example` file that contains placeholder values for environment variables. You need to rename this file to `.env`:

```bash
mv .env.example .env
```

### 3. Edit the `.env` File

Open the `.env` file and modify the environment variables if needed. These variables include the PostgreSQL database credentials and the connection string used by the API.

Here’s an example:

```bash
# Application environment
ASPNETCORE_ENVIRONMENT=Development

# PostgreSQL configuration
POSTGRES_USER=postgres
POSTGRES_PASSWORD=mypassword
POSTGRES_DB=mydatabase

# Connection string for the API to use PostgreSQL
CONNECTION_STRING=Host=postgresdb;Port=5432;Database=mydatabase;Username=postgres;Password=mypassword

# RabbitMQ configuration
RABBITMQ_HOSTNAME=rabbitmq
RABBITMQ_QUEUE_NAME=motorcycle_queue
RABBITMQ_DEFAULT_USER=myuser
RABBITMQ_DEFAULT_PASS=mypassword
```

Make sure to replace the `myuser`, `mypassword` and `mydatabase` placeholders with the actual values you'd like to use.

### 4. Run the Project with Docker Compose

Now that the `.env` file is set up, you can use **Docker Compose** to build and run the project.

Run the following command to build the Docker images and start the containers:

```bash
docker-compose up --build
```

This command will:
- Build the API image from the Dockerfile.
- Start the API service on port **5000**.
- Start the PostgreSQL service on port **5432**.
- Start the RabbitMQ service on port **5672** and manager in **15672**.

### 5. Access the API

Once the containers are running, you can access the API at:

```
http://localhost:5000
```

### 6. Access the Swagger Documentation

The API is documented using **Swagger**. You can view and interact with the API documentation by visiting:

```
http://localhost:5000/swagger
```

From here, you can explore the API endpoints, send requests, and view responses directly from the Swagger UI.

### 7. Stop the Containers

To stop the running containers, use the following command:

```bash
docker-compose down
```

This will stop all running services and remove the containers.

## Configuration Notes

### Fixed API Port

The API is configured to always run on port **5000** on your host machine, which is mapped to port **80** inside the container. This ensures that the API is always accessible on a predictable port.

## Additional Commands

- **Rebuild the containers**: If you make changes to the Dockerfile or dependencies, you can force a rebuild with:

  ```bash
  docker-compose up --build
  ```

- **View logs**: To view the logs from the running containers:

  ```bash
  docker-compose logs -f
  ```

This will display real-time logs from both the API and PostgreSQL services.

## Manual docker required commands to run the services

### RabbitMQ
```bash 
docker run -d --name rabbitmq -e RABBITMQ_DEFAULT_USER=rabbitmq -e RABBITMQ_DEFAULT_PASS=pass -p 5672:5672 -p 15672:15672 
```

### Postgres
```bash
docker run --name postgresdb -e POSTGRES_PASSWORD=pass -e POSTGRES_USER=postgres -e POSTGRES_DB=motcy -p 5555:5432 -d postgres  rabbitmq:management
```
