# The `FROM` instruction specifies the base image. You are
# extending the `microsoft/aspnet` image.

FROM mcr.microsoft.com/dotnet/framework/aspnet:latest

# The final instruction copies the site you published earlier into the container.
COPY ./bin/Release/net7.0/publish/ /inetpub/wwwroot

EXPOSE 80