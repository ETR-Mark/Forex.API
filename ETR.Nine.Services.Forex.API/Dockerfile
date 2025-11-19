# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install AWS CLI inside the build container
RUN apt-get update && apt-get install -y curl unzip && \
    curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip" && \
    unzip awscliv2.zip && ./aws/install

# Copy solution and project files (paths relative to build context)
COPY ../ETR.Nine.Services.Forex.sln ./
COPY ETR.Nine.Services.Forex.Domain/*.csproj ./ETR.Nine.Services.Forex.Domain/
COPY ETR.Nine.Services.Forex.Application/*.csproj ./ETR.Nine.Services.Forex.Application/
COPY ETR.Nine.Services.Forex.Infrastructure/*.csproj ./ETR.Nine.Services.Forex.Infrastructure/
COPY ETR.Nine.Services.Forex.API/*.csproj ./ETR.Nine.Services.Forex.API/

# Authenticate to AWS CodeArtifact
ARG AWS_ACCESS_KEY_ID
ARG AWS_SECRET_ACCESS_KEY
ARG AWS_REGION=ap-southeast-1

ENV AWS_ACCESS_KEY_ID=$AWS_ACCESS_KEY_ID
ENV AWS_SECRET_ACCESS_KEY=$AWS_SECRET_ACCESS_KEY
ENV AWS_DEFAULT_REGION=$AWS_REGION

RUN aws codeartifact login --tool dotnet --domain etrphils --repository etr-nine --region $AWS_REGION

# Restore dependencies
RUN dotnet restore

# Copy all source code
COPY ../ ./

# Build and publish the API
WORKDIR /src/ETR.Nine.Services.Forex.API
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Create folder for SQLite DB
RUN mkdir -p /app/data

# Copy the database into the image (from API folder)
COPY ETR.Nine.Services.Forex.API/ETRForex.db /app/data/ETRForex.db

# Copy published app from build stage
COPY --from=build /app/publish ./

# Set the connection string to point to the copied DB
ENV ConnectionStrings__SQLiteConnection="Data Source=/app/data/ETRForex.db"

EXPOSE 8080
ENTRYPOINT ["dotnet", "ETR.Nine.Services.Forex.API.dll"]
