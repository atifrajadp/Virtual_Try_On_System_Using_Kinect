using System.ComponentModel;

namespace Virtual_Try_On_System.View_Model
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
     
        // Called when property changed
        
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
