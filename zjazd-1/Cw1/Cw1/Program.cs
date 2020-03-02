using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                throw new ArgumentNullException("Nie podano argumentu");
            }
            if (new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", RegexOptions.IgnoreCase).Match(args[0]).Success == false)
            {
                throw new ArgumentException("Podany adres url jest niepoparwny");
            }
            using (var client = new HttpClient())
            {
                HttpResponseMessage result;
                try
                {
                    result = await client.GetAsync(args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Wystąpił bład podczas ładowania strony.");
                    return;
                }
                if (result.IsSuccessStatusCode)
                {
                    var emailPattern = "[a-z0-9]+@[a-z0-9]+\\.[a-z]+";
                    var regex = new Regex(emailPattern, RegexOptions.IgnoreCase);

                    var pageContent = await result.Content.ReadAsStringAsync();

                    MatchCollection matches = regex.Matches(pageContent);
                    Console.WriteLine($"Znaleziono: {matches.Count} adresy/ów email.");

                    for (int i = 0; i < matches.Count; i++)
                    {
                        Console.WriteLine($"{i}. {matches[i]}");
                    }
                }
            }
            Console.ReadKey();

        }
    }
}