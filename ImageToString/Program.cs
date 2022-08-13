using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ImageToString
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            while (true)
            {
                path = Console.ReadLine();
                if (path!=null)
                {
                    if (!File.Exists(path))
                    {
                        path = null;
                        Console.WriteLine("指定路劲不存在");
                    }
                    else break;
                }
            }
            FileInfo file = new FileInfo(path);
            var stream = file.OpenRead();
            byte[] buffer = new byte[file.Length];
            //读取图片字节流
            stream.Read(buffer, 0, Convert.ToInt32(file.Length));
            //将base64字符串保存到base64.txt文件中
            StreamWriter sw = new StreamWriter("base64.txt", false, Encoding.UTF8);
            //将字节流转化成base64字符串
            string outPut = Convert.ToBase64String(buffer);
            sw.Write(outPut);
            sw.Close();
            Console.WriteLine("Convert successful!");
            Console.WriteLine(outPut);
            Console.Read();
        }
    }
}