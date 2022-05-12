using System;

[Serializable]
public class InvalidRequestTokenException : Exception
{
    public InvalidRequestTokenException() : base("Invalid Response Services Token, does not contain properties") { }

    public InvalidRequestTokenException(string name)
        : base(string.Format("Invalid Response Services Token, does not contain properties {0}", name))
    {

    }
}
