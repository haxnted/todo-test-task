FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TodoTask.Presentation/TodoTask.Presentation.csproj", "src/TodoTask.Presentation/"]
COPY ["src/TodoTask.Application/TodoTask.Application.csproj", "src/TodoTask.Application/"]
COPY ["src/TodoTask.Domain/TodoTask.Domain.csproj", "src/TodoTask.Domain/"]
COPY ["src/GeneralKernel/TodoTask.GeneralKernel.Abstractions/TodoTask.GeneralKernel.Abstractions.csproj", "src/GeneralKernel/TodoTask.GeneralKernel.Abstractions/"]
COPY ["src/GeneralKernel/TodoTask.GeneralKernel.Database/TodoTask.GeneralKernel.Database.csproj", "src/GeneralKernel/TodoTask.GeneralKernel.Database/"]
COPY ["src/TodoTask.Contracts/TodoTask.Contracts/TodoTask.Contracts.csproj", "src/TodoTask.Contracts/TodoTask.Contracts/"]
RUN dotnet restore "src/TodoTask.Presentation/TodoTask.Presentation.csproj"
COPY . .
WORKDIR "/src/src/TodoTask.Presentation"
RUN dotnet build "./TodoTask.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TodoTask.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoTask.Presentation.dll"]
