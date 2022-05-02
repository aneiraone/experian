using Common;

interface IEmail
{
    bool Send(ResponseCarga data);
    public bool Send(string mensaje);
}