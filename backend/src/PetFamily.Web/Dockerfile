FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetFamily.Web/PetFamily.Web.csproj", "src/PetFamily.Web/"]
COPY ["src/Accounts/PetFamily.Accounts.Infrastructure/PetFamily.Accounts.Infrastructure.csproj", "src/Accounts/PetFamily.Accounts.Infrastructure/"]
COPY ["src/Shared/PetFamily.Core/PetFamily.Core.csproj", "src/Shared/PetFamily.Core/"]
COPY ["src/Shared/PetFamily.SharedKernel/PetFamily.SharedKernel.csproj", "src/Shared/PetFamily.SharedKernel/"]
COPY ["src/Shared/PetFamily.Framework/PetFamily.Framework.csproj", "src/Shared/PetFamily.Framework/"]
COPY ["src/Accounts/PetFamily.Accounts.Contracts/PetFamily.Accounts.Contracts.csproj", "src/Accounts/PetFamily.Accounts.Contracts/"]
COPY ["src/Accounts/PetFamily.Accounts.Application/PetFamily.Accounts.Application.csproj", "src/Accounts/PetFamily.Accounts.Application/"]
COPY ["src/Accounts/PetFamily.Accounts.Domain/PetFamily.Accounts.Domain.csproj", "src/Accounts/PetFamily.Accounts.Domain/"]
COPY ["src/Accounts/PetFamily.Accounts.Presentation/PetFamily.Accounts.Presentation.csproj", "src/Accounts/PetFamily.Accounts.Presentation/"]
COPY ["src/Pet/PetFamily.Pets.Application/PetFamily.Pets.Application.csproj", "src/Pet/PetFamily.Pets.Application/"]
COPY ["src/Specie/PetFamily.Species.Contracts/PetFamily.Species.Contracts.csproj", "src/Specie/PetFamily.Species.Contracts/"]
COPY ["src/Pet/PetFamily.Pets.Domain/PetFamily.Pets.Domain.csproj", "src/Pet/PetFamily.Pets.Domain/"]
COPY ["src/Pet/PetFamily.Pets.Infrastructure/PetFamily.Pets.Infrastructure.csproj", "src/Pet/PetFamily.Pets.Infrastructure/"]
COPY ["src/Pet/PetFamily.Pets.Contracts/PetFamily.Pets.Contracts.csproj", "src/Pet/PetFamily.Pets.Contracts/"]
COPY ["src/Pet/PetFamily.Pets.Presentation/PetFamily.Pets.Presentation.csproj", "src/Pet/PetFamily.Pets.Presentation/"]
COPY ["src/Specie/PetFamily.Species.Application/PetFamily.Species.Application.csproj", "src/Specie/PetFamily.Species.Application/"]
COPY ["src/Specie/PetFamily.Species.Domain/PetFamily.Species.Domain.csproj", "src/Specie/PetFamily.Species.Domain/"]
COPY ["src/Specie/PetFamily.Species.Infrastructure/PetFamily.Species.Infrastructure.csproj", "src/Specie/PetFamily.Species.Infrastructure/"]
COPY ["src/Specie/PetFamily.Species.Presentation/PetFamily.Species.Presentation.csproj", "src/Specie/PetFamily.Species.Presentation/"]
RUN dotnet restore "src/PetFamily.Web/PetFamily.Web.csproj"
COPY . .
WORKDIR "/src/src/PetFamily.Web"
RUN dotnet build "PetFamily.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetFamily.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetFamily.Web.dll"]
