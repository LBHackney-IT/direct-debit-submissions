FROM mcr.microsoft.com/dotnet/sdk:6.0

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and NUGet.Config and restore as distinct layers
COPY ./DirectDebitSubmissions/DirectDebitSubmissions.csproj ./
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config
RUN dotnet restore ./DirectDebitSubmissions.csproj


# Copy everything else and build
COPY ./DirectDebitSubmissions ./
RUN dotnet build -c Release -o out ./DirectDebitSubmissions.csproj
RUN dotnet publish -c Release -o out ./DirectDebitSubmissions.csproj

EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/DirectDebitSubmissions.dll
