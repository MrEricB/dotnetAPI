# Company XYZ API

A .NET API simulating the simple needs of a company. CRUD for Employees/Users and Jobs. This API is part of a project for school. Originally uisng Node.JS, Express, and MySQL. Migrated over to use .NET 8 and Microsoft SQL Server


## Needed Dependencies

[.NET 8](https://dotnet.microsoft.com/en-us/download) <br>
MS SQL SERVER <br>
VS CODE (optional)

**NOTE: This Project was developed and tested with Ubuntu 22.04** <br>

**NOTE: This Project uses Dapper and not Entity Framework as an ORM** 

## How to run it

Clone the repository into your working directory:
```bash
  git clone https://github.com/MrEricB/dotnetAPI.git
```
Move into the cloned directory:
```bash
cd dotnetAPI
```

Please run the SQL seed scripts first: <br> Schemas.SQL and DBSeed.SQL<br><br>
Once everything is installed the API can now be run.
```bash
dotnet watch run
```
or
```bash
dotnet run
```



## API Reference

#### Test connection

```http
  GET /User/TestConnection
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `None` | `None` | Returns a Date and Tme to test if Database is connected |

## Employee API

```JSON
{
  UserId: Int,
  FirstName: String,
  LastName: String,
  Email: String,
  Active: Bit,
  JobId: Int
}
```

#### GET All Employees

```http
  GET /User/Users
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `None`      | `None` | Returns all users/employees in database |


#### Add Employee
```http
  POST /User/Users
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `User`      | `JSON` | Takes User model (JSON) and adds them to the Databas |


#### Get All Employees (Detailed)
```http
  GET /User/UsersFull
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `None`      | `None` |  Returns all users with job and salary descriptions |

#### Get Single Employee
```http
  GET /User/Users/{userId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `User ID`      | `INT` | Returns single employee|

#### Update Single Employee
```http
  PUT /User/Users/{userId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `User ID, User`      | `INT, JSON` | Updates Employee in Database|

#### Get Single Employee (Detalied)
```http
  GET /User/Users/{userId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `User ID`      | `INT` | Returns single employee with job and salary|

#### DELETE Single Employee
```http
  DELET /User/Users/{userId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `User ID`      | `INT` | Returns single employee|


## Job API

```JSON
{
  JobId: Int,
  JobTitle: String,
  Separtment: String,
  Salary: Decimal
}
```
#### GET All Jobs

```http
  GET /UserJobInfo/Jobs
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `None`      | `None` | Returns all jobs in database |

#### Add Jobs

```http
  POST /UserJobInfo/Jobs
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `Job`      | `JSON` | Adds Job to the Database |

```http
  GET /UserJobInfo/Jobs/{jobId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `jobid`      | `int` | Returns single job form the DB |

```http
  PUT /UserJobInfo/Jobs/{jobId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `jobid, Job`      | `int, JSON` | Updates a Job in the Database |

```http
  DELETE /UserJobInfo/Jobs/{jobId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `jobid`      | `int` | Removes Job from the Database |

## Roadmap

- Add [SQLite](https://www.sqlite.org/index.html) database suport

- Add Authentication
- Add aditional endpoints

