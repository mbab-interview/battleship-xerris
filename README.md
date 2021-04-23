# battleship-xerris
This is an interview problem sent by Xerris that requires to build a command line battleship game

## Usage

### .Net

If you have dotnet 5 installed you can run the following command from the root of this repository.

```powershell
dotnet run --project .\src\battleship\battleship.csproj
```

### Docker

Your can use the provided DockerFile. Note

## Code Coverage

I used NCrunch for code coverage visualization during the development of this challenge. Therefore I didn't automate code coverage reports that I ran only a few time at the end.

The code coverage report is not automated at this moment (I'm sorry, didn't find an easy win for this so far). It can be obtained with coverlet and [ReportGenerator ](https://github.com/danielpalme/ReportGenerator).

To install the tooling you can run

```powershell
dotnet tool install -g dotnet-reportgenerator-globaltool
```

Then you can run

```powershell
dotnet test --collect:"XPlat Code Coverage"
```

Then it should run the test and show you a path with the report (it looks like `C:\dev\battleship-xerris\tests\battleship.tests\TestResults\6966395a-9c00-4bd0-81cc-51608ac630d4\coverage.cobertura.xml`). You will need to use that path to generate the html report with the following command (The double quotes arguments are really unusual ...).

```powershell
reportgenerator "-reports:.\tests\TestsResults\theGuid\coverage.cobertura.xml" "-targetdir:.\tests\battleship.tests\TestResults\report" "-reporttypes:Html"
```

The code coverage report should then be available at

```powershell
.\tests\battleship.tests\TestResults\report\index.html
```



