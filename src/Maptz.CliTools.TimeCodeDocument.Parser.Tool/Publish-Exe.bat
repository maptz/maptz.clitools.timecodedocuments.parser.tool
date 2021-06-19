REM https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
dotnet publish -r osx-x64 -c Release /p:PublishSingleFile=true
REM osx.10.15-x64