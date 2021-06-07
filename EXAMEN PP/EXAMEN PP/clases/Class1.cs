using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using EXAMEN_PP.clases;
using System.Data;

namespace EXAMEN_PP.clases
{
    class Class1
    {
        private static TelegramBotClient Bot;

        public async Task IniciarTelegram()
        {
            //bot le pasamos el token
            Bot = new TelegramBotClient("1898648504:AAFCFO2ABcCynypX1diYfnmvDwQ6oAhPU4M");

            var me = await Bot.GetMeAsync();
            //le cambiamos el nombre del usuario
            Console.Title = me.Username;

            //configurar el bot que cuando llegue el mensaje llame al metodo cuando recive menjaes
            Bot.OnMessage += BotCuandoRecibeMensajes;
            Bot.OnMessageEdited += BotCuandoRecibeMensajes;
            Bot.OnReceiveError += BotOnReceiveError; //si hay error 


            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"escuchando solicitudes del BOT @{me.Username}");

            Console.ReadLine();
            Bot.StopReceiving();
        }

        // cuando recibe mensajes
        private static async void BotCuandoRecibeMensajes(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text)
                return;

            switch (message.Text.Split(' ').First())
            {
                // Send inline keyboard
                case "/Pizza":
                    await Pizza (message);
                    break;

                // send custom keyboard
                case "/Hamburguesa":
                    await Hamburguesa(message);
                    break;

                // send a photo
                case "/Papas":
                    await PapasFritas(message);
                    break;

                // request location or contact
                case "/Tacos":
                    await Tacos(message);
                    break;

                default:
                    await Usage(message);
                    break;
            }

        } // fin del metodo de recepcion de mensajes

        static async Task Pizza(Message message)
        {
            ClsConexion conexion = new ClsConexion (message.Chat.Username);

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                    new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Pizza Grande Q100.00", "Pizza Mediana Q50.00" },
                    },
                    resizeKeyboard: true
                );

            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Elija una opción",
                replyMarkup: replyKeyboardMarkup
            );

        }
        static async Task Hamburguesa(Message message)
        {

            //ClsConexion conexion = new ClsConexion(message.Chat.Username);
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
        new KeyboardButton[][]
        {
                        new KeyboardButton[] { "Hamburguesa Grande Q25.00", "Hamburgusa Mediana Q15.00" },

        },
        resizeKeyboard: true
    );

            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Elija una opción",
                replyMarkup: replyKeyboardMarkup

            );
        }
        static async Task PapasFritas(Message message)
        {

            //ClsConexion conexion = new ClsConexion(message.Chat.Username);

            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                    new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Papas Grandes Q15.00", "Papas Medianas Q10.00" },
                   
                    },
                    resizeKeyboard: true
                );

            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Elija una opción",
                replyMarkup: replyKeyboardMarkup
            );
        }
        static async Task Tacos(Message message)
        {

            //ClsConexion conexion = new ClsConexion(message.Chat.Username);
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
        new KeyboardButton[][]
        {
                        new KeyboardButton[] { "Grande Q100.00", "Mediana Q50.00" },

        },
        resizeKeyboard: true
    );

            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Eliga una opción",
                replyMarkup: replyKeyboardMarkup

            );

        }
        static async Task Usage(Message message)
        {

            //ClsConexion conexion = new ClsConexion(message.Chat.Username);

            const string usage = "Hola:\n" +
                                        "¿Qué desea ordenar?\n" +
                                        "/Pizza\n" +
                                        "/Hamburguesa\n" +
                                        "/Papas\n" +
                                        "/Tacos";
            await Bot.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove()
            );
        }


        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("UPS!!! Recibo un error!!!: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message
            );
        }
    }
}

