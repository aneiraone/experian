using System;

[Serializable]
public class InvalidRequestTokenException : Exception
{
    public InvalidRequestTokenException() : base("Invalid Request does not contain properties") { }

    public InvalidRequestTokenException(string name)
        : base(String.Format("Invalid Request does not contain properties {0}", name))
    {

    }
}
