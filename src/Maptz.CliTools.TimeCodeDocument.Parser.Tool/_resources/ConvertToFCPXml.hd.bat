FOR %%A IN (%*) DO (
	node "X:\+++DEV\MaptzBitBucket\jsnodetool\maptz.js.node.wordtotext\wordtotext.js" %%A
	dotnet run -p "X:\+++DEV\MaptzGitHub\netcoretools\maptz.avid.tools\Maptz.Avid.TimeCodeToAvidDS.Tool" --FilePath "%%~dpnA.txt" --Mode FinalCutXml --FinalCutXmlSettings:Width 1920 --FinalCutXmlSettings:Height 1080 --MaxLineLength 60 --DefaultDurationFrames 200 --OutputFilePath "%%~dpnA.hd.xml"
)