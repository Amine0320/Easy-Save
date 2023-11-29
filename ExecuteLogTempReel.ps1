param (
    [string]$nbr,
    [string]$sourcePath,
    [string]$destinationPath
)

# Source and destination paths
$rutaArchivoJson = "C:\LOGJ\state.json"
# Get the list of files to copy
$filesToCopy = Get-ChildItem -Path $sourcePath

# Initialize progress bar
$progress = 0
$totalFiles = $filesToCopy.Count

# Verificar si la carpeta ya existe
if (-not (Test-Path $destinationPath -PathType Container)) {
    # Si no existe, crear la carpeta
    New-Item -Path $destinationPath -ItemType Directory
    #Write-Host "Carpeta creada en $destinationPath"
} else {
    #Write-Host "La carpeta ya existe en $destinationPath"
}

# Copy files with progress
foreach ($file in $filesToCopy) {
    $progress++
    $progressPercentage = ($progress / $totalFiles) * 100
	$EtatReel = @{
	IdEtaTemp = $nbr
	NomETR  = "Save" + $nbr
	SourceFilePath = $sourcePath
	TargetFilePath = $destinationPath
	State = "Active"
	TotalFilesToCopy = (Get-ChildItem -Path $sourcePath -File -Recurse).Count
	TotalFilesSize = (Get-ChildItem -Path $sourcePath -Recurse | Measure-Object -Property Length -Sum).Sum
	NbFilesLeftToDo = (Get-ChildItem -Path $sourcePath -File).Count - (Get-ChildItem -Path $destinationPath -File).Count
	Progression = $progressPercentage
	}
	$jsonString = $EtatReel | ConvertTo-Json
    # Display progress bar
    #Write-Progress -Activity "Copying Files" -Status "Progress: $progress/$totalFiles" -PercentComplete $progressPercentage

    # Copy the file
    Copy-Item -Path $file.FullName -Destination $destinationPath -Force
	$jsonString | Out-File -FilePath $rutaArchivoJson -Encoding UTF8 -Force
}
$EtatReel = @{
	IdEtaTemp = $nbr
	NomETR  = "Save" + $nbr
	SourceFilePath = ""
	TargetFilePath = ""
	State = "End"
	TotalFilesToCopy = 0
	TotalFilesSize = 0
	NbFilesLeftToDo = 0
	Progression = 0
	}
$jsonString = $EtatReel | ConvertTo-Json
$jsonString | Out-File -FilePath $rutaArchivoJson -Encoding UTF8 -Force
# Clear the progress bar
Write-Progress -Activity "Copying Files" -Completed



