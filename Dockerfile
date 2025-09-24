FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build-env
WORKDIR /app

ARG PAT_FOR_NUGET
ARG GITHUB_REPO_OWNER

COPY src .
RUN dotnet restore && dotnet publish Template.API/Template.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim
WORKDIR /app

RUN apt-get update && \
    apt-get install --no-install-recommends -y libcap2-bin && \
    setcap CAP_NET_BIND_SERVICE=+eip /usr/share/dotnet/dotnet && \
    apt-get purge -y --auto-remove libcap2-bin && \
    rm -rf /var/lib/apt/lists/*

RUN useradd -u 8899 armstrong

COPY --chown=armstrong:armstrong --from=build-env /app/out .

RUN apt-get update && apt-get install --no-install-recommends -y curl && rm -rf /var/lib/apt/lists/*

ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

USER armstrong

ENTRYPOINT ["dotnet", "Template.API.dll"]