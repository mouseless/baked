@echo off
:BEGIN
echo (1) Production
echo (2) Local
CHOICE /N /C:12 /M "Pick mode"
IF ERRORLEVEL == 2 GOTO Local
IF ERRORLEVEL == 1 GOTO Production
GOTO END
:Production
cd .theme
npm run generate:production
cd ..
:Local
cd .theme
npm run generate:local
cd ..
END:
