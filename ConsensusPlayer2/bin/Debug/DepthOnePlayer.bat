@ECHO OFF
echo %* > input.json
DepthOnePlayer.exe %*
exit /b %errorlevel%