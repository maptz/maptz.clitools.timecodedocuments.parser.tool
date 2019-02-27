FOR %%A IN (%*) DO (
	node "X:\+++DEV\MaptzBitBucket\jsnodetool\maptz.js.node.wordtotext\wordtotext.js" %%A
	dotnet run -p "X:\+++DEV\MaptzGitHub\netcoretools\maptz.avid.tools\Maptz.Avid.TimeCodeToAvidDS.Tool" --FilePath "%%~dpnA.txt" --Mode DS --FinalCutXmlSettings:Width 3840 --FinalCutXmlSettings:Height 2160 --MaxLineLength 60 --DefaultDurationFrames 200  --SourceEncoding UTF8 
)