using Virtual_Try_On_System.View.Buttons.Events;
using Virtual_Try_On_System.View_Model;
using Virtual_Try_On_System.Model.ClothingItems;
using Virtual_Try_On_System.View_Model.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Virtual_Try_On_System.View_Model.ButtonItems;


namespace Virtual_Try_On_System.View.Buttons
{
    class KinectScreenshotButton : KinectButton
    {
        
        // The timespan
        
        private const int Timespan = 3;

       
        // The screenshot timer
       
        private readonly DispatcherTimer _screenshotTimer;
        
        // The number of _screenshotTimer ticks
        
        private int _ticks;
      
        // The camera player.
      
        private SoundPlayer _cameraPlayer;

        
        // Initializes a new instance of the <see cref="KinectScreenshotButton"/> class.
        
        public KinectScreenshotButton()
            : base()
        {
            _cameraPlayer = new SoundPlayer(Properties.Resources.CameraClick);
            _screenshotTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
            _screenshotTimer.Tick += ScreenshotTimer_Tick;
        }
 
      
        // Handles the Tick event of the _screenshotTimer control.
       
        private void ScreenshotTimer_Tick(object sender, EventArgs e)
        {
        

            _ticks++;
            if (_ticks < Timespan)
                (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = (Timespan - _ticks) + "...";
            else
                (Application.Current.MainWindow as MainWindow).ScreenshotText.Text ="Smile";

            if (_ticks > Timespan)
            {
                _screenshotTimer.Stop();
                _ticks = 0;
                (Application.Current.MainWindow as MainWindow).ScreenshotGrid.Visibility = Visibility.Collapsed;
                MakeScreenshot();
            }
        }
       
        // Imitates the click event for KinectScreenshotButtun

        protected override void KinectButton_HandCursorClick(object sender, HandCursorEventArgs args)
        {
            SetValue(IsClickedProperty, true);

            (Application.Current.MainWindow as MainWindow).ScreenshotGrid.Visibility = Visibility.Visible;
            (Application.Current.MainWindow as MainWindow).ScreenshotText.Text = "3...";
            _screenshotTimer.Start();

            AfterClickTimer.Start();
        }
      
        // Makes the screenshot.
      
        private void MakeScreenshot()
        {
            int actualWidth = (int)(Application.Current.MainWindow as MainWindow).ImageArea.ActualWidth;
            int actualHeight = (int)(Application.Current.MainWindow as MainWindow).ImageArea.ActualHeight;
            int emptySpace = (int)(0.5 * (SystemParameters.PrimaryScreenWidth - actualWidth));

            string fileName = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss", CultureInfo.InvariantCulture) + ".png";
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Virtual Try On System");
            Directory.CreateDirectory(directoryPath);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(actualWidth + emptySpace, actualHeight, 96, 96,
                PixelFormats.Pbgra32);
            renderTargetBitmap.Render((Application.Current.MainWindow as MainWindow).ImageArea);
            renderTargetBitmap.Render((Application.Current.MainWindow as MainWindow).ClothesArea);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(new CroppedBitmap(renderTargetBitmap, new Int32Rect(emptySpace, 0, actualWidth, actualHeight))));
            using (Stream fileStream = File.Create(directoryPath + "\\" + fileName))
            {
                pngImage.Save(fileStream);
            }

            if (AreSoundsOn)
                _cameraPlayer.Play();
        }
       

       

    }

}
