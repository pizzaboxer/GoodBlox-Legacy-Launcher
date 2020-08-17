@echo off
echo The .NET 2.0 Framework is required for GoodBlox to install and run properly.
echo If you already have it installed then you can just skip this.
echo --
set /P confirm=Would you like to install it? (y/n)
if /I "%confirm%" EQU "n" GOTO END

echo The .NET 2.0 Framework will now install.
START NetFx20SP2_x86.exe
pause

:end
echo --
echo If you wish to run this again, then navigate to where you installed GoodBlox and run "checkFordotNET.bat"
echo --
pause
START RegisterXP.exe