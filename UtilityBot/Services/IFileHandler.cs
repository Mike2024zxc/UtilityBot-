using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using UtilityBot.Controllers;
using UtilityBot.Services;


namespace UtilityBot.Services
{
    public class IFileHandler
    {
        readonly ITelegramBotClient _telegramClient;
        readonly IStorage _memoryStorage;

        public IFileHandler(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (_memoryStorage.GetSession(message.Chat.Id).Code)
            {
                case Datas.TextLenght:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        $"Длина текста составляет {GetLenght(message.Text)} символов.",
                        cancellationToken: ct);
                    break;
                case Datas.Sum:
                    await GetSumm(message, ct);
                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                        "",
                        cancellationToken: ct);
                    break;
            }
        }

        string GetLenght(string text)
        {
            return text.Length.ToString();
        }

        async Task GetSumm(Message message, CancellationToken ct)
        {
            var nums = message.Text.Split(" ");
            var sum = 0;
            try
            {
                foreach (var num in nums)
                {
                    sum += Convert.ToInt32(num);
                }
                await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                    $"Cумма чисел: {sum.ToString()}.",
                    cancellationToken: ct);
            }
            catch (Exception)
            {
                await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                    $"Не верный формат сообщения.",
                    cancellationToken: ct);
            }
        }
    }
}
