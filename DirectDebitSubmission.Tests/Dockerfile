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
RUN dotnet sonarscanner begin /k:"LBHackney-IT_financial-transactions-api" /o:"lbhackney-it" /d:sonar.host.url=https://sonarcloud.io /d:sonar.login="${SONAR_TOKEN}" /d:sonar.coverageReportPaths="coverage/SonarQube.xml"

# Copy csproj and restore as distinct layers
COPY ./DirectDebitSubmission.sln ./
COPY ./DirectDebitSubmission/DirectDebitSubmission.csproj ./DirectDebitSubmission/
COPY ./DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj ./DirectDebitSubmission.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./DirectDebitSubmission/DirectDebitSubmission.csproj
RUN dotnet restore ./DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out DirectDebitSubmission/DirectDebitSubmission.csproj
RUN dotnet build -c debug -o out DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj

## Run dotnet test setting the output on the /coverage folder
RUN dotnet test ./DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj --collect:"XPlat Code Coverage" --results-directory ./coverage

## Create the code coverage file in sonarqube format using the cobertura file generated from the dotnet test command
RUN reportgenerator "-reports:./coverage/*/coverage.cobertura.xml" "-targetdir:coverage" "-reporttypes:SonarQube"

RUN dotnet sonarscanner end /d:sonar.login="${SONAR_TOKEN}"
