

using Discord;
using Discord.Commands;
using System.Threading.Tasks;



namespace ConsoleApp1.modules
{
    public class purge : ModuleBase<SocketCommandContext>
    {
        [Command("purge"), Alias("p")]
        [RequireBotPermission(GuildPermission.Administrator)]
        public async Task Purge(int LinesNumber)
        {
            int i = 0;
            var items = await Context.Channel.GetMessagesAsync(LinesNumber).Flatten();
            await Context.Channel.DeleteMessagesAsync(items);
        }

    }
}
