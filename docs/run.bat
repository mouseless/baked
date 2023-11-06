@echo off
:BEGIN
echo (1) Dev
echo (2) Local
echo (3) Eslint Fix
CHOICE /N /C:123 /M "Pick mode"
IF ERRORLEVEL == 3 GOTO Eslint-Fix
IF ERRORLEVEL == 2 GOTO Local
IF ERRORLEVEL == 1 GOTO Dev
GOTO END
:Dev
cd .theme
npm run dev
cd ..
:Local
cd .theme
npm run local
cd ..
:Eslint-Fix
cd .theme
npx eslint . --fix
cd ..
END:
