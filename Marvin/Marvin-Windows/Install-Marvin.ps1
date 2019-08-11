$rootLocation = $PSScriptRoot
$logTime = "$(Get-Date -format 'u')"
$nl = "`r`n"


function Install-Marvin
{
  Write-Host "($logTime) : [INSTALLING Marvin] $n1" 

  $serviceName = "MarvinService"
  $exePath = "$PSScriptRoot\bin\Release\netcoreapp2.2\win-x64\publish\Marvin-Windows.exe"

  $existingService = Get-WmiObject -Class Win32_Service -Filter "Name='$serviceName'"

  Write-Host $exePath

  if($existingService)
  {
    Stop-Service $serviceName
    Start-Sleep -s 5

    $existingService.Delete()

    Start-Sleep -s 5
  }

  New-Service -BinaryPathName $exePath -Name $serviceName -DisplayName $serviceName -StartupType Automatic

  $serviceToStart = Get-WmiObject -Class Win32_Service -Filter "Name='$serviceName'"

  $serviceToStart.startservice()

} 

Install-Marvin