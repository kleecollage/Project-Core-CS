# .NET Project #
## System Requirements ##
Before downloading and running this project, make sure your system meets the following requirements:

### .NET 6.0 SDK ###
You need to have the .NET 6.0 SDK installed to build and run the project. You can download it from the official .NET site: [Download .NET 6.0.](https://dotnet.microsoft.com/download/dotnet/6.0)

### MSSQL Server ###
The project requires an MSSQL Server database for its operation. Make sure you have MSSQL Server installed and configured correctly on your system.

### Node.js and npm ###
You must have npm installed, and the recommended version of Node.js is v12.22.12.

## Execution Instructions
Once you've downloaded the project, follow these steps to get it up and running:
* Lift the Backend and Migrate the Database
* From the root of the project, run the following command to stand up the backend and perform the database migration:

```bash
dotnet run --project WebAPI/
```

This command will build and run the project in the WebAPI folder, making sure the database is configured correctly.

Install Dependencies and Launch the Frontend Application

Next, navigate to the courses-online-app folder containing the frontend application, and install the necessary dependencies by running:

```bash
npm install 
npm start 
```

This will launch the app and open it in your default browser.