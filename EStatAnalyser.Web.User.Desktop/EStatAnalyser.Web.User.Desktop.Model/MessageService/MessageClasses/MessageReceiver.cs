using EStatAnalyser.Web.User.Desktop.Core.Interfaces;
using EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageBoxService;

namespace EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageClasses
{
    public class MessageReceiver
    {
        private readonly IMessageService _messageBus;

        public MessageReceiver(IMessageService messageBus)
        {
            _messageBus = messageBus;
            _messageBus.Subscribe((string message) =>
            {
                GetMessageBox.Show(message);
            });
        }
    }
}