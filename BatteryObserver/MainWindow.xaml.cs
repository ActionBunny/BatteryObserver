using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;

using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace BatteryObserver
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
          public MainWindow()
        {
            InitializeComponent();

            PowerStatus pwr = SystemInformation.PowerStatus;

            //System.Windows.Forms.MessageBox.Show("State: " + pwr.BatteryChargeStatus.ToString() + " Charge: " + pwr.BatteryLifePercent.ToString());

            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }

        public void lowBatteryToast()
        {
            var xml = @"<toast>
                    <visual>
                        <binding template=""ToastImageAndText04"">
                            <image id=""1"" src=""file:///M:\GitHub\BatteryObserver\BatteryObserver\wurm.png"" alt=""wurm""/>
                            <text id=""1"">Batterie fast leer...</text>
                            <text id=""2"">Jetzt aber schnell wurmen.</text>
                            <text id=""3""></text>
                        </binding>
                    </visual>
                </toast>";

            var toastXml = new XmlDocument();
            toastXml.LoadXml(xml);
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("Battery Observer").Show(toast);
        }

        public void criticalBatteryToast()
        {
            var xml = @"<toast>
                    <visual>
                        <binding template=""ToastImageAndText04"">
                            <image id=""1"" src=""file:///M:\GitHub\BatteryObserver\BatteryObserver\wurm.png"" alt=""wurm""/>
                            <text id=""1"">Batterie ganz doll leer...</text>
                            <text id=""2"">Jetzt aber ganz ganz schnell wurmen.</text>
                            <text id=""3""></text>
                        </binding>
                    </visual>
                </toast>";

            var toastXml = new XmlDocument();
            toastXml.LoadXml(xml);
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("Battery Observer").Show(toast);
        }

        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            

            switch (SystemInformation.PowerStatus.BatteryChargeStatus)
            {
                case System.Windows.Forms.BatteryChargeStatus.Low:
                    //System.Windows.Forms.MessageBox.Show("Battery is running low.", "Low Battery");
                    lowBatteryToast();
                    break;
                case System.Windows.Forms.BatteryChargeStatus.Critical:
                    //System.Windows.Forms.MessageBox.Show("Battery is critcally low.", "Critical Battery");
                    criticalBatteryToast();
                    break;
                default:
                    //System.Windows.MessageBox.Show("Debug: BatteryChargeStatus changed");
                    break;
            }
        }
    }
}
