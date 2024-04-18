using System;
using System.Collections.Generic;
using System.Text;
using UtilityBot.Models;
using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using UtilityBot.Controllers;
using UtilityBot.Services;


namespace UtilityBot.Models
{
    internal static class Enter
    {
        public static async Task SendInstruction(ITelegramBotClient telegramClient,long id, Session session, CancellationToken ct)
        {
            switch (session.Code)
            {
                case Datas.TextLenght:
                    await telegramClient.SendTextMessageAsync(id,"Введите текстовое сообщение:",cancellationToken: ct);
                    break;
                case Datas.Sum:
                    await telegramClient.SendTextMessageAsync(id,"Введите числа:",cancellationToken: ct);
                    break;
                default:
                    await telegramClient.SendTextMessageAsync(id,"", cancellationToken: ct);
                    break;
            }
        }
    }
}
