using Domain.Entities;
using Domain.Enum;
using Infra.UnitOfWork;
using Services.FanService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace CeilingFanDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region [Fields]

        private Fan fan = new Fan();
        private IUnitOfWork uow;

        private string imageFanSource = "pack://siteoforigin:,,,/images/fan-forward.gif";


        //public string bitmapCord = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/images/fan-forward.gif"));
        public string bitmapCord = @"pack://siteoforigin:,,,/images/cord.png";
        public string bitmapPullCord = @"pack://siteoforigin:,,,/images/cordHandle.png";
        private string bitmapEmpty = null;//new BitmapImage(new Uri(@"fds"));
        #endregion

        #region [Properties]

        public string ImageFanSource
        {
            get => imageFanSource;
            set
            {
                imageFanSource = value;
                OnPropertyChanged("ImageFanSource");
            }
        }


        public string BitmapCord
        {
            get => @"pack://siteoforigin:,,,/images/cord.png";
        }

        public string BitmapCordHandle
        {
            get => @"pack://siteoforigin:,,,/images/cordHandle.png";
        }
        #endregion

        #region [Constructor]

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uow">dependency injections of Unit of work</param>
        public MainWindow(IUnitOfWork uow)
        {
            //this.mapper = mapper;
            this.uow = uow;
            InitializeComponent();
        }

        #endregion


        private void btnSpeedCord_Click(object sender, RoutedEventArgs e)
        {
            IFanService fanService = FanServiceFactory.createInstance(this.uow);

            fanService.changeFanSpeed(this.fan);

            this.updadteUI();

        }

        private void btnDirectionCord_Click(object sender, RoutedEventArgs e)
        {
            IFanService fanService = FanServiceFactory.createInstance(this.uow);

            fanService.changeFanDirection(this.fan);

            this.updadteUI();
        }


        private void updadteUI()
        {
            //Update Fan Speed GIF
            switch (this.fan.Speed)
            {
                case FanSpeed.SPEED_1:
                    imgFan.SetValue(WpfAnimatedGif.ImageBehavior.AnimationSpeedRatioProperty, 1.0);
                    break;
                case FanSpeed.SPEED_2:
                    imgFan.SetValue(WpfAnimatedGif.ImageBehavior.AnimationSpeedRatioProperty, 1.8);
                    break;
                case FanSpeed.SPEED_3:
                    imgFan.SetValue(WpfAnimatedGif.ImageBehavior.AnimationSpeedRatioProperty, 3.0);
                    break;
                case FanSpeed.SPEED_0:
                default:
                    imgFan.SetValue(WpfAnimatedGif.ImageBehavior.AnimationSpeedRatioProperty, 0.00000001);
                    break;
            }

            // Update Fan Direction Arrows
            ScaleTransform flipTrans = new ScaleTransform();
            switch (this.fan.Direction)
            {
                case FanDirection.REVERSE:
                    flipTrans.ScaleX = -1;
                    this.imgArrow1.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.imgArrow1.RenderTransform = flipTrans;
                    this.imgArrow2.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.imgArrow2.RenderTransform = flipTrans;


                    this.ImageFanSource = "pack://siteoforigin:,,,/images/fan-reverse.gif";
                    break;
                case FanDirection.FORWARD:
                default:
                    flipTrans.ScaleX = 1;
                    this.imgArrow1.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.imgArrow1.RenderTransform = flipTrans;
                    this.imgArrow2.RenderTransformOrigin = new Point(0.5, 0.5);
                    this.imgArrow2.RenderTransform = flipTrans;
                    this.ImageFanSource = "pack://siteoforigin:,,,/images/fan-forward.gif";
                    break;
            }

            //Update Speed Pull Cord

            if(this.fan.Speed == FanSpeed.SPEED_0)
            {
                this.imgSpeed0_PullCord.Visibility = Visibility.Visible;
                this.imgSpeed0_Cord.Visibility = Visibility.Hidden;
                //this.imgSpeed0.Source = this.bitmapPullCord;
                this.imgSpeed1_PullCord.Visibility = Visibility.Hidden;
                this.imgSpeed1_Cord.Visibility = Visibility.Hidden;

                this.imgSpeed2_PullCord.Visibility = Visibility.Hidden;
                this.imgSpeed2_Cord.Visibility = Visibility.Hidden;

                this.imgSpeed3_PullCord.Visibility = Visibility.Hidden;
            }

            if (this.fan.Speed == FanSpeed.SPEED_1)
            {
                this.imgSpeed0_PullCord.Visibility = Visibility.Hidden;
                this.imgSpeed0_Cord.Visibility = Visibility.Visible;
                //this.imgSpeed0.Source = this.bitmapCord;
                //this.imgSpeed1.Source = this.bitmapPullCord;

                this.imgSpeed1_PullCord.Visibility = Visibility.Visible;
                this.imgSpeed1_Cord.Visibility = Visibility.Hidden;
            }

            if (this.fan.Speed == FanSpeed.SPEED_2)
            {

                //this.imgSpeed1.Source = this.bitmapCord;
                //this.imgSpeed2.Source = this.bitmapPullCord;

                this.imgSpeed1_PullCord.Visibility = Visibility.Hidden;
                this.imgSpeed1_Cord.Visibility = Visibility.Visible;

                this.imgSpeed2_PullCord.Visibility = Visibility.Visible;
                this.imgSpeed2_Cord.Visibility = Visibility.Hidden;
            }

            if (this.fan.Speed == FanSpeed.SPEED_3)
            {

                //this.imgSpeed2.Source = this.bitmapCord;
                //this.imgSpeed3.Source = this.bitmapPullCord;

                this.imgSpeed2_PullCord.Visibility = Visibility.Hidden;
                this.imgSpeed2_Cord.Visibility = Visibility.Visible;

                this.imgSpeed3_PullCord.Visibility = Visibility.Visible;
            }

            //Update Speed Direction Cord
            if(this.fan.Direction == FanDirection.REVERSE)
            {
                this.imgDirectionForward_PullCord.Visibility = Visibility.Hidden;
                this.imgDirectionForward_Cord.Visibility = Visibility.Visible;

                this.imgDirectionReverse_PullCord.Visibility = Visibility.Visible;
                //this.imgDirectionForward.Source = this.bitmapCord;
                //this.imgDirectionReverse.Source = this.bitmapPullCord;
            }
            else
            {
                this.imgDirectionForward_PullCord.Visibility = Visibility.Visible;
                this.imgDirectionForward_Cord.Visibility = Visibility.Hidden;

                this.imgDirectionReverse_PullCord.Visibility = Visibility.Hidden;
                //this.imgDirectionForward.Source = this.bitmapPullCord;
                //this.imgDirectionReverse.Source = this.bitmapEmpty;
            }

        }


        #region [INotifyPropertyChanged]



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected bool SetField<T>(ref T field, T value,
           [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updadteUI();
        }
    }
}
