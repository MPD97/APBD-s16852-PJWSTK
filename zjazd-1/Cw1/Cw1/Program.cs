using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
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
                catch (HttpRequestException)
                {
                    Console.WriteLine("Błąd w czasie pobierania strony");
                    return;
                }
                if (result.IsSuccessStatusCode)
                {
                    var emailPattern = "[a-z0-9]+@[a-z0-9]+\\.[a-z]+";
                    var regex = new Regex(emailPattern, RegexOptions.IgnoreCase);

                    var pageContent = await result.Content.ReadAsStringAsync();

                    MatchCollection matches = regex.Matches(pageContent);
                    if (matches.Count == 0)
                    {
                        Console.WriteLine("Nie znaleziono adresów email");
                    }
                    else
                    {
                        Console.WriteLine($"Znaleziono: {matches.Count} adresy/ów email.");

                        HashSet<string> uniqeSet = new HashSet<string>();
                        for (int i = 0; i < matches.Count; i++)
                        {
                            uniqeSet.Add(matches[i].ToString());
                        }
                        foreach (string email in uniqeSet)
                        {
                            Console.WriteLine(email);
                        }
                    }
                }
            }
            Console.ReadKey();

        }
    }
}