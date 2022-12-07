FROM mcr.microsoft.com/dotnet/sdk:6.0

## disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
ARG SONAR_TOKEN
ENV SONAR_TOKEN=$SONAR_TOKEN

WORKDIR /app

 ## Enable SonarCloud
 RUN apt-get update && apt-get install -y openjdk-11-jdk
 # Install sonarscanner
 RUN dotnet tool install --global dotnet-sonarscanner

## Install report generator
RUN dotnet tool install --global dotnet-reportgenerator-globaltool
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet sonarscanner begin /k:"LBHackney-IT_direct-debit-submissions" /o:"lbhackney-it" /d:sonar.host.url=https://sonarcloud.io /d:sonar.login="${SONAR_TOKEN}" /d:sonar.coverageReportPaths="coverage/SonarQube.xml"

# Copy csproj and restore as distinct layers
COPY ./BaseListener.sln ./
COPY ./BaseListener/BaseListener.csproj ./BaseListener/
COPY ./BaseListener.Tests/BaseListener.Tests.csproj ./BaseListener.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./BaseListener/BaseListener.csproj
RUN dotnet restore ./BaseListener.Tests/BaseListener.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out BaseListener/BaseListener.csproj
RUN dotnet build -c debug -o out BaseListener.Tests/BaseListener.Tests.csproj

## Run dotnet test setting the output on the /coverage folder
RUN dotnet test ./BaseListener.Tests/BaseListener.Tests.csproj --collect:"XPlat Code Coverage" --results-directory ./coverage

## Create the code coverage file in sonarqube format using the cobertura file generated from the dotnet test command
RUN reportgenerator "-reports:./coverage/*/coverage.cobertura.xml" "-targetdir:coverage" "-reporttypes:SonarQube"

RUN dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
