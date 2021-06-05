using System.Collections.ObjectModel;
using System.Windows;


namespace Virtual_Try_On_System.View_Model.ButtonItems.TopMenuButtons
{
    public sealed class TopMenuManager : ViewModelBase
    {
       
        // Only instance of TopMenuManager
      
        private static TopMenuManager _instance;
        
        // Information about sounds.
       
        private bool _soundsOn;
     
        // Actually displayed top menu buttons
       
        private ObservableCollection<TopMenuButtonViewModel> _actualTopMenuButtons;
      
        // All top menu buttons view
       
        private ObservableCollection<TopMenuButtonViewModel> _allButtons;

        // Clear buttons view
      
        private ObservableCollection<TopMenuButtonViewModel> _clearButtons;
       
        // Visibility of camera button
       
        private Visibility _cameraButtonVisibility;
       
        // Visibility of Order buttons
        private ObservableCollection<TopMenuButtonViewModel> _OrderButtons;

       // Ordering Buttons
        private ObservableCollection<TopMenuButtonViewModel> _OrderingButtons;
        
        // Visibility of CloseAppGrid

        private Visibility _quantityGridVisibility;

        private Visibility _OrderButtonsVisibility;

        private Visibility _confirmGridVisibility;



        public ObservableCollection<TopMenuButtonViewModel> OrderingButtons
        {
            get { return _OrderingButtons; }
            set
            {
                if (_OrderingButtons == value)
                    return;
                _OrderingButtons = value;
                OnPropertyChanged("OrderingButtons");
            }
        }

        public ObservableCollection<TopMenuButtonViewModel> OrderButtons
        {
            get { return _OrderButtons; }
        }
        
       
        // Method with access to only instance of TopMenuManager
       
        public static TopMenuManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TopMenuManager();
                return _instance;
            }
        }
       
        // Gets actual top menu buttons.
       
        public ObservableCollection<TopMenuButtonViewModel> ActualTopMenuButtons
        {
            get { return _actualTopMenuButtons; }
            set
            {
                if (_actualTopMenuButtons == value)
                    return;
                _actualTopMenuButtons = value;
                OnPropertyChanged("ActualTopMenuButtons");
            }
        }




        public Visibility OrderButtonsVisibility
        {
            get { return _OrderButtonsVisibility; }
            set
            {
                if (_OrderButtonsVisibility == value)
                    return;
                _OrderButtonsVisibility = value;
                OnPropertyChanged("OrderButtonsVisibility");
            }
        }
        // Gets all top menu buttons.
       
        public ObservableCollection<TopMenuButtonViewModel> AllButtons
        {
            get { return _allButtons; }
        }
      
        // Gets or sets main menu button
        
        public MenuButtonViewModel MenuButton
        {
            get;
            private set;
        }


        // Gets or sets Ok button
        public OkButtonViewModel OkButton
        {
            get;
            private set;
        }

        /// Gets or sets Plus button
    
        public PlusButtonViewModel PlusButton
        {
            get;
            private set;
        }
       
        
        // Gets clear buttons.
        
        public ObservableCollection<TopMenuButtonViewModel> ClearButtons
        {
            get { return _clearButtons; }
        }
       
        // Gets or sets camera button
       
        public ScreenshotButtonViewModel CameraButton
        {
            get;
            private set;
        }


      
        // Gets or sets the visibility of camera button
        
        public Visibility CameraButtonVisibility
        {
            get { return _cameraButtonVisibility; }
            set
            {
                if (_cameraButtonVisibility == value)
                    return;
                _cameraButtonVisibility = value;
                OnPropertyChanged("CameraButtonVisibility");
            }
        }

      
      
        // Gets or sets information about sounds.
        
        public bool SoundsOn
        {
            get { return _soundsOn; }
            set
            {
                if (_soundsOn == value)
                    return;
                _soundsOn = value;
                OnPropertyChanged("SoundsOn");
            }
        }
        
       
     
       
        // Gets or sets Quantity entering buttons button

        public Visibility QuantityGridVisibility
        {
            get { return _quantityGridVisibility; }
            set
            {
                if (_quantityGridVisibility == value)
                    return;
                _quantityGridVisibility = value;
                OnPropertyChanged("QuantityGridVisibility");
            }
        }
       
        // Private constructor of TopMenuManager. 
       
        private TopMenuManager()
        {
            InitializeTopMenuButtons();
            SoundsOn = true;
            CreateCloseButtons();
       
        }
       
        // Initializes buttons in top menu
       
        private void InitializeTopMenuButtons()
        {
            CreateAllTopMenuButtons();
            CreateClearButtons();
            CreateOrderButtons();
            OrderButtonsVisibility = Visibility.Collapsed;
        }
       
        // Creates basic top menu buttons
      
        private void CreateAllTopMenuButtons()
        {
           
            MenuButton = new MenuButtonViewModel(Properties.Resources.menu);
            CameraButton = new ScreenshotButtonViewModel(Properties.Resources.menu_camera);
            _allButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ClearItemsButtonViewModel(Properties.Resources.menu_clear),
                new SoundsButtonViewModel(Properties.Resources.menu_speaker),
                new OrderButtonViewModel(Properties.Resources.basket),
                 
            };
        }
       
        // Creates clear buttons
       
        private void CreateClearButtons()
        {
            _clearButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new ClearSetButtonViewModel(Properties.Resources.menu_clear_set),
            };
        }



        //Creates buttons used for Ordering
        private void CreateOrderButtons()
        {
            _OrderButtons = new ObservableCollection<TopMenuButtonViewModel>()
            {
                new SmallSizeButtonViewModel(Properties.Resources.small),
               new MediumSizeButtonViewModel(Properties.Resources.medium),
               new LargeSizeButtonViewModel(Properties.Resources.large),
               new ExtraLargeSizeButtonViewModel(Properties.Resources.extralarge)
            };
        }
       
        // Creates cancel and confirm closing buttons

        private void CreateCloseButtons()
        {
            OkButton = new OkButtonViewModel(Properties.Resources.Ok);
            PlusButton = new PlusButtonViewModel(Properties.Resources.plus);
            QuantityGridVisibility = Visibility.Collapsed;
        }  
  
    }
}
