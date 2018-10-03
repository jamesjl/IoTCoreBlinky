using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTCoreBlink
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const int LED1_PIN = 5;
        private const int LED2_PIN = 6;
        private const int LED3_PIN = 7;

        private GpioPin pin1;
        private GpioPin pin2;
        private GpioPin pin3;

        private GpioPinValue pinValue;


        private DispatcherTimer timer;
        private SolidColorBrush redBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush greenBrush = new SolidColorBrush(Windows.UI.Colors.Green);
        private SolidColorBrush blueBrush = new SolidColorBrush(Windows.UI.Colors.Blue);
        private SolidColorBrush grayBrush = new SolidColorBrush(Windows.UI.Colors.LightGray);

        private int counter = 0;

        public MainPage()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;
            InitGPIO();
            if (pin1 != null)
            {
                timer.Start();
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin1 = null;
                pin2 = null;
                pin3 = null;
                GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin1 = gpio.OpenPin(LED1_PIN);
            pin2 = gpio.OpenPin(LED2_PIN);
            pin3 = gpio.OpenPin(LED3_PIN);
            pinValue = GpioPinValue.High;
            pin1.Write(pinValue);
            pin2.Write(pinValue);
            pin3.Write(pinValue);
            pin1.SetDriveMode(GpioPinDriveMode.Output);
            pin2.SetDriveMode(GpioPinDriveMode.Output);
            pin3.SetDriveMode(GpioPinDriveMode.Output);


            GpioStatus.Text = "GPIO pin initialized correctly.";

        }

        private void Timer_Tick(object sender, object e)
        {
            if (counter == 0)
            {
                pinValue = GpioPinValue.High;
                pin1.Write(pinValue);
                pin2.Write(pinValue);
                pin3.Write(pinValue);
                LED1.Fill = redBrush;
                LED2.Fill = greenBrush;
                LED3.Fill = blueBrush;
            }
            else if (counter == 1)
            {
                pinValue = GpioPinValue.Low;
                pin1.Write(pinValue);
                pin2.Write(pinValue);
                pin3.Write(pinValue);
                LED1.Fill = grayBrush;
                LED2.Fill = grayBrush;
                LED3.Fill = grayBrush;
            }
            else if (counter == 2)
            {
                pinValue = GpioPinValue.High;
                pin1.Write(pinValue);
                pin2.Write(pinValue);
                pin3.Write(pinValue);
                LED1.Fill = redBrush;
                LED2.Fill = greenBrush;
                LED3.Fill = blueBrush;
            }
            else if (counter == 3)
            {
                pinValue = GpioPinValue.Low;
                pin1.Write(pinValue);
                pin2.Write(pinValue);
                pin3.Write(pinValue);
                LED1.Fill = grayBrush;
                LED2.Fill = grayBrush;
                LED3.Fill = grayBrush;

            }
            else if (counter == 4)
            {
                pinValue = GpioPinValue.Low;
                pin1.Write(pinValue);
                pin2.Write(pinValue);
                pin3.Write(pinValue);
                LED1.Fill = grayBrush;
                LED2.Fill = grayBrush;
                LED3.Fill = grayBrush;
            }

            //if (pinValue == GpioPinValue.High)
            //{
            //    pinValue = GpioPinValue.Low;
            //    pin.Write(pinValue);
            //    LED.Fill = redBrush;
            //}
            //else
            //{
            //    pinValue = GpioPinValue.High;
            //    pin.Write(pinValue);
            //    LED.Fill = grayBrush;
            //}

            counter++;

            if (counter == 5)
                counter = 0;
        }
    }
}
