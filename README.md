# SteamAutoStarter
A small tool to automaticall launch steam, when your SteamLink tries to discover it

## Why?

The [SteamLink](https://store.steampowered.com/app/353380/Steam_Link/) is a very neat pice of hardware (or Software!) to stream games from your PC to a TV or similiar.
The downside is, that it (understandably) only works when Steam is running on your PC.
This means that you you have to set Steam to start automatically with your PC.
Since I use by PC mostly for work, I tried to avoid this.

This small tool can start automatically with your PC and waits for a Steam discovery request from your SteamLink.
When such a request is detected, Steam is launched and the tool quits.

## Installation

- Download the [latest release from GitHub](https://github.com/nrother/SteamAutoStarter/releases) and extract the ZIP-File somewhere
- Right-click the file `SteamAutoStarter.exe` and select "Copy"
- Open a new Explorer Window, type `shell:startup` in the address bar
- Right click the empty space and select "Paste as link". This will make SteamAutoStart start automatically with your computer.

## Usage

When the tool is installed and running (i.e. after a restart of your computer) just start your SteamLink and select your computer.
Steam should automatically start and the SteamLink should connect as usually.

## Limitations

- The tool was only tested on Window. It should be trivial to compile it for Linux thanks to .NET Core, let me know if you are interested in this.
- The tool will exit after Steam was launched. This means that it will not be able to start Steam again until your computer is restarted.
This is usually not a limitation, but probably could be fixed. Let me know, if you are interested in this.

## How?

This tool listens for a valid [Steam In-House Streaming Discovery Protocol](https://codingrange.com/blog/steam-in-home-streaming-discovery-protocol) discovery message (`k_ERemoteClientBroadcastMsgDiscovery`) on port 27036.
When such a messag is received Steam is launched using the [Stream Browser protcol](https://developer.valvesoftware.com/wiki/Steam_browser_protocol) (`steam://`) and the tool exits.
