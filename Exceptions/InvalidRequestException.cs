using System;

[Serializable]
public class InvalidRequestException : Exception
{
    public InvalidRequestException() : base("Invalid Request does not contain payload") { }

    public InvalidRequestException(string name)
        : base(String.Format("Invalid Request: does not contain {0}", name))
    {

    }
}
