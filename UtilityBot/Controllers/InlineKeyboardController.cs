using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;
using UtilityBot.Controllers;
using UtilityBot.Models;
using UtilityBot.Services;
using Telegram.Bot.Types.Enums;

namespace UtilityBot.Controllers
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

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).Code = callbackQuery.Data;

            // Генерим информационное сообщение
            string code = callbackQuery.Data switch
            {
                Datas.TextLenght => "Посчитать длину текста",
                Datas.Sum => "Сложить числа",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Вы выбрали - {code}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);

            await Enter.SendInstruction(_telegramClient,callbackQuery.From.Id, _memoryStorage.GetSession(callbackQuery.From.Id), ct);
        }
    }
}