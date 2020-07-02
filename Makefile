nuget/%: pack
	echo "Update the version number in VS first! (project properties/application -> Assembly Information)"
	cd SeleniumHelpers && nuget push SeleniumHelpers.BasicActions.$*.nupkg oy2bpeclo5zytxzvsn6qczdssrje2zqelija2v2fjw4j4q -Source https://api.nuget.org/v3/index.json

pack:
	cd SeleniumHelpers && nuget pack
