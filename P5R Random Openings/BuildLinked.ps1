# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/P5R_Random_Openings/*" -Force -Recurse
dotnet publish "./P5R_Random_Openings.csproj" -c Release -o "$env:RELOADEDIIMODS/P5R_Random_Openings" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location