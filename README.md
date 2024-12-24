# E-commerce API / Insta-Shop API

This is an E-commerce API built with **ASP.NET Core** and **Entity Framework** for managing products, users, orders, and related entities in an online store environment. The API supports basic CRUD operations for products and orders, authentication for users, and interaction with an SQL Server database for persistent storage.

## Features

- User authentication (JWT-based)
- Product management (add, edit, delete, view)
- Order management (create, update, view orders)
- Order item management
- Swagger API documentation
- Entity Framework with SQL Server for data storage

## Technologies Used

- **ASP.NET Core**: Backend framework for building the API
- **Entity Framework Core**: ORM for database interaction
- **SQL Server**: Database for storing entities
- **Swagger**: API documentation for easy testing
- **JWT (JSON Web Tokens)**: For secure authentication
- **C#**: Primary language used

## Getting Started

### Prerequisites

To run this project locally, ensure you have the following tools installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or above but I used version 8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/) (for testing API endpoints)

### Setup

1. **Clone the Repository**

   Clone the repository to your local machine:

   ```bash
   git clone https://github.com/PlayzAe/Insta-Shop.git
   ```

2. **Install Dependencies**

   Navigate to the project directory and restore the required dependencies:

   ```bash
   cd ECommerceAPI
   dotnet restore
   ```

3. **Configure the Database**

   - Update the `appsettings.json` file with your database connection string:
   
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=ECommerceDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```
   
   - Use **SQL Server Management Studio (SSMS)** or a migration command to create the database schema.

4. **Run the Application**

   To start the application:

   ```bash
   dotnet run
   ```

   The API will be accessible at `https://localhost:5001` or `http://localhost:5000` depending on your configuration.

5. **Database Migration (if necessary)**

   If this is your first time setting up the database, run the following migration command to create the tables:

   ```bash
   dotnet ef database update
   ```

### API Endpoints

The following are the key API endpoints for the application:

- **User Authentication:**
  - `POST /api/auth/login` - Login with username and password (returns JWT)
  - `POST /api/auth/register` - Register a new user

- **Products:**
  - `GET /api/products` - Get all products
  - `GET /api/products/{id}` - Get product by ID
  - `POST /api/products` - Add a new product
  - `PUT /api/products/{id}` - Update an existing product
  - `DELETE /api/products/{id}` - Delete a product

- **Orders:**
  - `GET /api/orders` - Get all orders
  - `GET /api/orders/{id}` - Get order by ID
  - `POST /api/orders` - Create a new order

- **Swagger UI:**
  - Open `https://localhost:5001/swagger` to access Swagger UI for testing API endpoints.

## Contributing

If you'd like to contribute to this project, please fork the repository, make your changes, and submit a pull request.

### Steps for Contributing

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-branch`).
3. Make your changes and commit (`git commit -am 'Add feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a new pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to the community for their open-source contributions.
- [Microsoft](https://dotnet.microsoft.com/) for creating **ASP.NET Core**.
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) for easy ORM database management.
```

  

