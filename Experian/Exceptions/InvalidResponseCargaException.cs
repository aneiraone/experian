using System;

[Serializable]
public class InvalidResponseCargaException : Exception
{
    public InvalidResponseCargaException() : base("Invalid response carga document") { }

    public InvalidResponseCargaException(string name)
        : base(String.Format("Invalid response carga document: {0}", name))
    {

    }
}
