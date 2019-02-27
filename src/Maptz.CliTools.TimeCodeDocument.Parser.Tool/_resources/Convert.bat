@echo off



FOR %%A IN (%*) DO (

	IF NOT EXIST "%%~DPA\DS" (
		md "%%~DPA\DS"
	)
	IF NOT EXIST "%%~DPA\TXT" (
		md "%%~DPA\TXT"
	)
	IF NOT EXIST "%%~DPA\XML" (
		md "%%~DPA\XML"
	)

	call mwordtotext --FilePath %%~dpnxA --OutputDirectoryPath %%~dpA/TXT --Overwrite true
	if %ERRORLEVEL% GEQ 1 GOTO ERROR
	call mds --FilePath %%~dpA\TXT\%%~NA.txt --OutputFilePath %%~dpA\DS\%%~NA.ds.txt --Overwrite true
	if %ERRORLEVEL% GEQ 1 GOTO ERROR
	call mfcp --FilePath %%~dpA\TXT\%%~NA.txt --OutputFilePath %%~dpA\XML\%%~NA.xml --Overwrite true
	if %ERRORLEVEL% GEQ 1 GOTO ERROR
)
PAUSE
GOTO EOF

:ERROR
EXIT /b 0

:EOF
PAUSE

