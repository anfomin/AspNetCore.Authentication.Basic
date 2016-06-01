@echo off
cd %~dp0

rd /s /q artifacts 2>nul

dotnet restore src
for /D %%d in (src\*) do (
	dotnet pack %%d -c Release -o artifacts
)