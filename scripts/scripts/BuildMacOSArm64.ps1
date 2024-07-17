[string] $Product = "LiteCosmosExplorer"
[string] $Target = "MacOSArm64"

& "$PSScriptRoot\Build.ps1" -Product $Product -Target $Target -BuildArgs "-t:BundleApp -p:RuntimeIdentifier=osx-arm64 -p:PublishTrimmed=true -p:IncludeAllContentForSelfExtract=true" -ProjectPath "CosmosExplorer.Avalonia\$CosmosExplorer.Avalonia.csproj"

# Remove everything except the app bundle
Get-Childitem "$PSScriptRoot\..\bld\$Product.$Target\" -Exclude "LiteCosmosExplorer.app" | Remove-Item -Recurse

& "$PSScriptRoot\Build.ps1" -Product $Product -Target $Target -Clean $False -BuildArgs "-t:Publish -p:RuntimeIdentifier=osx-arm64 -p:PublishSingleFile=true -p:PublishTrimmed=true -p:IncludeAllContentForSelfExtract=true -p:EnableCompressionInSingleFile=true"

# Remove unbundled 
Remove-Item "$PSScriptRoot\..\bld\$Product.$Target\LiteCosmosExplorer"

& "$PSScriptRoot\TarRelease.ps1" -Product $Product -Target $Target
