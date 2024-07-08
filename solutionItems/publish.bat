dotnet publish --runtime linux-arm64 -p PublishProfile=DefaultContainer -p ContainerArchiveOutputPath=../../dockerImages/
"C:\Program Files\PuTTY\pscp.exe" -pw xxxxxxxx -r "D:\Users\kirichenec\Git\ReceptionNET\dockerImages" kirichenec@kirichrpi5.local:/home/kirichenec/Downloads
