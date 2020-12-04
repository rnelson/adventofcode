using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    internal class Day3 : Day
    {
        private const char OPEN = '.';
        private const byte OPEN_B = 0;
        private const char TREE = '#';
        private const byte TREE_B = 1;
        
        public Day3() : base(3)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            var landscape = Process(new List<string>
            {
                "..##.......",
                "#...#...#..",
                ".#....#..#.",
                "..#.#...#.#",
                ".#...##..#.",
                "..#.##.....",
                ".#.#.#....#",
                ".#........#",
                "#.##...#...",
                "#...##....#",
                ".#..#...#.#"
            });

            var treesA = Solve(landscape, 3, 1);
            
            return treesA == 7;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Process(Data), 3, 1);
            return new List<string> {$"[bold yellow]{answer}[/] trees"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var d = Process(Data);
            var slopes = new List<int>
            {
                Solve(d, 1, 1),
                Solve(d, 3, 1),
                Solve(d, 5, 1),
                Solve(d, 7, 1),
                Solve(d, 1, 2)
            };
            var answer = slopes.Aggregate(1, (a, b) => a * b);
            return new List<string> {$"[bold yellow]{answer}[/] tress"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int Solve(IReadOnlyList<byte[]> map, sbyte x, sbyte y)
        {
            int curX = 0, curY = 0;
            var trees = 0;

            while (curY < map.Count)
            {
                if (curX >= map[0].Length)
                {
                    curX -= map[0].Length;
                }

                if (map[curY][curX] == TREE_B)
                    trees++;

                curX += x;
                curY += y;
            }

            /*
            while (curY < map.Length && curX < map[0].Length)
            {
                if (map[curY][curX] == TREE_B)
                    trees++;

                curX += x;
                curY += y;
            }
            */
            
            return trees;
        }

        private static byte[][] Process(IEnumerable<string> data)
        {
            var d = data.ToList();
            var landscape = new byte[d.Count][];
            var currentRow = 0;
            
            foreach (var line in d)
            {
                var row = new byte[line.Length];

                for (var i = 0; i < line.Length; i++)
                {
                    row[i] = line[i] == TREE ? TREE_B : OPEN_B;
                }

                landscape[currentRow++] = row;
            }

            return landscape;
        }
        #endregion Private Methods
    }
}