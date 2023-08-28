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
            
            // Set to true in order to save as CSV in the same fashion as the TAM System Explorer does it.
            bool exportToCsv = false;

            // Choose how much data you want to process at once.
            // The higher the value, the more memory the application will consume.
            // This is independent of the shown time frame you chose in the scope originally.
            var chunkDuration = TimeSpan.FromSeconds(1);

            var file = args[0];
            int i = 0;
            using (var acquisition = Protobuf.Acquire(GraphStreamingProperties.OpenStream(file)))
            {
                void ProcessData()
                {
                    if (exportToCsv)
                    {
                        new CsvExporter().ExportTo(acquisition, $"out{i++}.csv");
                    }
                    else
                    {
                        // Get data into a multidimensional array. First dimension selects the plot, second dimension
                        // the time.
                        // This naive implementation just keeps allocating new arrays in each loop, stressing the
                        // garbage collector.
                        var data = acquisition.Select(variable => variable.ToArray()).ToArray();

                        // TODO: Process the data.
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
