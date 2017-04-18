@ECHO OFF
CALL "C:\Program Files (x86)\Microsoft Visual Studio 11.0\VC\vcvarsall.bat"
PATH %path%;C:\Program Files (x86)\NUnit 2.6.3\bin
CD %~dp0
CD bin\debug
nunit-console.exe /process:separate /framework:4.5 /include:Live AlphaRooms.AutomationFramework.Tests.dll
PAUSE