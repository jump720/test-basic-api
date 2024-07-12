FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine-amd64 AS runtime
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine-amd64 AS build

# prepare build dir
WORKDIR /app

# Copy the solution files into the container
COPY ./src/ ./

# Publish app version
RUN dotnet publish TestProject.WebApi.csproj -c Release -r linux-musl-x64 --output "./publish" --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -p:NoWarn=*

# Set the final runtime image
FROM runtime AS final
WORKDIR /app
COPY --from=build /app/publish .

# Run the app
RUN chmod +x ./TestProject.WebApi
CMD ["./TestProject.WebApi", "--urls", "http://0.0.0.0:8080"]