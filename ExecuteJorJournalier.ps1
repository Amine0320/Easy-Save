param (
	[string]$IdLogJourn,
    [string]$NomLj,
    [string]$FileSource,
	[string]$FileTarget,
	[string]$FileSize,
	[string]$FileTransferTime,
	[string]$Time
)

$rutaArchivoJson2 = "C:\LOGJ\2020-11-30.json"
$LogJournalier = @{
	IdLogJourn = $IdLogJourn
	NomLj  = $NomLj
	FileSource = $FileSource
	FileTarget = $FileTarget
	FileSize = $FileSize
	FileTransferTime = $FileTransferTime
	Time = $Time
	}
$jsonString = $LogJournalier | ConvertTo-Json
##Write-Host "La carpeta ya existe en $($EtatReel.SourceFilePath)"
$jsonString | Out-File -FilePath $rutaArchivoJson2 -Append -Encoding UTF8




