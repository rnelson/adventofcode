using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day8 : Day
    {
        public Day8()
        {
            DayNumber = 8;
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            var height = 6;
            var width = 25;
            var image = GetPixels(height, width);

            var layer = image.OrderBy(l => l.Count(p => p == 0)).First();
            var ones = layer.Count(p => p == 1);
            var twos = layer.Count(p => p == 2);

            return new List<string> { $"{ones * twos}" };
        }

        protected override ICollection<string> DoPart2()
        {
            var height = 6;
            var width = 25;
            var image = GetPixels(height, width);

            var white = ' ';
            var black = (char) 178;
            var display = new int[height * width];

            // Fill the image with invalid placeholder values
            for (var p = 0; p < display.Length; p++)
                display[p] = -1;

            #region Determine what we see
            for (var j = 0; j < height; j++)
            {
                for (var k = 0; k < width; k++)
                {
                    var offset = (j * width) + k;
                    var layer = 0;

                    while (display[offset] < 0)
                    {
                        var pixel = image[layer++][offset];
                        if (pixel != 2)
                            display[offset] = pixel;
                    }
                }
            }
            #endregion Determine what we see

            #region Display the image
            for (var j = 0; j < height; j++)
            {
                for (var k = 0; k < width; k++)
                {
                    var offset = (j * width) + k;
                    var pixel = display[offset];

                    switch (pixel)
                    {
                        case 0:
                            Console.Write(black);
                            break;
                        case 1:
                            Console.Write(white);
                            break;
                    }
                }

                Console.WriteLine();
            }
            #endregion Display the image

            return new List<string> { "" };
        }
        #endregion IDay Members

        #region Private Methods
        private List<int[]> GetPixels(int height = 6, int width = 25)
        {
            LoadInput();

            var layers = new List<int[]>();
            var pixelCount = height * width;

            var remaining = Data.First();
            while (!string.IsNullOrWhiteSpace(remaining))
            {
                var layer = remaining.Substring(0, pixelCount);
                remaining = remaining.Substring(pixelCount);

                var pixels = new int[pixelCount];
                for (var i = 0; i < pixelCount; i++)
                {
                    pixels[i] = int.Parse(layer[i].ToString(Culture), Culture.NumberFormat);
                }

                layers.Add(pixels);
            }

            return layers;
        }
        #endregion Private Methods
    }
}