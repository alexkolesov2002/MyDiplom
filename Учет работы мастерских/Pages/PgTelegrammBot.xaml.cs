using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telegram.Bot;
using Telegram.Bot.Types;

using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;

namespace Учет_работы_мастерских
{
    /// <summary>
    /// Логика взаимодействия для PgSelectWork.xaml
    /// </summary>
    public partial class PgTelegrammBot : Page, INotifyPropertyChanged
    {
        public static string RichText;





        public PgTelegrammBot()
        {
            InitializeComponent();
            startTG();
          

        }

        public event PropertyChangedEventHandler PropertyChanged;

        static ITelegramBotClient bot = new TelegramBotClient("5266343401:AAE0lhwQEp4UYSAtbPeCe7lt66_zGVNF4WY");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            RichText = (Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update != null)
            {
                var message = update.Message;
                try
                {

                    if (message.Text != null && message.Text.ToLower() == "/start")
                    {
                        REST.Get().GetAwaiter().GetResult();
                        await botClient.SendTextMessageAsync(message.Chat, Bufstring.TGstring);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await botClient.SendTextMessageAsync(message.Chat, ex.ToString());
                }
            }
            else
            {
                var message = update.Message;
                await botClient.SendTextMessageAsync(message.Chat, "Поменяй формат");
            }

        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            RichText = (Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public void startTG()
        {
            try
            {
                Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = { }, // receive all update types
                };
                bot.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken

                );
                Txt.Text = RichText;
            }
            catch (Exception ex)
            {

            }

        }
       
    }
    
}
