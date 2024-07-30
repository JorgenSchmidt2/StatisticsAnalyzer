using EStatAnalyser.Web.User.Desktop.Core.Interfaces;

namespace EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageClasses
{
    public class MessageObjects
    {
        private static IMessageService MessageBus = new MessageService();

        public static MessageSender Sender = new MessageSender(MessageBus);
        public static MessageReceiver Receiver = new MessageReceiver(MessageBus);
    }
}