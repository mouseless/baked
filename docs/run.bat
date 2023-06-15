@echo off
:BEGIN
echo (1) Dev
echo (2) Dev With Build
echo (3) Local
echo (4) Eslint
echo (5) Eslint Fix
CHOICE /N /C:12345 /M "Pick mode"
IF ERRORLEVEL == 5 GOTO Eslint-Fix
IF ERRORLEVEL == 4 GOTO Eslint
IF ERRORLEVEL == 3 GOTO Local
IF ERRORLEVEL == 2 GOTO Dev_With_Build
IF ERRORLEVEL == 1 GOTO Dev
GOTO END
:Dev
cd .theme
npm run dev
cd ..
:Dev_With_Build
cd .theme
npm run dev:build
cd ..
:Local
cd .theme
npm run local
cd ..
:Eslint
cd .theme
npx eslint .
cd ..
:Eslint-Fix
cd .theme
npx eslint . --fix
cd ..
END:
