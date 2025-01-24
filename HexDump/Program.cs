using System.ComponentModel.Design;
using System.Text;

namespace HexDump
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var position = 0;
            string fileName = "binarydata.dat"; //binarydata.dat data.txt

            using (Stream input = GetInputStream(args))
            {
                var buffer = new byte[16];
                int bytesRead;

                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    Console.Write("{0:x4}: ", position);
                    position += bytesRead;

                    for (var i = 0; i < 16; i++)
                    {
                        if (i < bytesRead)
                        {
                            if (buffer[i] < 0x20 || buffer[i] > 0x7F) buffer[i] = (byte)'.';

                            Console.Write("{0:x2} ", (byte)buffer[i]);
                        }
                        else
                            Console.Write("   ");
                        if (i == 7) Console.Write("-- ");
                    }

                    var bufferContents = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine("\t{0}", bufferContents.Substring(0, bytesRead));
                }
            }
        }

        static Stream GetInputStream(string[] args)
        {
            if ((args.Length != 1) || !File.Exists(args[0]))
                return Console.OpenStandardInput();
            else
                try
                {
                    return File.OpenRead(args[0]);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.Error.WriteLine("Nie można wczytać {0}, \nZrzut ze standardowego wejścia: {1}", args[0], ex.Message);
                    return Console.OpenStandardInput();
                }
        }
    }
}
