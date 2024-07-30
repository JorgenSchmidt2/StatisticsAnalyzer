using System.Windows;

namespace EStatAnalyser.Web.User.Desktop.Model.MessageService.MessageBoxService
{
    public static class GetMessageBox
    {
        public static void Show(string Message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(Message);
            });
        }
    }
}