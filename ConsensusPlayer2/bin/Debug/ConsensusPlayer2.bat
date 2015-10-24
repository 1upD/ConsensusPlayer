@ECHO OFF
echo %* > input.json
ConsensusPlayer2.exe %*
exit /b %errorlevel%