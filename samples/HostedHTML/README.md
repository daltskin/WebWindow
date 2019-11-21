# WebWindow with Hosted HTML

Create a single binary and run

## Windows

1. Publish

```powershell
dotnet publish -o publish -c Debug -r win10-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```

2. Execute:
```powershell
.\HostedHTML.exe
```

## Linux (WSL)

1. Publish

```bash
dotnet publish -o publish -c Debug -r linux-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```

2.	Install Xming https://sourceforge.net/projects/xming/files/latest/download

```bash
export DISPLAY=:0
```

3.	Install dependencies:

```bash
sudo apt-get update && sudo apt-get install libgtk-3-dev libwebkit2gtk-4.0-dev
```

4. Execute:
```bash
./HostedHTML
```