using Microsoft.Practices.Prism.Commands;
using System.Drawing;
using System.Windows.Input;
namespace Virtual_Try_On_System.View_Model.ButtonItems
{
    public abstract class ButtonViewModelBase : ViewModelBase
    {
        
        // The button's image
        
        private Bitmap _image;

      
        // Gets or sets the button's image.
       
        public Bitmap Image
        {
            get { return _image; }
            set
            {
                if (_image == value)
                    return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        
        // The command, executed after clicking on button
        
        private ICommand _clickCommand;
       
        // Gets the command.
        
        public ICommand ClickCommand
        {
            get { return _clickCommand ?? (_clickCommand = new DelegateCommand(ClickExecuted)); }
        }
       
        // Executes when button was hit.
       
        public abstract void ClickExecuted();
       
        // Plays the sound associated with the button when sounns are on.
        
        public void PlaySound()
        {
            if (TopMenuButtons.TopMenuManager.Instance.SoundsOn)
                KinectViewModel.ButtonPlayer.Play();
        }
        
        
    }
}
