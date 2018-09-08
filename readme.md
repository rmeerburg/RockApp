# RockApp
A small webapi application showcasing some REST api endpoints with odata support, rudimentary authentication and authorization, a database and some unit tests.

## Getting started
Clone the project and navigate to the ```RockApp``` directory.  
Restore dependencies using
```
dotnet restore
```
Run the project using
```
dotnet run
```
A webserver is now listinging on http://localhost:5000/  
Send HTTP requests to ```api/artists``` or ```api/songs```. Tack on some [OData](https://www.odata.org/documentation/odata-version-2-0/operations/) operations to transform the response.  
 Check the code for all supported operations on those endpoints

## Testing
Navigate to the ```RockApp.Test``` directory  
Run
```
dotnet test
```
  
## Built using
* [ASP .NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
* [EF Core](https://docs.microsoft.com/en-us/ef/core/)
* [OData](https://www.odata.org/)
* [xUnit](https://xunit.github.io/)