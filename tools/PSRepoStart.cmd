@echo off
SET currDir=%~dp0
SET PSModulePath=%PSModulePath%;%currDir%\Modules
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsDevCmd.bat"
