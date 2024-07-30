using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EStatAnalyser.Web.User.Desktop.UI.AppService
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void CheckChanges([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}