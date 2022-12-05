FROM mcr.microsoft.com/dotnet/sdk:6.0

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN

ARG SONAR_TOKEN
ENV SONAR_TOKEN=$SONAR_TOKEN

WORKDIR /app

# Enable SonarCloud
RUN apt-get update && apt-get install -y openjdk-11-jdk
RUN dotnet tool install --global dotnet-sonarscanner
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet sonarscanner begin /k:"LBHackney-IT_direct-debit-submissions" /o:"lbhackney-it" /d:sonar.host.url=https://sonarcloud.io /d:sonar.login="${SONAR_TOKEN}"

# Copy csproj and restore as distinct layers
COPY ./DirectDebitSubmissions.sln ./
COPY ./DirectDebitSubmissions/DirectDebitSubmissions.csproj ./DirectDebitSubmissions/
COPY ./DirectDebitSubmissions.Tests/DirectDebitSubmissions.Tests.csproj ./DirectDebitSubmissions.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./DirectDebitSubmissions/DirectDebitSubmissions.csproj
RUN dotnet restore ./DirectDebitSubmissions.Tests/DirectDebitSubmissions.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out DirectDebitSubmissions.Tests/DirectDebitSubmissions.Tests.csproj

CMD dotnet test
