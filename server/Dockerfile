###############################################################################
## SPDX-License-Identifier: MIT
## Copyright (c) 2026 Florian Wilhelm
## Description: Multi-stage Docker build for the frontend and ASP.NET server.
###############################################################################
# Stage 1: use Node to build the frontend inside Docker.
FROM node:20 AS frontend-build
# Put the frontend build under a predictable working folder.
WORKDIR /src/frontend
# Copy package files first so Docker can cache dependency install.
COPY frontend/package*.json ./
# Install exact frontend dependencies from the lock file.
RUN npm ci
# Copy the rest of the frontend source code.
COPY frontend/ ./
# Produce the static frontend build output.
RUN npm run build

# Stage 2: use the .NET SDK to build and publish the server.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
# Work from /src for restore, copy, and publish commands.
WORKDIR /src

# Copy only the project file first so restore can be cached separately.
COPY ["server/Server.csproj", "./"]
# Download NuGet packages needed by the server project.
RUN dotnet restore "Server.csproj"

# Copy the rest of the server source code into the build stage.
COPY server/ ./

# Move the built frontend files into the server's static web root.
COPY --from=frontend-build /src/frontend/out ./wwwroot

# Publish a Release build into a clean output folder for the runtime image.
RUN dotnet publish "Server.csproj" -c Release -o /app/publish /p:SkipFrontendBuild=true

# Stage 3: create the smaller final image that only runs the app.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
# The app will run from /app inside the final container.
WORKDIR /app
# Copy the published server files from the build stage.
COPY --from=build /app/publish .

# Document that the container listens on port 80.
EXPOSE 80
# Tell ASP.NET Core to bind to port 80 on all interfaces.
ENV ASPNETCORE_URLS=http://+:80

# Start the published ASP.NET application.
ENTRYPOINT ["dotnet", "Server.dll"]
