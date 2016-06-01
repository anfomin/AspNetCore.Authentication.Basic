@echo off
cd %~dp0

echo Cleaning artifacts
rd /s /q artifacts 2>nul

for /D %%d in (src\*) do (
	echo Cleaning directory %%d
	rd /s /q %%d\bin 2>nul
	rd /s /q %%d\obj 2>nul
	del /q %%d\project.lock.json 2>nul
)