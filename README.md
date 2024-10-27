# Pi Store Management System

## Overview

The Pi Store Management System is a comprehensive Windows Form Application built with C# and SQL Server. It provides a robust solution for managing employees, clients, products, orders, and bills for a Pi Store. This project demonstrates proficiency in C# programming, SQL database design, and user interface development.

## Features

- **User Authentication**: Secure login system for authorized access.
- **Employee Management**: Add, view, edit, and delete employee records.
- **Client Management**: Maintain a database of clients with full CRUD operations.
- **Product Inventory**: Keep track of products, including descriptions, prices, and quantities.
- **Order Processing**: Place and manage orders, automatically updating inventory.
- **Billing System**: Generate bills for orders with automatic price calculation.
- **Data Validation**: Robust input validation and error handling throughout the application.

## Database Schema

The system uses the following SQL Server database schema:

- Employee (ID, Name, Email, Phone, Address, Salary, HireDate)
- Client (ID, Name, Email, Phone, Address)
- Product (ID, Name, Description, Price, Quantity)
- Order (ID, ClientID, EmployeeID, OrderDate, TotalPrice)
- OrderItem (ID, OrderID, ProductID, Quantity)
- Bill (ID, OrderID, ClientID, EmployeeID, BillDate, TotalPrice)

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server 2019 or later

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/everestt2806/pi_store.git
   ```
2. Open the solution file `PiStoreManagement.sln` in Visual Studio.
3. Update the connection string in `App.config` to point to your SQL Server instance.
4. Run the SQL scripts in the `Database` folder to set up your database schema.
5. Build and run the application.

## Usage

1. Launch the application and log in with your credentials.
2. Use the main menu to navigate between different management modules.
3. Follow on-screen instructions for adding, editing, or deleting records.
4. Use the "Place Order" feature to create new orders and generate bills.

## Contributing

Contributions to improve the Pi Store Management System are welcome. Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

## License

Distributed under the MIT License. See `LICENSE` file for more information.

## Contact

Your Name - [your-email@example.com](mailto:your-email@example.com)

Project Link: [https://github.com/yourusername/pi-store-management](https://github.com/yourusername/pi-store-management)

## Acknowledgements

- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [SQL Server Documentation](https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15)
- [Windows Forms Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/?view=netframeworkdesktop-4.8)
