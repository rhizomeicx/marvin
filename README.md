<p align="center">
  <img 
    src="https://avatars1.githubusercontent.com/u/53635700?s=400&v=4" 
    width="180px"
    alt="Rhizome logo">
</p>

<h1 align="center">Marvin : Price Bot for <a href="https://github.com/iconation/Daedric" />Daedric : Price Feed SCORE </a> </h1>

[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

## Disclaimer
This is still a work in progress

## Introduction

**Marvin** is a price feed application (bot) designed to provide an hourly price update to a Daedric SCORE running on the ICON Public Blockchain (see here: https://github.com/iconation/Daedric). Marvin has been written in .Net Core 2.2 and is able to run on either Windows or Ubuntu.

*At the moment this guide assumes you already have a keystore file created. I am working on a utility app that will allow you to generate a new keystore/wallet and interact with basic features.*

## Table of Contents
  * [Ubuntu Setup](https://github.com/rhizomeicx/marvin#ubuntu)
    - [Prerequisites](https://github.com/rhizomeicx/marvin#prerequisites-ubuntu)
    - [Installation](https://github.com/rhizomeicx/marvin#installation-ubuntu)
    - [Configuration](https://github.com/rhizomeicx/marvin#configuration)
    - [Run](https://github.com/rhizomeicx/marvin#run-ubuntu)
  * [Windows Setup](https://github.com/rhizomeicx/marvin#windows)

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

## Configuration
Marvin is designed to be driven via some appconfig.

<pre>$ nano /Marvin/Marvin-Ubuntu/appsettings.json </pre>

default should look like this:
<pre> 
{
  "LogPath": "/home/usr/Marvin/logs/log.log",
  "Yeouido_Keystore": "/home/usr/Marvin/config/yeouido/keystore/operator.icx",
  "Yeouido_Daedric_Address": "cx58ca994194cf0c6a2a68b789d81c70484a5675b3",
  "Yeouido_url": "https://bicon.net.solidwallet.io/api/v3"
}
</pre>
Update the configuration to meet your needs

## Run-Ubuntu
I have yet to successfully run a .NET Core application as a background Daemon on Ubuntu so next best thing is to have this run from cron

<pre>$ crontab -e </pre>

see example of the crontab that is run every hour

<pre> 0 * * * * /usr/bin/dotnet /Marvin/Marvin-Ubuntu/bin/Release/netcoreapp2.2/ubuntu.16.04-x64/publish/Marvin-Ubuntu.dll "keystorepassword" </pre>

you can change this to run every minute to test it is working like so:

<pre> * * * * * /usr/bin/dotnet /Marvin/Marvin-Ubuntu/bin/Release/netcoreapp2.2/ubuntu.16.04-x64/publish/Marvin-Ubuntu.dll "keystorepassword" </pre>

## Windows WIP *************************************

## Prerequisites-Ubuntu

**[Install .NET Core SDK]**

download & install https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.401-windows-x64-installer

## Installation-Windows

open Command Prompt terminal and naviated to:
<pre>cd /Marvin/Marvin-Windows</pre>

**Build/Publish**
<pre>
dotnet build --runtime win-x64 --configuration Release
</pre>

**Create Windows Service**

see below for an example if Marvin was located in "F:\dev\Marvin"
<pre>
sc create Marvin binPath= "F:\dev\Marvin\Marvin\Marvin-Windows\bin\Release\netcoreapp2.2\win-x64\Marvin-Windows.exe --keystorepassword" start=auto  
</pre>



