using System.Text.RegularExpressions;


namespace Harvesting.UI.ManualConnection.Model
{
    public class ManualConnectionModel
    {
        public string ip;
        public string port;
        public string code;

        public string GetClearIP() => Regex.Replace(ip, "[^A-Za-z0-9.]", "");
        public string GetClearPort() => Regex.Replace(port, "[^A-Za-z0-9.]", "");
        public int GetPort() => int.Parse(GetClearPort());
    }
}