using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;

namespace APIDemo.IoT.DeviceEmulator
{
    class MessageManagement : IoTHubManager
    {
        internal static void EmulateDeviceToCloudMessagesAsync(List<Microsoft.Azure.Devices.Device> devices)
        {
            Random rand = new Random();
            var tokenSource2 = new CancellationTokenSource();

            Task.Run(
                   async () =>
                   {
                       // Were we already canceled?
                       tokenSource2.Token.ThrowIfCancellationRequested();

                       Console.CancelKeyPress += new ConsoleCancelEventHandler(cancelationHandler);
                       bool moreToDo = true;
                       while (moreToDo)
                       {
                           foreach (var curDevice in devices)
                           {
                               string messageString = GetRandomMessage(curDevice.Id, (MessageType)rand.Next(0, 5));
                               Message message = new Message(Encoding.ASCII.GetBytes(messageString));

                               DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(connectionString, curDevice.Id);
                               await deviceClient.SendEventAsync(message);
                               Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                           }

                           //Checks if a cancellation is requested
                           if (tokenSource2.Token.IsCancellationRequested)
                           {
                               // Clean up here, then...
                               moreToDo = false;
                               tokenSource2.Token.ThrowIfCancellationRequested();
                           }

                           Task.Delay(5000).Wait();
                       }

                   }, tokenSource2.Token).GetAwaiter().GetResult();

          
        }

        protected static void cancelationHandler(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            //tokenSource2.Cancel();
        }

        private static string GetRandomMessage(string deviceId, MessageType messageType)
        {
            string messageString = string.Empty;

            Random rand = new Random();
            switch (messageType)
            {
                case MessageType.Location:
                    messageString = JsonConvert.SerializeObject(new
                    {
                        deviceId = deviceId,
                        latitude = GetRandomNumber(-90, 90),
                        longitude = GetRandomNumber(-180, 180),
                        distortion = GetRandomNumber(0, 5),
                    });
                    break;

                case MessageType.Altitude:
                    messageString = JsonConvert.SerializeObject(new
                    {
                        deviceId = deviceId,
                        altitude = GetRandomNumber(0, 8848)
                    });
                    break;

                case MessageType.Temperature:
                    messageString = JsonConvert.SerializeObject(new
                    {
                        deviceId = deviceId,
                        scale = "celsius",
                        temperature = GetRandomNumber(-40, 55)
                    });
                    break;

                case MessageType.Humidity:
                    messageString = JsonConvert.SerializeObject(new
                    {
                        deviceId = deviceId,
                        humidity = GetRandomNumber(0, 100)
                    });
                    break;

                case MessageType.GetRoute:
                    messageString = JsonConvert.SerializeObject(new
                    {
                        deviceId = deviceId,
                        lastLocations = GetRandomNumber(0, 20)
                    });
                    break;

                default:
                    messageString = JsonConvert.SerializeObject(new
                    {
                        deviceId = deviceId,
                        errorNumber = GetRandomNumber(0, 999),
                        errorDescription = "Random error."
                    });
                    break;
            }
            return messageString;
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        internal enum MessageType
        {
            Location,
            Altitude,
            Temperature,
            Humidity,
            GetRoute,
            Error
        }
    }
}
