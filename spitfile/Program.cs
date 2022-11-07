using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spitfile
{
    class Program
    {
        static void Main(string[] args)
        {
            //argomenti "nomefile" "megabyte"
            var nomefile = args[0];
            var mega = int.Parse(args[1]);
            var counterfile = 1 ;
            int count = 0;
            int bytes = 1024 * 1024;

            using (var stream = File.Open(nomefile, FileMode.Open))
            {

                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {
                    while (true)
                    {
                        var reade = false;
                        var file = new FileInfo(nomefile).Name.Replace(new FileInfo(nomefile).Extension, "") + "-" + counterfile + new FileInfo(nomefile).Extension;
                        using (var streamOpen = File.Open(file, FileMode.Create))
                        {
                            using (var writer = new BinaryWriter(streamOpen, Encoding.UTF8, false))
                            {
                                byte[] buf = null;
                                var totalLength = 0;
                                do
                                {
                                    buf = reader.ReadBytes(bytes);
                                    writer.Write(buf);
                                    totalLength += buf.Length;
                                    reade = buf.Length > 0;

                                } while (reade && totalLength/(1024*1024)<mega);
                                
                            }
                        }
                        if (!reade)
                        {
                            break;
                        }
                        counterfile++;
                    }

                }
                    
            }
        }
    }
}
