using System;

[Serializable]
public class InvalidParametersException : Exception
{
    public InvalidParametersException() : base("Invalid Parameters") { }

    public InvalidParametersException(string name)
        : base(String.Format("Invalid Parameters: does not contain {0}", name))
    {

    }
}
