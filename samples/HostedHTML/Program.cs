using WebWindows;

namespace HostedHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new WebWindow("WebWindow for Microsoft Teams :)");
            window.NavigateToUrl("https://teams.microsoft.com");
            window.WaitForExit();
        }
    }
}

