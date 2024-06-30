#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LibraryOfBooks.WebApi/LibraryOfBooks.WebApi.csproj", "src/LibraryOfBooks.WebApi/"]
COPY ["src/LibraryOfBooks.DataAccess/LibraryOfBooks.Dataccess.csproj", "src/LibraryOfBooks.DataAccess/"]
COPY ["src/LibraryOfBooks.Domain/LibraryOfBooks.Domain.csproj", "src/LibraryOfBooks.Domain/"]
COPY ["src/LibraryOfBooks.Service/LibraryOfBooks.Service.csproj", "src/LibraryOfBooks.Service/"]
RUN dotnet restore "./src/LibraryOfBooks.WebApi/LibraryOfBooks.WebApi.csproj"
COPY . .
WORKDIR "/src/src/LibraryOfBooks.WebApi"
RUN dotnet build "./LibraryOfBooks.WebApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LibraryOfBooks.WebApi.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryOfBooks.WebApi.dll"]