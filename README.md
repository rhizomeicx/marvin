<p align="center">
  <img 
    src="https://avatars1.githubusercontent.com/u/53635700?s=400&v=4" 
    width="180px"
    alt="Rhizome logo">
</p>

<h1 align="center">Marvin : Price Bot for <a href="https://github.com/iconation/Daedric" />Daedric : Price Feed SCORE </a> </h1>

![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)
<img align="right" src="http://teamcity.mine.bz/app/rest/builds/buildType:(id:Marvin_Build)/statusIcon"/>



## Introduction

**Marvin** is a price feed application (bot) designed to provide an hourly price update to a Daedric SCORE running on the ICON Public Blockchain (see here: https://github.com/iconation/Daedric). Marvin has been written in .Net Core 2.2 and is able to run on either Windows or Ubuntu.

*At the moment this guide assumes you already have a keystore file created. I am working on a utility app that will allow you to generate a new keystore/wallet and interact with basic features.* 
**UPDATE:** Wallet feature has been created. Will write up basic usage shortly. 

## Table of Contents
  * [Ubuntu Setup](https://github.com/rhizomeicx/marvin#ubuntu)
    - [Prerequisites](https://github.com/rhizomeicx/marvin#prerequisites-ubuntu)
    - [Installation](https://github.com/rhizomeicx/marvin#installation-ubuntu)
    - [Configuration](https://github.com/rhizomeicx/marvin#configuration-ubuntu)
    - [Run](https://github.com/rhizomeicx/marvin#run-ubuntu)
  * [Windows Setup](https://github.com/rhizomeicx/marvin#windows)
    - [Prerequisites](https://github.com/rhizomeicx/marvin#prerequisites-windows)
    - [Installation](https://github.com/rhizomeicx/marvin#installation-windows)
    - [Configuration](https://github.com/rhizomeicx/marvin#configuration-windows)
    - [Run](https://github.com/rhizomeicx/marvin#run-windows)
  * [Post Verification Test](https://github.com/rhizomeicx/marvin#Testing)
## Ubuntu

## Prerequisites-Ubuntu"

**[Install .NET Core SDK]**

**Register Microsoft key and feed**
<pre>
$ wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
$ sudo dpkg -i packages-microsoft-prod.deb
</pre>

**Install the .NET SDK**
<pre>
$ sudo apt-get install apt-transport-https
$ sudo apt-get update
$ sudo apt-get install dotnet-sdk-2.2
</pre>


## Installation-Ubuntu

<pre>$ cd /Marvin/Marvin-Ubuntu </pre>

**Build/Publish**
<pre>
$ dotnet publish -c Release -r ubuntu.16.04-x64
</pre>

## Configuration-Ubuntu
Marvin is designed to be driven via some appconfig.

<pre>$ nano /Marvin/Marvin-Ubuntu/appsettings.json </pre>

default should look like this:
<pre> 
{
  "LogPath": "/home/usr/Marvin/logs/log.log",
  "Keystore": "/home/usr/Marvin/config/yeouido/keystore/operator.icx",
  "Daedric_Address": "cx58ca994194cf0c6a2a68b789d81c70484a5675b3",
  "Network_Url": "https://bicon.net.solidwallet.io/api/v3"
}
</pre>
Update the configuration to meet your needs

## Run-Ubuntu
I have yet to successfully run a .NET Core application as a background Daemon on Ubuntu so next best thing is to have this run from cron

<pre>$ crontab -e </pre>

see example of the crontab that is run every hour

<pre> 0 * * * * /usr/bin/dotnet /Marvin/Marvin-Ubuntu/bin/Release/netcoreapp2.2/ubuntu.16.04-x64/publish/Marvin-Ubuntu.dll "yourkeystorepassword" </pre>

you can change this to run every minute to test it is working like so:

<pre> * * * * * /usr/bin/dotnet /Marvin/Marvin-Ubuntu/bin/Release/netcoreapp2.2/ubuntu.16.04-x64/publish/Marvin-Ubuntu.dll "keystorepassword" </pre>

## Windows

## Prerequisites-Ubuntu

**[Install .NET Core SDK]**

download & install https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.401-windows-x64-installer

## Installation-Windows

open Command Prompt (as Administrator) and navigate to:
<pre>cd /Marvin/Marvin-Windows</pre>

**Build/Publish**
<pre>
dotnet build --runtime win-x64 --configuration Release
</pre>

**Create Windows Service**

see below for an example if Marvin was located in "F:\dev\Marvin"
<pre>
sc create Marvin binPath= "F:\dev\Marvin\Marvin\Marvin-Windows\bin\Release\netcoreapp2.2\win-x64\Marvin-Windows.exe" start=auto  
</pre>

## Configuration-Windows
Marvin is designed to be driven via some appconfig.

open in a text editor: /Marvin/Marvin-Windows/appsettings.json

default should look like this:
<pre> 
{
  "LogPath": "F:/dev/marvin/log.log",
  "Keystore": "F:/dev/Marvin/config/mainnet/keystore/operator.icx",
  "Daedric_Address": "cxcc00000000000000000000000000000000000",
  "Network_Url": "https://ctz.solidwallet.io/api/v3",
  "Price_Increment": 3600000
}
</pre>

*Price_Increment* is how often to fetch for new prices default once per hour. 

## Run-Windows

**Start Windows Service**
<pre>
sc start Marvin yourkeystorepassword
</pre>



## Testing
Confirmation of Marvin running can be found in the logs (see log file in LogPath specified in Configuration.
example log file output:  

**NOTE**: Please allow for the full 1 hour to confirm Marvin is working as this is how it is configured. Alternatively, stop the service and change the "Price_Increment": 3600000 to a lower setting for testing purposes. 

<pre>
2019-08-15 21:43:32.351 +08:00 [INF] Starting Marvin...
2019-08-15 21:43:32.465 +08:00 [INF] ------------------------------
2019-08-15 21:43:32.465 +08:00 [INF] Getting current price...
2019-08-15 21:43:38.359 +08:00 [INF] ICXUSD current price: 5086328541551993856
2019-08-15 21:43:38.726 +08:00 [INF] Updating Daedric...
2019-08-15 21:43:39.994 +08:00 [INF] Deadric updated tx hash: 0x41f7ee3d9955e501058d6349df57c2c540409cf83462d8e5943e991d2c702fa9
</pre>

Also confirm on ICON tracker: https://bicon.tracker.solidwallet.io/contract/cx58ca994194cf0c6a2a68b789d81c70484a5675b3
