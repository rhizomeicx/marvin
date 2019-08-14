<p align="center">
  <img 
    src="https://avatars1.githubusercontent.com/u/53635700?s=400&v=4" 
    width="180px"
    alt="Rhizome logo">
</p>

<h1 align="center">Marvin : Price Bot for <a href="https://github.com/iconation/Daedric" />Daedric : Price Feed SCORE </a> </h1>

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

## Introduction

**Marvin** is a price feed application (bot) designed to provide an hourly price update to a Daedric SCORE running on the ICON Public Blockchain (see here: https://github.com/iconation/Daedric). Marvin has been written in .Net Core 2.2 and is able to be run on either Windows or Linux OS.

## Table of Contents

  * [Prerequisites](https://github.com/rhizomeicx/marvin#prerequisites-ubuntu)
  * [Installation-Ubuntu](https://github.com/rhizomeicx/marvin#installation-ubuntu)

## Prerequisites-Ubuntu

**[Install .NET Core SDK]**

**Register Microsoft key and feed**
<pre>
wget -q https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
</pre>

**Install the .NET SDK**
<pre>
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-2.2
</pre>


## Installation-Ubuntu

<pre> cd /Marvin/Marvin-Ubuntu </pre>

**Build/Publish**
<pre>
dotnet build --runtime ubuntu.16.04-x64 --configuration Release
</pre>

## Configuration
Marvin is designed to be driven via some appconfig.

<pre> nano /Marvin/Marvin-Ubuntu/appsettings.json </pre>

default should look like this:
<pre> 
{
  "LogPath": "/home/usr/Marvin/logs/log.log",
  "Yeouido_Keystore": "/home/fir3fight/Marvin/config/yeouido/keystore/operator.icx",
  "Yeouido_Daedric_Address": "cx58ca994194cf0c6a2a68b789d81c70484a5675b3",
  "Yeouido_url": "https://bicon.net.solidwallet.io/api/v3"
}
</pre>
Update the configuration to meet your needs

## Run - Ubuntu
I have yet to successfully run a .NET Core application as a background Daemon on Ubuntu so next best thing is to have this run from cron

<pre> crontab -e </pre>

see example of the crontab that is run every hour

<pre> 1 * * * * /usr/bin/dotnet /Marvin/Marvin-Ubuntu/bin/release/netcoreapp2.2/ubuntu.16.04-x64/publish/Marvin-Ubuntu.dll "keystorepassword" </pre>

you can change this to run every minute to test it is working like so:

<pre> * * * * * /usr/bin/dotnet /Marvin/Marvin-Ubuntu/bin/release/netcoreapp2.2/ubuntu.16.04-x64/publish/Marvin-Ubuntu.dll "keystorepassword" </pre>
