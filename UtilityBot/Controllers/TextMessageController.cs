using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using UtilityBot.Models;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{

    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _iFileHandler;
        public TextMessageController(ITelegramBotClient telegramBotClient, IFileHandler iFileHandler)
        {
            _telegramClient = telegramBotClient;
            _iFileHandler = iFileHandler;  
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Кол-во символов" , Datas.TextLenght),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел " , Datas.Sum)
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот обрабатывает текст.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно выбрать посчитать кол-во символов или сумму чисел.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    await _iFileHandler.Handle(message, ct);
                    break;
            }
        }
    }
}