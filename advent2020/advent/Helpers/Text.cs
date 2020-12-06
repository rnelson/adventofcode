using System.Collections.Generic;
using System.Linq;

namespace advent.Helpers
{
    internal static class Text
    {
        /// <summary>
        /// Breaks <paramref name="data"/> into a list of lists of strings, each
        /// containing one block/group of input text (separated by blank lines).
        /// </summary>
        /// <param name="data">The original input text.</param>
        /// <returns>A list of lists, each containing one grouping of input.</returns>
        public static IEnumerable<IEnumerable<string>> Chunk(IEnumerable<string> data)
        {
            var currentBlock = new List<string>();
            var dataArray = data.ToArray();

            var blocks = new List<IEnumerable<string>>();
            
            for (var i = 0; i < dataArray.Length; i++)
            {
                var line = dataArray[i];
                
                if (i >= dataArray.Length - 1)
                {
                    currentBlock.Add(line);
                    blocks.Add(currentBlock);
                    currentBlock = new List<string>();
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    blocks.Add(currentBlock);
                    currentBlock = new List<string>();
                }
                else
                {
                    currentBlock.Add(line);
                }
            }

            return blocks;
        }
    }
}