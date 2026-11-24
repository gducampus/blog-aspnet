# Étape 1 : compiler et publier avec le SDK .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Blog.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app

# Étape 2 : l'image finale, avec seulement le runtime ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
RUN mkdir /app/data && chown app /app/data
USER app
ENV ASPNETCORE_HTTP_PORTS=10000
ENV ConnectionStrings__BlogContext="Data Source=/app/data/blog.db"
EXPOSE 10000
ENTRYPOINT ["dotnet", "Blog.dll"]
