using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 参考:https://social.msdn.microsoft.com/Forums/vstudio/ja-JP/092402bc-e842-4a19-add0-690de68be813/12471125221245012523125091254012488123631242512398214632044912?forum=csharpgeneralja

namespace SerialReceive
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.Ports.SerialPort portReceive = new System.IO.Ports.SerialPort("COM7", 9600);
            portReceive.DataReceived += portA_DataReceived; // 割込処理を登録
            portReceive.Open();

            while (true) {
                var readline = Console.ReadLine();
                Console.WriteLine($"KeyInput {readline}");
            }

            Console.ReadLine();
        }

        static void portA_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort port = (System.IO.Ports.SerialPort)sender;

            while (port.BytesToRead > 0)//受信バイトがあるなら繰り返し
            {
                byte[] receiveByte = new byte[2];
                int r = port.ReadByte();//1バイトずつ受信データを取り出す
                if (r >= 0)
                {
                    byte b = (byte)r;
                    receiveByte[0] = b;
                    receiveByte[1] = 0;
                    Console.WriteLine($"Receive {BitConverter.ToChar(receiveByte, 0)}({b})");
                }
            }
        }
    }
}
