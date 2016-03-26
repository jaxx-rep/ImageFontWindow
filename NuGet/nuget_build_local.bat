
call nuget_build

xcopy .\*.nupkg S:\Etudiants\Fred\NuGetFeed\ /Y
