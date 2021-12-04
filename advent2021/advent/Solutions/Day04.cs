using System.Diagnostics.CodeAnalysis;
using advent.Util.Extensions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day04 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day04() { }

        /// <inheritdoc/>
        public Day04(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            (var input, var boards) = ReadInput();
            var marks = input.Split(',').Select(s => int.Parse(s));

            foreach (var mark in marks)
            {
                foreach (var board in boards)
                    board.Mark(mark);

                var winner = boards.FirstOrDefault(b => b.Bingo);
                if (winner != null)
                {
                    return (long)(winner.UnmarkedSum() * mark);
                }
            }

            return -1L;
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            (var input, var boards) = ReadInput();
            var marks = input.Split(',').Select(s => int.Parse(s));

            var winners = new List<Board>();
            var lastMark = 0;
            var lastSum = 0L;

            foreach (var mark in marks)
            {
                foreach (var board in boards)
                {
                    board.Mark(mark);

                    if (board.Bingo && !winners.Contains(board))
                    {
                        lastMark = mark;
                        lastSum = board.UnmarkedSum();

                        winners.Add(board);
                    }
                }
            }

            return (long)(lastSum * lastMark);
        }
        #endregion Day Members

        #region Private Methods
        private Tuple<string, IEnumerable<Board>> ReadInput()
        {
            var input = Data.ToList();

            var marks = input[0];
            var boards = new List<Board>();

            const int size = 5;
            var rows = 0;
            var board = new Board(size);

            for (var i = 2; i < input.Count; i++)
            {
                if (rows >= size || string.IsNullOrWhiteSpace(input[i]))
                {
                    boards.Add(board);
                    board = new Board(size);
                    rows = 0;

                    continue;
                }

                rows++;
                board.AddRow(input[i]);
            }

            return new Tuple<string, IEnumerable<Board>>(marks, boards);
        }
        #endregion Private Methods

        private enum Direction { Up, Down, Forward }

        private class Board : IEquatable<Board>
        {
            private string id = string.Empty;
            private IList<Tuple<int, bool>> values;
            private int n;

            public bool Bingo => IsWinner();

            public Board(int size)
            {
                n = size;
                values = new List<Tuple<int, bool>>(n * n);
            }

            public bool Equals(Board? other)
            {
                return GetHashCode() == other?.GetHashCode();
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(id);
            }

            public void AddRow(IEnumerable<int> row)
            {
                foreach (var number in row)
                {
                    values.Add(new Tuple<int, bool>(number, false));
                    id += $"{number}|";
                }
            }

            public void AddRow(IEnumerable<string> row)
            {
                AddRow(row.Select(number => int.Parse(number)));
            }

            public void AddRow(string row)
            {
                AddRow(row.SpaceSplit());
            }

            public Tuple<int, bool> At(int row, int col)
            {
                return values.ElementAt((row * n) + col);
            }

            public void Mark(int value)
            {
                var match = values.FirstOrDefault(t => t.Item1 == value);
                if (match is null) return;

                var idx = values.IndexOf(match);
                values[idx] = new Tuple<int, bool>(value, true);
            }

            public int UnmarkedSum() => values.Where(t => !t.Item2).Select(t => t.Item1).Sum();

            public int MarkedSum() => values.Where(t => t.Item2).Select(t => t.Item1).Sum();

            private bool IsWinner()
            {
                for (var i = 0; i < n; i++)
                {
                    // row
                    var rowWin = true;
                    for (var j = 0; j < n; j++)
                        rowWin &= At(j, i).Item2;

                    // col
                    var colWin = true;
                    for (var j = 0; j < n; j++)
                        colWin &= At(i, j).Item2;

                    if (rowWin || colWin) return true;
                }

                return false;
            }
        }
    }
}