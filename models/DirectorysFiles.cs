
class DirectorysFiles
{
    public DirectorysFiles(string inD, string log, string move)
    {
        InDirectory = inD;
        LogDirectory = log;
        MoveDirectory = move;
    }

    public DirectorysFiles()
    {
        InDirectory = string.Empty;
        LogDirectory = string.Empty;
        MoveDirectory = string.Empty;
    }
    public string InDirectory { get; set; }
    public string LogDirectory { get; set; }
    public string MoveDirectory { get; set; }

    public bool Validate(DirectorysFiles data) {
        if (data.InDirectory == string.Empty)
            return false;
        if (data.LogDirectory == string.Empty)
            return false;
        if (data.MoveDirectory == string.Empty)
            return false;
        return true;
    }
}

