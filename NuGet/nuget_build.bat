
xcopy ..\src\ImageFontWindow\bin\Debug\ImageFontWindow.dll .\ImageFontWindow\lib\ImageFontWindow.dll /Y

nuget pack .\ImageFontWindow\ImageFontWindow.nuspec

nuget pack .\ImageFontWindow.WPF\ImageFontWindow.WPF.nuspec