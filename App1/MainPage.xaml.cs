// Copyright (c) Microsoft. All rights reserved.

//using System;
using Windows.Devices.Gpio;
//using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Media;
using System.Threading.Tasks;

namespace Blinky
{
    public sealed partial class MainPage : Page
    {

        private const int AMOUNT_OF_BITS = 3;
        private const int MAX_INT = (1 << AMOUNT_OF_BITS) - 1;

        private const int LED_PIN_1 = 6;
        private const int LED_PIN_2 = 13;
        private const int LED_PIN_3 = 19;
        private const int DELAY = 1000;

        private GpioPin pin1;
        private GpioPin pin2;
        private GpioPin pin3;

        public MainPage()
        {
            InitializeComponent();
            InitGPIO();

            while (true)
            {   
                for (int i = 0; i <= MAX_INT; i++)
                {
                    Method1(i);
                    //Method2(i);
                    Task.Delay(DELAY).Wait();
                }
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

            pin1 = gpio.OpenPin(LED_PIN_1);
            pin2 = gpio.OpenPin(LED_PIN_2);
            pin3 = gpio.OpenPin(LED_PIN_3);

            pin1.Write(GpioPinValue.Low);
            pin2.Write(GpioPinValue.Low);
            pin3.Write(GpioPinValue.Low);

            pin1.SetDriveMode(GpioPinDriveMode.Output);
            pin2.SetDriveMode(GpioPinDriveMode.Output);
            pin3.SetDriveMode(GpioPinDriveMode.Output);

            GpioStatus.Text = "GPIO pin initialized correctly.";
        }

        private void Method1(int currentInt)
        {
            int[] pinValues = new int[AMOUNT_OF_BITS];
            GpioPin[] pins = { pin3, pin2, pin1 };

            for (int j = 0; j < AMOUNT_OF_BITS; j++)
            {
                pinValues[j] = currentInt & 1;
                currentInt >>=  1;
            }

            for(int i = 0; i < pinValues.Length; i++)
            {
                if(pinValues[i] == 1)
                {
                    pins[i].Write(GpioPinValue.High);
                }
                else
                {
                    pins[i].Write(GpioPinValue.Low);
                }
            }
        }

        private void Method2(int currentInt)
        {
                    switch (currentInt)
                    {
                        case 0:
                            pin1.Write(GpioPinValue.Low);
                            pin2.Write(GpioPinValue.Low);
                            pin3.Write(GpioPinValue.Low);
                            break;
                        case 1:
                            pin1.Write(GpioPinValue.Low);
                            pin2.Write(GpioPinValue.Low);
                            pin3.Write(GpioPinValue.High);
                            break;
                        case 2:
                            pin1.Write(GpioPinValue.Low);
                            pin2.Write(GpioPinValue.High);
                            pin3.Write(GpioPinValue.Low);
                            break;
                        case 3:
                            pin1.Write(GpioPinValue.Low);
                            pin2.Write(GpioPinValue.High);
                            pin3.Write(GpioPinValue.High);
                            break;
                        case 4:
                            pin1.Write(GpioPinValue.High);
                            pin2.Write(GpioPinValue.Low);
                            pin3.Write(GpioPinValue.Low);
                            break;
                        case 5:
                            pin1.Write(GpioPinValue.High);
                            pin2.Write(GpioPinValue.Low);
                            pin3.Write(GpioPinValue.High);
                            break;
                        case 6:
                            pin1.Write(GpioPinValue.High);
                            pin2.Write(GpioPinValue.High);
                            pin3.Write(GpioPinValue.Low);
                            break;
                        case 7:
                            pin1.Write(GpioPinValue.High);
                            pin2.Write(GpioPinValue.High);
                            pin3.Write(GpioPinValue.High);
                            break;
                    }
        }

    }


}
