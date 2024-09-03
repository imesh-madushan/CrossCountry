# CrossCountry

_A Shoe Selling Application (desktop)_, built with **C# .NET**, is designed to offer an engaging and user-friendly shoe shopping experience.

>It’s a project created to achive my acedemic goals and enhance my development and design skills.

### Features
- **User Registration & Login**: Securely register and log in to your account.
- **Browse Shoes**: Explore a variety of shoes with detailed price lists.
- **Shopping Cart**: Add items to your cart and proceed to checkout.
- **Multiple Addresses**: Manage and select multiple shipping addresses.
- **Order History**: View and manage your past orders.
- **Admin Dashboard**: Separate admin login to update stock and manage inventory.


### Installation
1. **Clone the Repository**

   ```bash
   git clone https://github.com/imesh927/CrossCountry.git
   ```
   
2. **Open in Visual Studio**
     - Open the project in [Visual Studio](https://visualstudio.microsoft.com/) and restore NuGet packages.

3. **Setup the Database**
   - Import the provided SQL script [`ScrossCountry.sql`](ScrossCountry.sql) into Microsoft SQL Server and execute queries to set up the database.
   - Configure the connection string in the [`WPFApp-GUI Project/CreateDBconnection.cs`](WPFApp-GUI%20Project/CreateDBconnection.cs) file to match your database setup.

4. **Build and Run**
   - Build the solution in Visual Studio.
   - Run the application.

### Usage

>**NOTE**:

>This programm only contains limited numberof samples shoes.
If you  wish to add more custom shoes you need upload images to `WPFApp-GUI Project/products`, and also add that item to database with manual prodocut id and other data.

>Admins accounts are created by database query manually.

- **For Customers**: Register or log in to browse shoes, add them to your cart, and proceed to checkout. Manage your shipping addresses and view your order history.
- **For Admins**: Log in through the admin interface to update stock and manage inventory.

### License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for details.

### Acknowledgments

- [Microsoft](https://www.microsoft.com/) for providing the .NET framework.
- [Visual Studio](https://visualstudio.microsoft.com/) for an excellent development environment.
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) for database management.

---

Thank you !
