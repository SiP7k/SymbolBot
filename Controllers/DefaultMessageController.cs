using Telegram.Bot.Types;
using Telegram.Bot;

namespace SymbolBot.Controllers
{
    internal class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено странное сообщение, пришлите боту текстовое сообщение для его корректной работы", cancellationToken: ct);
        }
    }
}