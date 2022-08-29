using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace NoChannelsBot
{
    class Program
    {
        private static string token { get; set; } = "5686170940:AAFzWxhvdh1gpOXLzKxsibKSEUNWDeorRL8";
        private static long botId { get; set; } = 5686170940;
        private static TelegramBotClient bot;

        static void Main(string[] args)
        {
            bot = new TelegramBotClient(token);
            bot.StartReceiving();
            bot.OnMessage += OnMessageHandler;
            Console.Title = "AntiChannelsBot";
            Console.WriteLine("Bot was started!\n");
            Console.WriteLine("Now channels can't send messages in chats.");
            Thread.Sleep(Timeout.Infinite);
            bot.StopReceiving();
        }
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg != null)
            {
                if (msg.NewChatMembers != null)
                {
                    if (msg.NewChatMembers[0].Id == 5686170940)
                    {
                        await bot.SendTextMessageAsync(chatId: msg.Chat.Id, "Thanks for using!\nFor stable work promote me with right \"Delete messages\"");
                    }
                }
                if (msg.From.Id == 136817688)
                {
                    ChatMember[] admins = await bot.GetChatAdministratorsAsync(chatId: msg.Chat.Id);
                    if (admins.FirstOrDefault(a => { return a.User.Id != null && a.User.Id == botId; }) == null)
                    {
                        await bot.SendTextMessageAsync(chatId: msg.Chat.Id, "For deleting messages by channels promote me with right \"Delete messages\"");
                    }
                    else
                    {
                        await bot.DeleteMessageAsync(chatId: msg.Chat.Id, messageId: msg.MessageId);
                    }
                }

                if (msg.From.Id == msg.Chat.Id)
                {
                    await bot.SendTextMessageAsync(chatId: msg.Chat.Id, "I'm bot for deleting message from channels", replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("Add in chat", "http://t.me/AntiAnonimBot?startgroup=start&admin=delete_messages")));
                }
            }
        }
    }
}

