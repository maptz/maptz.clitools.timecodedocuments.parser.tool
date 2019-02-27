@echo off
SET TOOLPATH="%~dp0/Maptz.Avid.TimeCodeToAvidDS.Tool.dll"

:loop
	if [%1]==[] goto :eof
	dotnet %TOOLPATH% --FilePath %1 --Overwrite true
	shift
	goto loop
:eof
	pause
	exit