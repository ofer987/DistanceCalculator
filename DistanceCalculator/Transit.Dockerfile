FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY DistanceCalculator.sln .
COPY DistanceCalculator.Api/ DistanceCalculator.Api/
COPY DistanceCalculator.Common/ DistanceCalculator.Common/

RUN dotnet restore
RUN dotnet clean
RUN dotnet build

WORKDIR /source/DistanceCalculator.Api
CMD dotnet run --launch-profile "Production"
