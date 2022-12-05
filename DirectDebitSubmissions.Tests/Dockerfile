FROM mcr.microsoft.com/dotnet/sdk:6.0

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'
ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

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
