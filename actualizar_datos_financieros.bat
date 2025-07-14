@echo off
setlocal

REM ============================================
REM    PeopleWorks Finance Data Updater (.BAT)
REM ============================================

set EXE=PeopleWorksFinanceConsole.exe
set INICIO=2024-03
set FIN=2024-03

echo ============================================
echo    🧠 PeopleWorks Finance Data Updater
echo           Periodo: %INICIO% a %FIN%
echo ============================================

REM 🏛️ Catálogo de Entidades (solo una vez necesario)
%EXE% --endpoint=entidades --guardarbd=true

REM 💰 Captaciones
%EXE% --endpoint=captaciones --inicio=%INICIO% --fin=%FIN% --persona=fisica --guardarbd=true
%EXE% --endpoint=captaciones --inicio=%INICIO% --fin=%FIN% --persona=juridica --guardarbd=true

REM 💼 Carteras
%EXE% --endpoint=carteras --inicio=%INICIO% --fin=%FIN% --guardarbd=true

REM 📊 Estados financieros
%EXE% --endpoint=estados --inicio=%INICIO% --fin=%FIN% --guardarbd=true

REM 📈 Indicadores financieros
%EXE% --endpoint=indicadores --inicio=%INICIO% --fin=%FIN% --guardarbd=true

echo ============================================
echo      ✓ Finalizado correctamente
echo ============================================

pause
