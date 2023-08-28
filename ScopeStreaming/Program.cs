using System;
using System.Linq;
using Triamec.Acquisitions;
using Triamec.Tam.Scope;

namespace ScopeStreaming
{
    static class Program
    {

        /// <summary>
        /// Processes the specified ".TAMpbf" file.
        /// </summary>
        static void Main(string[] args)
        {
            bool exportToCsv = false;

            // free choice of duration
            var chunkDuration = TimeSpan.FromSeconds(1);

            var file = args[0];
            int i = 0;
            using (var acquisition = Protobuf.Acquire(GraphStreamingProperties.OpenStream(file)))
            {
                void ProcessData()
                {
                    if (exportToCsv)
                    {

                        // export to a CVS file 
                        new CsvExporter().ExportTo(acquisition, $"out{i++}.csv");
                    }
                    else
                    {

                        // get data into a multidimensional array
                        var data = acquisition.Select(variable => variable.ToArray()).ToArray();

                        Console.WriteLine($"Processed {data[0].Length} samples.");
                    }
                }

                while (acquisition.Acquire(chunkDuration))
                {
                    ProcessData();
                }

                // process the rest
                ProcessData();
            }
        }
    }
}
