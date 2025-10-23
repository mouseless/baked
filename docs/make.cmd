@echo off
setlocal enabledelayedexpansion
title Project Runner

if "%1"=="" (
    echo Usage: %0 ^<command^>
    echo.
    echo Available commands: run, build
    exit /b 1
)

set CMD=%1

if /i "%CMD%"=="run" goto run
if /i "%CMD%"=="build" goto build

echo Invalid command: %CMD%
exit /b 1

:run
cls
echo (1) Dev
echo (2) Local
echo.
set /p srv="Please select 1-2: "

if "%srv%"=="1" goto run_dev
if "%srv%"=="2" goto run_local
echo Invalid selection!
goto end

:run_dev
cd .theme
npm run dev
cd ..
goto end

:run_local
cd .theme
npm run local
cd ..
goto end

:build
cls
echo (1) Production
echo (2) Local
echo.
set /p srv="Please select 1-2: "

if "%srv%"=="1" goto production
if "%srv%"=="2" goto local
echo Invalid selection!
goto end

:production
cd .theme
npm run generate:production
cd ..
goto end

:local
cd .theme
npm run generate:local
cd ..
goto end

:end
echo Done.
