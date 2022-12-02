FROM mcr.microsoft.com/dotnet/sdk:6.0

# disable microsoft telematry
ENV DOTNET_CLI_TELEMETRY_OPTOUT='true'

ENV DynamoDb_LocalMode='true'
ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DirectDebitSubmission.sln ./
COPY ./DirectDebitSubmission/DirectDebitSubmission.csproj ./DirectDebitSubmission/
COPY ./DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj ./DirectDebitSubmission.Tests/
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config

RUN dotnet restore ./DirectDebitSubmission/DirectDebitSubmission.csproj
RUN dotnet restore ./DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj

# Copy everything else and build
COPY . .

RUN dotnet build -c debug -o out DirectDebitSubmission.Tests/DirectDebitSubmission.Tests.csproj

CMD dotnet test
