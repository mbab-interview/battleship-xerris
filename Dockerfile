FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . /app
WORKDIR /app
RUN dotnet build -c Release -o output
RUN dotnet test

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime
COPY --from=build /app/output .
ENTRYPOINT ["dotnet", "battleship.dll"]