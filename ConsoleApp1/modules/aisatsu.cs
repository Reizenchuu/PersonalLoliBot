using System;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;



namespace ConsoleApp1.modules
{
    public class aisatsu : ModuleBase<SocketCommandContext>
    {
   
        [Command("sup")]
        public async Task GreetBack()
        {
            string[] SayHello =
             {
                   "hellow!",
                   "one-coin sachan desu!",
                   "heyheyhey",
                   "eating lolis for breakfast."
             };

            Random rnd = new Random();
            int index = rnd.Next(SayHello.Length);

            await ReplyAsync(SayHello[index] + "<:Smug_youko:455391241182380052>");
        }
    }
}
