FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY DistanceCalculator.sln .
COPY Api/ Api/
COPY Common/ Common/

RUN dotnet restore
RUN dotnet clean
RUN dotnet build

WORKDIR /source/Api
CMD dotnet run --launch-profile "Production"
