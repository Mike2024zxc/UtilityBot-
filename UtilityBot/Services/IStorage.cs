﻿using System;
using System.Collections.Generic;
using System.Text;
using UtilityBot.Models;

namespace UtilityBot.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }

}
