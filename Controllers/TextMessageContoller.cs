using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using SymbolBot.Services;

namespace SymbolBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly ICalculator _calculator;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, ICalculator calculator)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _calculator = calculator;
        }
        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                    InlineKeyboardButton.WithCallbackData($"Суммирование чисел", $"sum"),
                    InlineKeyboardButton.WithCallbackData($"Подсчёт символов в тексте", $"calc")
                    });
                    await
                        _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Выберите одну из функций бота:{Environment.NewLine}1) Сложить числа{Environment.NewLine}Пример ввода: 15 5 10" +
                        $"{Environment.NewLine}Результат действий бота: 30{Environment.NewLine}2) Подсчитать все символы в тексте{Environment.NewLine}Пример ввода: собака лает на сапог" +
                        $"{Environment.NewLine}Результат действий бота: 20", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new
                        InlineKeyboardMarkup(buttons));

                    break;
                default:
                    string calculationMode = _memoryStorage.GetSession(message.Chat.Id).CaltulationMode;

                    switch (calculationMode)
                    {
                        case "calc":
                            await
                            _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Длина сообщения: {_calculator.CalculateSymbols(message.Text)} знаков", cancellationToken: ct);
                            break;
                        case "sum":
                            await
                            _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {_calculator.Sum(message.Text)}", cancellationToken: ct);
                            break;
                    }
                    
                        
                    break;
            }
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
        }
    }
}
