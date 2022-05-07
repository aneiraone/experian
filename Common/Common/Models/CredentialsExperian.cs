namespace Common
{
    public class CredentialsExperian
    {
        public string username { get; set; } = Parametros.GetInstance().TokenNameExperian;
        public string password { get; set; } = Parametros.GetInstance().TokenPassExperian;
    }
}
