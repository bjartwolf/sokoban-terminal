# sokoban-terminal
I want to make [sokoban](https://en.wikipedia.org/wiki/Sokoban) in the terminal.
It should be cross-platform. I wrote the game in F#, and then I have made UIs to expose the game over web with links, a Python Tkinter client, a Javascript client with UI I borrowed from https://sokoboko.glitch.me/ (https://github.com/Glorp) and a cross plattform .NET terminal UI written in C#.


To run try
```
dotnet run --project src/terminal/sokoban-terminal.csproj
```

# C# Terminal GUI

It is built with [Terminal.Gui](https://gui-cs.github.io/Terminal.Gui/index.html)

![Screenshot](screenshot.PNG)


# Python things


<img width="376" alt="image" src="https://user-images.githubusercontent.com/88324093/193473608-f2935b1e-65f3-48c5-940d-cf13bfd3ea3e.png">


```
dotnet new --install Fable.Template
dotnet tool restore

```

# Javascript things
Only works on my WSL setup
```
cd src/sokoban-js
npm ci
npm run start
```
<img width="435" alt="image" src="https://user-images.githubusercontent.com/88324093/193473673-19ec4e52-f617-44b8-8050-3a6ef98da33a.png">

# JSON 

![image](https://user-images.githubusercontent.com/88324093/193473744-57b685b0-1c41-4480-9c4e-44dbdaa3f829.png)

