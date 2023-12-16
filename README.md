
### 0. Add MongoDB and DOT.ENV
```bash
dotnet add package MongoDB.Driver
dotnet add package DotNetEnv
```

### 1. Mention ENV in `Program.cs`
```cs
if (builder.Environment.IsDevelopment())
{
    try
    {
        DotNetEnv.Env.Load();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading environment variables: {ex.Message}");
        throw; // Rethrow the exception
    }
}

builder.Configuration
    .AddEnvironmentVariables()  // Adds environment variables to the configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddCommandLine(args);
```
* Add `.env` to `.gitignore` file

### 2. Add `.env` file to root folder
```env
MY_VARIABLE=from DOT ENV

DB_NAME=######
DB_PASSWORD=#####
DB_PORT=#####
```

### 3. Create `MongoDBContext.cs` inside Util folder and create connection with DB

* For this project Railway.app client is use as Mongodb provider
  [Read more](https://railway.app/)

* Create connection using .env variables
```cs
var connectionString = $"mongodb://{Environment.GetEnvironmentVariable("DB_NAME")}:{Environment.GetEnvironmentVariable("DB_PASSWORD")}@monorail.proxy.rlwy.net:{Environment.GetEnvironmentVariable("DB_PORT")}";

var client = new MongoClient(connectionString);
```
* Get access to the Connected DataBase
```cs
 _database = client.GetDatabase("test");
```
* Export connection for a collection in DB
```cs
return _database.GetCollection<T>(collectionName);
```

### 4. Export connection `GetConnection` in DatabaseHelper class

### 5. Created `UserServiece.cs` and registered the `UserService` in the dependency injection container in `Program.cs` file
(In order to use the services in side contoller class)
```cs
builder.Services.AddScoped<UserService>();
```

```cs
// use services using dependency injection
private readonly UserService _userService;

public UserController(UserService us)
{
    _userService = us;
}
```