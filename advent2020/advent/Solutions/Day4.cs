using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using advent.Exceptions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day4 : Day
    {
        public Day4() : base(4)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            #region Test data
            var text = new List<string>
            {
                "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd",
                "byr:1937 iyr:2017 cid:147 hgt:183cm",
                "",
                "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884",
                "hcl:#cfa07d byr:1929",
                "",
                "hcl:#ae17e1 iyr:2013",
                "eyr:2024",
                "ecl:brn pid:760753108 byr:1931",
                "hgt:179cm",
                "",
                "hcl:#cfa07d eyr:2025 pid:166559648",
                "iyr:2011 ecl:brn hgt:59in"
            };
            
            var invalidB = new List<string>
            {
                "eyr:1972 cid:100",
                "hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
                "",
                "iyr:2019",
                "hcl:#602927 eyr:1967 hgt:170cm",
                "ecl:grn pid:012533040 byr:1946",
                "",
                "hcl:dab227 iyr:2012",
                "ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
                "",
                "hgt:59cm ecl:zzz",
                "eyr:2038 hcl:74454a iyr:2023",
                "pid:3556412378 byr:2007"
            };
            
            var validB = new List<string>
            {
                "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980",
                "hcl:#623a2f",
                "",
                "eyr:2029 ecl:blu cid:129 byr:1989",
                "iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
                "",
                "hcl:#888785",
                "hgt:164cm byr:2001 iyr:2015 cid:88",
                "pid:545766238 ecl:hzl",
                "eyr:2022",
                "",
                "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"
            };
            #endregion Test data
            
            var passportsA = Solve(text);
            
            var badValidB = Solve(invalidB).Where(p => p.SuperValid).ToArray();
            var badInvalidB = Solve(validB).Where(p => !p.SuperValid).ToArray();
            
            return passportsA.Count(p => p.Valid) == 2 &&
                !badValidB.Any() && !badInvalidB.Any();
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data).Count(p => p.Valid);
            return new List<string> {$"[bold yellow]{answer}[/] valid passports"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = Solve(Data).Count(p => p.SuperValid);
            return new List<string> {$"[bold yellow]{answer}[/] valid passports"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static IEnumerable<Passport> Solve(IEnumerable<string> batch)
        {
            var currentBlock = new List<string>();
            var batchArray = batch.ToArray();

            var passportBlocks = new List<IEnumerable<string>>();
            var lastLine = batchArray.Last();

            foreach (var line in batchArray)
            {
                if (line.Equals(lastLine, StringComparison.Ordinal))
                {
                    currentBlock.Add(line);
                    passportBlocks.Add(currentBlock);
                    currentBlock = new List<string>();
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    passportBlocks.Add(currentBlock);
                    currentBlock = new List<string>();
                }
                else
                {
                    currentBlock.Add(line);
                }
            }

            return passportBlocks.Select(Passport.Parse).ToList();
        }
        #endregion Private Methods
        
        #region Classes
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        internal class Passport
        {
            public string? BirthYear { get; set; }
            public string? IssueYear { get; set; }
            public string? ExpirationYear { get; set; }
            public string? Height { get; set; }
            public string? HairColor { get; set; }
            public string? EyeColor { get; set; }
            public string? PassportId { get; set; }
            public string? CountryId { get; set; }
            public bool Valid => IsValid();
            public bool SuperValid => IsValid(true);

            private Passport() { }

            [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
            public static Passport Parse(IEnumerable<string> data)
            {
                if (data is null)
                    throw new BadDataException();
                
                var lines = data.ToList();
                if (!lines.Any())
                    throw new BadDataException();

                #region Split everything and make a dictionary out of the `key:value` pairs
                var tokens = new List<string>();
                var delimiters = new[] {'\r', '\n', ' ', ':'};
                foreach (var words in lines.Select(line => line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)))
                {
                    tokens.AddRange(words);
                }

                var tokenArray = tokens.ToArray();
                var pp = new Dictionary<string, string>();
                
                for (var i = 0; i < tokenArray.Length; i += 2)
                {
                    pp[tokenArray[i]] = tokenArray[i + 1];
                }
                #endregion Split everything and make a dictionary out of the `key:value` pairs
                
                #region Turn that dictionary into a Passport object
                var passport = new Passport();
                foreach (var (key, value) in pp)
                {
                    switch (key)
                    {
                        case "byr":
                            passport.BirthYear = value;
                            break;
                        case "iyr":
                            passport.IssueYear = value;
                            break;
                        case "eyr":
                            passport.ExpirationYear = value;
                            break;
                        case "hgt":
                            passport.Height = value;
                            break;
                        case "hcl":
                            passport.HairColor = value;
                            break;
                        case "ecl":
                            passport.EyeColor = value;
                            break;
                        case "pid":
                            passport.PassportId = value;
                            break;
                        case "cid":
                            passport.CountryId = value;
                            break;
                    }
                }
                #endregion Turn that dictionary into a Passport object

                return passport;
            }

#pragma warning disable 8604
            [SuppressMessage("ReSharper", "CA2201")]
            private bool IsValid(bool strict = false)
            {
                if (!strict)
                {
                    return !string.IsNullOrWhiteSpace(BirthYear) &&
                           !string.IsNullOrWhiteSpace(IssueYear) &&
                           !string.IsNullOrWhiteSpace(ExpirationYear) &&
                           !string.IsNullOrWhiteSpace(Height) &&
                           !string.IsNullOrWhiteSpace(HairColor) &&
                           !string.IsNullOrWhiteSpace(EyeColor) &&
                           !string.IsNullOrWhiteSpace(PassportId);
                }

                try
                {
                    #region Years
                    var by = int.Parse(BirthYear);
                    if (by < 1920 || by > 2002)
                        throw new Exception();
                    
                    var iy = int.Parse(IssueYear);
                    if (iy < 2010 || iy > 2020)
                        throw new Exception();
                    
                    var ey = int.Parse(ExpirationYear);
                    if (ey < 2020 || ey > 2030)
                        throw new Exception();
                    #endregion Years

                    #region Height
                    var heightM = Regex.Match(Height, @"^(\d+)(cm|in)$");
                    if (!heightM.Success)
                        throw new Exception();

                    switch (heightM.Groups[2].Value)
                    {
                        case "cm":
                            var heightCM = int.Parse(heightM.Groups[1].Value);
                            if (heightCM < 150 || heightCM > 193)
                                throw new Exception();
                            
                            break;
                        case "in":
                            var heightIN = int.Parse(heightM.Groups[1].Value);
                            if (heightIN < 59 || heightIN > 76)
                                throw new Exception();

                            break;
                        default:
                            throw new Exception();
                    }
                    #endregion Height
                    
                    #region Hair Color
                    if (!Regex.Match(HairColor, @"^#[0-9a-fA-F]{6}$").Success)
                        throw new Exception();
                    #endregion Hair Color
                    
                    #region Eye Color
                    if (!Regex.Match(EyeColor, @"^amb|blu|brn|gry|grn|hzl|oth$").Success)
                        throw new Exception();
                    #endregion Eye Color
                    
                    #region Passport ID
                    if (!Regex.Match(PassportId, @"^\d{9}$").Success)
                        throw new Exception();
                    #endregion Passport ID

                    return true;
                }
                catch
                {
                    return false;
                }
            }
#pragma warning restore 8604
        }
        #endregion Classes
    }
}