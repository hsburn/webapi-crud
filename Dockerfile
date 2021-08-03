FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /srv/endpoint
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o ./bin/app
EXPOSE 3000
ENTRYPOINT ["dotnet", "./bin/app/telstra.demo.dll"]