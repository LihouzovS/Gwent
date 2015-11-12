
        $VerFromAssemblyInfo = get-content SharedAssemblyInfo.cs
        $InstallScript = get-content Install\Gwent.iss
        $i = 27
		$verString = ""
        do
        	{
        	$verString = $verString + $VerFromAssemblyInfo[38][$i]
        	$i = $i + 1
        	}
        while($VerFromAssemblyInfo[38][$i] -ne ')')
        $verString
        $InstallScript[2]
        $InstallScript[2] = "#define Version " + $verString
        $InstallScript[2]
        $InstallScript | set-content Install\Gwent.iss