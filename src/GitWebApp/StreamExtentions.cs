using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GitWebApp
{
    public static class StreamExtentions
    {
        public static async Task<string> ReadToEndAsync(this Stream stream)
        {
            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}
