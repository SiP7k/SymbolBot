using SymbolBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace SymbolBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).CaltulationMode = callbackQuery.Data;

            string calculationMode = callbackQuery.Data
                switch
            {
                "calc" => "Подсчёт_символов",
                "sum" => "Суммирование_чисел",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Режим работы бота - {calculationMode}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.",
                cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
