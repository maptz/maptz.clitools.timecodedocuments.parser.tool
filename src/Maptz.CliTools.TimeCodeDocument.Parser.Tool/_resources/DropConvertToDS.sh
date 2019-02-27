#!/bin/bash
DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
for i in $*
do
    echo Converting file: $i
	dotnet "$DIR/Maptz.Avid.TimeCodeToAvidDS.Tool.dll" --FilePath "$i" --Overwrite true
done
