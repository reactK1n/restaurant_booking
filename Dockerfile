#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .

COPY ["restaurant_booking_api/restaurant_booking_api.csproj", "restaurant_booking_api/"]
COPY ["restaurant_booking_Application/restaurant_booking_Application.csproj", "restaurant_booking_Application/"]
COPY ["restaurant_booking_Domain/restaurant_booking_Domain.csproj", "restaurant_booking_Domain/"]
COPY ["restaurant_booking_Infrastructure/restaurant_booking_Infrastructure.csproj", "restaurant_booking_Infrastructure/"]
RUN dotnet restore "restaurant_booking_api/restaurant_booking_api.csproj"
COPY . .
WORKDIR /src/restaurant_booking_api
RUN dotnet build

FROM build AS publish
WORKDIR /src/restaurant_booking_api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY --from=publish /src/restaurant_booking_api/Json/Meal.json ./

CMD ASPNETCORE_URLS=http://*:$PORT dotnet restaurant_booking_api.dll