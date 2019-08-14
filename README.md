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


## INSTALLATION-Ubuntu

<pre> cd /Marvin/Marvin-Ubuntu </pre>

**Build/Publish**
<pre>
dotnet build --runtime ubuntu.16.04-x64 --configuration Release
</pre>




## RUN INSTRUCTIONS : TODO
