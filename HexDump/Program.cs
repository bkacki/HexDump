namespace HexDump
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var position = 0;
            string fileName = "data.txt";

            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var buffer = new char[16];

                    var bytesRead = reader.ReadBlock(buffer, 0, 16);

                    Console.Write("{0:x4}: ", position);
                    position += bytesRead;

                    for (var i = 0; i < 16; i++)
                    {
                        if (i < bytesRead)
                            Console.Write("{0:x2} ", (byte)buffer[i]);
                        else
                            Console.Write("   ");
                        if (i == 7) Console.Write("-- ");
                    }

                    var bufferContents = new string(buffer);
                    Console.WriteLine("\t{0}", bufferContents.Substring(0, bytesRead));
                }
            }
        }
    }
}
