FROM mcr.microsoft.com/dotnet/sdk:6.0
RUN apt-get update && apt-get -y install xvfb && apt-get -y install fontconfig && apt-get -y install libssl1.0-dev && apt-get -y install libx11-dev libx11-xcb-dev libxcb-icccm4-dev libxcb-image0-dev libxcb-keysyms1-dev libxcb-randr0-dev libxcb-render-util0-dev libxcb-render0-dev libxcb-shm0-dev libxcb-util0-dev libxcb-xfixes0-dev libxcb-xkb-dev libxcb1-dev libxfixes-dev libxrandr-dev libxrender-dev

ARG LBHPACKAGESTOKEN
ENV LBHPACKAGESTOKEN=$LBHPACKAGESTOKEN
WORKDIR /app

# Copy csproj and NUGet.Config and restore as distinct layers
COPY ./DirectDebitSubmission/DirectDebitSubmission.csproj ./
COPY /NuGet.Config /root/.nuget/NuGet/NuGet.Config
RUN dotnet restore ./DirectDebitSubmission.csproj


# Copy everything else and build
COPY ./DirectDebitSubmission ./
RUN dotnet build -c Release -o out ./DirectDebitSubmission.csproj
RUN dotnet publish -c Release -o out ./DirectDebitSubmission.csproj

EXPOSE ${PORT:-3000}
CMD ASPNETCORE_URLS=http://+:${PORT:-3000} dotnet ./out/DirectDebitSubmission.dll
