param (
	[string]$ID,
    [string]$sourcePath,
    [string]$destinationPath
)

$rutaArchivoJson = "C:\LOGJ\state.json"
$rutaArchivoJson2 = "C:\LOGJ\state2.json"
$rutaArchivo = "C:\LOGJ\stop.txt"
$contenidoArchivo = Get-Content -Path $rutaArchivo
## primera parte
# Source and destination paths
$fichier = 0
$TotalFilesSize =0
# Get the list of files to copy

$filesToCopy = Get-ChildItem -Path $sourcePath -Recurse
foreach ($file in $filesToCopy) {

	$file2=$file.FullName -replace [regex]::Escape($sourcePath), ""
	$newfile=$destinationPath+$file2
	if (Test-Path $newfile -PathType Leaf) {
		if ($newfile.LastWriteTime -eq $file.LastWriteTime) {
		}
		else
		{
			$fichier++
			$TotalFilesSize=(Get-ChildItem -Path $newfile -Recurse | Measure-Object -Property Length -Sum).Sum + $TotalFilesSize

		}
	}
}
# Initialize progress bar
$filesToCopy = Get-ChildItem -Path $sourcePath
$progress = 0
$totalFiles = $filesToCopy.Count
##Write-Host $fichier
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
	do{
		Write-Host "Sigo Esperando"
		Write-Host $file
		$contenidoArchivo = Get-Content -Path $rutaArchivo 
	}while ($contenidoArchivo -ne "go")
	$file2=$file.FullName -replace [regex]::Escape($sourcePath), ""
	$newfile=$destinationPath+$file2
	$newfile2=Get-ChildItem -Path $newfile
	
	if ($newfile2.LastWriteTime -eq $file.LastWriteTime) {
		}
		else{
    $progress++
	
	
    $progressPercentage = ($progress / $fichier) * 100
	$EtatReel = @{
	IdEtaTemp = $ID
	NomETR  = "Save "+$ID
	SourceFilePath = $sourcePath
	TargetFilePath = $destinationPath
	State = "Active"
	TotalFilesToCopy = $fichier
	TotalFilesSize = $TotalFilesSize
	NbFilesLeftToDo = $fichier - $progress
	Progression = $progressPercentage
	}
	$jsonString = $EtatReel | ConvertTo-Json
    # Display progress bar
    #Write-Progress -Activity "Copying Files" -Status "Progress: $progress/$totalFiles" -PercentComplete $progressPercentage
	#Start-Sleep -Seconds 1
    # Copy the file
    Copy-Item -Path $file.FullName -Destination $destinationPath -Force
	$jsonString | Out-File -FilePath $rutaArchivoJson -Encoding UTF8 -Force
		}
}

$EtatReel = @{
	IdEtaTemp = $ID
	NomETR  = "Save "+$ID
	SourceFilePath = ""
	TargetFilePath = ""
	State = "End"
	TotalFilesToCopy = 0
	TotalFilesSize = 0
	NbFilesLeftToDo = 0
	Progression = 0
	}
$jsonString = $EtatReel | ConvertTo-Json
$jsonString | Out-File -FilePath $rutaArchivoJson2 -Append -Encoding UTF8




