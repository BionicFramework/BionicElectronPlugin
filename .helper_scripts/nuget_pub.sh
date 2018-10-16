#!/usr/bin/env bash

source ~/.nuget_bionic_key

DIR=$(dirname "$0")

if [ -z "$1" ]
  then
    echo "No version supplied"
    exit 1
fi

if [ -z "${NUGET_BIONIC_KEY}" ]
  then
    echo "No NuGet key found for Bionic"
    exit 1
fi

${DIR}/build_release.sh

echo "Pushing BionicElectronPlugin.$1.nupkg to NuGet..."
dotnet nuget push ./BionicElectronPlugin/nupkg/BionicElectronPlugin.$1.nupkg --source https://api.nuget.org/v3/index.json --api-key ${NUGET_BIONIC_KEY}
if [ $? = 0 ]
  then
    echo "Completed pushing BionicElectronPlugin.$1.nupkg to NuGet. Should be available in a few minutes."
fi

echo "Pushing BionicElectronTemplate.$1.nupkg to NuGet..."
dotnet nuget push ./BionicElectronTemplate/nupkg/BionicElectronTemplate.$1.nupkg --source https://api.nuget.org/v3/index.json --api-key ${NUGET_BIONIC_KEY}
if [ $? = 0 ]
  then
    echo "Completed pushing BionicElectronTemplate.$1.nupkg to NuGet. Should be available in a few minutes."
fi
