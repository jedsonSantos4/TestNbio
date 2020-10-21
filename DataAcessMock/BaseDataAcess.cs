using Models.MSG;

namespace DataAcessMock
{
    public class BaseDataAcess
    { 
        public string MSGErro(string msg, string url)
        {
            if (string.IsNullOrEmpty(msg))
                msg = $"Erro tratar a chamada do arquivo:> {url} ";
            else
                msg += string.Format(MSG.Erro, msg, url);
            return msg;
        }
    }
}
