using System;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

using Discord.Addons.Interactive;

namespace MyBot
{
    

    public class Program
    {

        public static DiscordSocketClient client;
        public static CommandService commands;
        public static IServiceProvider services;


        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .AddSingleton(new InteractiveService(client))
                .BuildServiceProvider();

            client.Log += Log;
            client.MessageReceived += CommandRecieved;

            commands = new CommandService();

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            string token = "NDg0MzE3MDI2NTQ3MDA3NDk5.DmnAFw.0A80-vp4Ub7_LAwcQ_PTotC14aM";
            await client.LoginAsync(Discord.TokenType.Bot, token);
            await client.StartAsync();

         


            await Task.Delay(-1);
        }

        private async Task CommandRecieved(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            string[] Agrees =
            {
                          "yes, onii-chan  <:random:485494488106663936>",
                          "of course :sip:",
                          "surely",
                          "soudayo~ :awoosmug: ",
                          "umu!",
                          "exactly, onii-chan",
                          "yeah, that's right!",
                          "onii-chan no iuu touri desu <:Uma_Tehe:483812422570999809>",
                          "yeap",
                          "It is indeed, hehe :neko_hehe: ",
                          "hmm, but I'm hungry.",
                          "one-coin sachan agrees."

            };
            string[] disagrees =
            {
                "no",
                "what? no.",
                "... faggot.",
                "why are you even asking me?",
                "whatever.",
                "fuck off.",
                "don't ask me.",
                "onii-chan, what does this person want from me?",
                "...",
                "die.",
                "feed me first.",
                "I don't care."

            };

            Random rnd = new Random();
            int index = rnd.Next(Agrees.Length);

            if (message == null || message.Author.IsBot) { return; }

            int argPos = 0;


            if (message.Content.Contains("right, Sachan?") || message.Content.Contains("right, sachan?"))
            {
                if (message.Author.Id == 186972985477824514)
                {
                    
                    await message.Channel.SendMessageAsync(Agrees[index]);
                }
                else
                {
                    await message.Channel.SendMessageAsync(disagrees[index]);
                }
            }

            if (message.Content == "Shika" || message.Content == "shika")
                await message.Channel.SendMessageAsync("Kamemushi");
            if (message.Content == "Kamemushi" || message.Content == "kamemushi")
                await message.Channel.SendMessageAsync("Shika");

            if (message.HasStringPrefix("sachan! ", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos) || (message.HasStringPrefix(".", ref argPos)))
            {
                
                var context = new SocketCommandContext(client, message);

                var result = await commands.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ErrorReason);
            }

           






        }


    }


}
