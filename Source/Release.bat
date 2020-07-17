@echo off
set connection=%~1
set password=%~2
set package_root=..\..\..

REM Find the spkl in the package folder (irrespective of version)
For /R %package_root% %%G IN (spkl.exe) do (
	IF EXIST "%%G" (set spkl_path=%%G
	goto :continue)
	)

:continue
@echo Deploying jusing '%spkl_path%'

REM spkl plugins [path] [connection-string] [/p:release]
"%spkl_path%" plugins "" "%connection%%password%" /p:release

if errorlevel 1 (
echo Error Code=%errorlevel%
exit /b %errorlevel%
)