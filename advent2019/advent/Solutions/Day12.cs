using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day12 : Day
    {
        public Day12()
        {
            DayNumber = 12;
            LoadInput();
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            var moons = ParseInput();
            var n = 1000;
            var energy = 0;

            for (var step = 0; step < n; step++)
            {
                moons = Step(moons);
                energy = CalculateEnergy(moons);
            }

            return new List<string> { $"{energy}" };
        }

        protected override ICollection<string> DoPart2()
        {
            var moons = ParseInput(); 

            long x = -1;
            long y = -1;
            long z = -1;
            long steps = 0;

            while (true)
            {
                moons = Step(moons);
                steps++;

                if (x < 0 && moons.All(m => m.VelX == 0)) x = steps;
                if (y < 0 && moons.All(m => m.VelY == 0)) y = steps;
                if (z < 0 && moons.All(m => m.VelZ == 0)) z = steps;

                if (x > -1 && y > -1 && z > -1) break;
            }

            var lcm = Helpers.Math.Lcm(new[] {x, y, z});
            var repeat = lcm * 2;
            return new List<string> { $"{repeat}" };
        }
        #endregion IDay Members

        #region Private Methods
        private IEnumerable<Moon> ParseInput()
        {
            var regex = new Regex(@"<x=(-?\d+), y=(-?\d+), z=(-?\d+)>");
            return Data.Select(line => regex.Match(line)).Select(matches => new Moon(int.Parse(matches.Groups[1].Value, Culture.NumberFormat), int.Parse(matches.Groups[2].Value, Culture.NumberFormat), int.Parse(matches.Groups[3].Value, Culture.NumberFormat))).ToList();
        }

        private IEnumerable<Moon> Step(IEnumerable<Moon> moons, int steps = 1)
        {
            var snoom = moons.ToArray();
            var step = 0;

            while (step < steps)
            {
                #region Calculate velocities
                for (var i = 0; i < snoom.Length; i++)
                {
                    for (var j = 0; j < snoom.Length; j++)
                    {
                        if (i == j) continue;

                        #region X
                        if (snoom[i].PosX > snoom[j].PosX)
                            snoom[i].VelX--;
                        else if (snoom[i].PosX < snoom[j].PosX)
                            snoom[i].VelX++;
                        #endregion X

                        #region Y
                        if (snoom[i].PosY > snoom[j].PosY)
                            snoom[i].VelY--;
                        else if (snoom[i].PosY < snoom[j].PosY)
                            snoom[i].VelY++;
                        #endregion Y

                        #region Z
                        if (snoom[i].PosZ > snoom[j].PosZ)
                            snoom[i].VelZ--;
                        else if (snoom[i].PosZ < snoom[j].PosZ)
                            snoom[i].VelZ++;
                        #endregion Z
                    }
                }
                #endregion Calculate velocities

                #region Move
                foreach (var moon in snoom)
                {
                    moon.PosX += moon.VelX;
                    moon.PosY += moon.VelY;
                    moon.PosZ += moon.VelZ;
                }
                #endregion Move

                step++;
            }

            return snoom.ToList();
        }

        private int CalculateEnergy(Moon moon)
        {
            var potential = Math.Abs(moon.PosX) + Math.Abs(moon.PosY) + Math.Abs(moon.PosZ);
            var kinetic = Math.Abs(moon.VelX) + Math.Abs(moon.VelY) + Math.Abs(moon.VelZ);

            return potential * kinetic;
        }

        private int CalculateEnergy(IEnumerable<Moon> moons)
        {
            return moons.Sum(CalculateEnergy);
        }

        private string GetUniverse(IEnumerable<Moon> moons)
        {
            return moons.Aggregate(string.Empty, (current, moon) => current + moon);
        }
        #endregion Private Methods

        #region Moon
        private class Moon
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public int PosZ { get; set; }
            public int VelX { get; set; }
            public int VelY { get; set; }
            public int VelZ { get; set; }

            public Moon(int posX, int posY, int posZ, int velX = 0, int velY = 0, int velZ = 0)
            {
                PosX = posX;
                PosY = posY;
                PosZ = posZ;
                VelX = velX;
                VelY = velY;
                VelZ = velZ;
            }

            public override string ToString()
            {
                if (VelX == 0 && VelY == 0 && VelZ == 0)
                {
                    return $"<x={PosX}, y={PosY}, z={PosZ}>";
                }
                else
                {
                    return $"pos=<x={PosX}, y={PosY}, z={PosZ}>, vel=<x={VelX}, y={VelY}, z={VelZ}>";
                }
            }
        }
        #endregion Moon
    }
}