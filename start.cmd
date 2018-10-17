sqlite3.exe -init  generation_script.sql data.db .exit
pause
csc /r:System.Data.SQLite.dll /out:a.exe /platform:x86 src\*.cs
pause
a.exe -?
pause
a.exe -db data.db
