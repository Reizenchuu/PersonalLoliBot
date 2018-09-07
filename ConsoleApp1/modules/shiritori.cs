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
using Discord.Addons.Interactive;
using PlatformSpellCheck;



namespace ConsoleApp1.modules
{
    public class shiritori : InteractiveBase
    {

        

        [Command("shiritori", RunMode = RunMode.Async)]
        public async Task play()
        {

            

            string wordsStr = File.ReadAllText(@"C:\Users\jaafar\Desktop\New folder\ConsoleApp1\ConsoleApp1\words.json");
            string word = string.Empty;
            string Alphabets = "abcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();
            Console.WriteLine("loaded the words!");
            var spelling = new SpellChecker();
            

            Words words = JsonConvert.DeserializeObject<Words>(wordsStr);
            
            string[][] Walphabets = { words.a, words.b, words.c, words.d, words.e, words.f, words.g, words.h, words.i, words.j, words.k, words.l, words.m, words.n, words.o, words.p, words.q, words.r, words.s, words.t, words.u, words.v, words.w, words.x, words.y, words.z };

            int index = rnd.Next(26);
            int fixedrnd = rnd.Next(Walphabets[index].Length);
            await ReplyAsync(Walphabets[index][fixedrnd]);
            bool okay = true;
            var response = await NextMessageAsync();
            string BotReply = Walphabets[index][fixedrnd];

            while (!response.Content.Contains(" "))
            {
                

                foreach (var mistake in spelling.Check(response.Content))
                {
                    if (mistake.StartIndex == 0)
                    {
                        okay = false;
                        break;
                    }
                }

                if (okay == false)
                {
                    await ReplyAsync("that's not a word! You lose.");
                    break;
                }

                // check if the last letter inputed by user = that of the last word.

                else if (BotReply[BotReply.Length - 1] != response.Content[0])
                {
                    
                    await ReplyAsync($"that word doesn't start with **{BotReply[BotReply.Length - 1]}**, Kys, you lose.");
                    break;
                }


                index = 0;
                char Lettre = response.Content[response.Content.Length - 1];
               
                //to get the index of the lettle and access it in Json.
                foreach (char c in Alphabets)
                {
                    if (c == Lettre)
                    {
                        break;
                    }
                    index++;
                }
                BotReply = Walphabets[index][rnd.Next(Walphabets[index].Length)];
                await ReplyAsync(BotReply);
                
                response = await NextMessageAsync();
            }

            await ReplyAsync("good game.");

            

          

            foreach (var mistake in spelling.Check("problem"))
            {
                Console.WriteLine(mistake.StartIndex);
            }













        }

        

    }

    class Words
    {
        public string[] a { get; set; }
        public string[] b { get; set; }
        public string[] c { get; set; }
        public string[] d { get; set; }
        public string[] e { get; set; }
        public string[] f { get; set; }
        public string[] g { get; set; }
        public string[] h { get; set; }
        public string[] i { get; set; }
        public string[] j { get; set; }
        public string[] k { get; set; }
        public string[] l { get; set; }
        public string[] m { get; set; }
        public string[] n { get; set; }
        public string[] o { get; set; }
        public string[] p { get; set; }
        public string[] q { get; set; }
        public string[] r { get; set; }
        public string[] s { get; set; }
        public string[] t { get; set; }
        public string[] u { get; set; }
        public string[] v { get; set; }
        public string[] w { get; set; }
        public string[] x { get; set; }
        public string[] y { get; set; }
        public string[] z { get; set; }

    }
}
