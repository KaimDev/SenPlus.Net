# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0

# Set the working directory.
WORKDIR /app

# Copy csproj and restore as distinct layers.
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the project.
COPY . ./
RUN dotnet publish -c Release -o out

COPY --from=0 /app/out .

# The application runs on port 80 by default, expose that port.
EXPOSE 80

# Run the application when the docker container starts.
ENTRYPOINT ["dotnet", "SenPlus.dll"]