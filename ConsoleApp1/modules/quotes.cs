using System;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.modules
{
    public class quotes : ModuleBase<SocketCommandContext>
    {

        [Command(".")]
        public async Task SaveQuote(string name, [Remainder] string line)
        {
            string[] emotes = { "<:random:485494488106663936>", "<:uuuuhh:454457061204295685>", "<:gasm:481116052357644315>", "<:Azusa_Thumbsup:482698167553622021>", "<:loli_wink:482711189382496266>" };
            string StringedFile = string.Empty;
            Random rnd = new Random();
            int numb = rnd.Next(5);
            string QuoteFile = File.ReadAllText(@"C:\Users\jaafar\Desktop\New folder\ConsoleApp1\ConsoleApp1\quotes.json");
            List<string> AllQuotes = JsonConvert.DeserializeObject<List<string>>(QuoteFile);
            string LowerName = name.ToLower();
            AllQuotes.Add($"{LowerName} {line}");
            StringedFile = JsonConvert.SerializeObject(AllQuotes);
            File.WriteAllText(@"C:\Users\jaafar\Desktop\New folder\ConsoleApp1\ConsoleApp1\quotes.json", StringedFile);
            await ReplyAsync($"quoted! {emotes[numb]}");

            
        }

        [Command("..")]
        public async Task Quote(string name)
        {
            string QuoteFile = File.ReadAllText(@"C:\Users\jaafar\Desktop\New folder\ConsoleApp1\ConsoleApp1\quotes.json");
            String[] AllQuotes = JsonConvert.DeserializeObject<string[]>(QuoteFile);
            string LowerName = name.ToLower();
            Random rnd = new Random();
            int index = 0;
            bool flag = true;
            string line = string.Empty;
            while (flag)
            {
                index = rnd.Next(AllQuotes.Length);
                if (LowerName == AllQuotes[index].Split(' ').First())
                {
                    flag = false;
                    line = AllQuotes[index].Replace(LowerName, "");
                }
            }
            await ReplyAsync(line);


        }
    }
}
