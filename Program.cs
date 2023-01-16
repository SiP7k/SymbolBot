using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Telegram.Bot;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SymbolBot.Controllers;
using SymbolBot.Services;

namespace SymbolBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            ConfigureServices(services)).
            UseConsoleLifetime().Build();

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }
        static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStorage, Storage>();
            services.AddSingleton<ICalculator, Calculator>();

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5973790626:AAFRj3R0rA8ijp1sBvlWDwZ-F4O1mGMIfH4"));
            services.AddHostedService<Bot>();
        }
    }
}
