########################################

function Copy-WithFullPath {
    param (
        [string]$Source,
        [string]$Destination
    )

    # Ensure the destination directory exists
    $destinationDir = Split-Path -Path $Destination
    if (!(Test-Path -Path $destinationDir)) {
        New-Item -ItemType Directory -Path $destinationDir -Force | Out-Null
    }

    # Copy the file and overwrite if necessary
    Copy-Item -Path $Source -Destination $Destination -Force
}

########################################

function New-SHA256Checksum {
    param(
        [Parameter(Mandatory=$true)]
        [string]$FilePath
    )

    if (-not (Test-Path -Path $FilePath -PathType Leaf)) {
        throw "Súbor nenájdený: $FilePath"
    }

    # Vypočíta hash, prevedie na lowercase hex
    $h = Get-FileHash -Path $FilePath -Algorithm SHA256
    $hex = $h.Hash.ToLower()

    # Iba názov súboru (bez cesty), tak ako to očakáva .SHA256 súbor
    $fileName = [System.IO.Path]::GetFileName($FilePath)

    # Výstupný súbor: pôvodná cesta + .SHA256
    $outPath = "$FilePath.SHA256"

    # Dve medzery medzi hashom a názvom súboru
    $line = "{0}  {1}" -f $hex, $fileName

    # Uložíme v ASCII (kompatibilné)
    $line | Out-File -FilePath $outPath -Encoding Ascii -Force

    return $outPath
}

########################################

$CERT_STRONG_NAME = $env:CERT_STRONG_NAME
Write-Host "CERT_STRONG_NAME=>$CERT_STRONG_NAME<"

$snPath = 'sn.exe'

########################################

$signtoolPath = 'signtool.exe'
$innoInstallPath = 'iscc'

$CERT_CODE = $env:CERT_CODE
Write-Host "CERT_CODE=>$CERT_CODE<"
$CERT_PWD = $env:CERT_PWD
Write-Host "CERT_PWD=>$CERT_PWD<"
$tag = git describe --tags --abbrev=0
Write-Output "TAG=>$tag<"

$paths = @(
    "FlowToDo\bin\x64\Release\net9.0-windows\FlowToDo.dll"
    "FlowToDo\bin\x64\Release\net9.0-windows\FlowToDo.exe"
)

foreach ($path in $paths) {
    Write-Output "################## Processing: $path"
    if (Test-Path $path) {
        Write-Output "################## STRONG NAME"
        #& $snPath -R "$path" $CERT_STRONG_NAME
        Write-Output "################## STRONG NAME VERIFY"
        #& $snPath -v "$path"
        Write-Output "################## SUBSCRIBE"
        & $signtoolPath sign /fd SHA256 /f "$CERT_CODE" /p $CERT_PWD /tr http://timestamp.digicert.com /td sha256 /v "$path"
        Write-Output "################## SUBSCRIBE VERIFY"
		& $signtoolPath verify /pa /v "$path"
    } else {
        Write-Output "Path does not exist: $path"
    }
}

########################################
Write-Output "################## BUILD PACKAGE"
$version = Get-Content -Path ".\version.txt" -TotalCount 1
Write-Output "################## VERSION $version" 
& $innoInstallPath /q FlowToDo.iss /DMyAppVersion=$version /DMyAppPublisher="Success Company, s.r.o." /DMyAppURL="https://pekand.com/page/FlowToDo"

########################################


$exeFiles = Get-ChildItem -Path Output -Filter *.exe -Recurse -File
foreach ($file in $exeFiles) {
    $TARGET=$file.FullName
    Write-Output "################## SUBSCRIBE PACKAGE $TARGET"
    & $signtoolPath sign /fd SHA256 /f "$CERT_CODE" /p $CERT_PWD /tr http://timestamp.digicert.com /td sha256 /v "$TARGET"
    Write-Output "################## SUBSCRIBE VERIFY"
    & $signtoolPath verify /pa /v "$TARGET"
    Write-Output "################## CREATE PACKAGE HASH"
    # $hash = Get-FileHash -Path $TARGET -Algorithm SHA256
    # $hash.Hash | Out-File -FilePath "$TARGET.SHA256"
    New-SHA256Checksum -FilePath $TARGET
}

Read-Host "Press Enter to continue"
