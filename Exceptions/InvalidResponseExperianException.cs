using System;

[Serializable]
public class InvalidResponseExperianException : Exception
{
    public InvalidResponseExperianException() : base("Invalid Response does not contain payload") { }

    public InvalidResponseExperianException(string name)
        : base(String.Format("Invalid Response: {0}", name))
    {

    }
}
