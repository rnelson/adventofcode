namespace advent2022.Solutions;

public class Day07 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		var fs = Day7Classes.Filesystem.Parse(Input!);
		return (fs.Root.GetTotalSize(), 0L);
	}
}

internal class Day7Classes
{
	public class Filesystem
	{
		public FsDirectory Root { get; set; } = new FsDirectory(null, "/");

		public static Filesystem Parse(IEnumerable<string> input)
		{
			var fs = new Filesystem();
			var cd = fs.Root;
			var lineNumber = 1;
			
			foreach (var line in input)
			{
				try
				{
					switch (line[0])
					{
						case '$':
							if (line.Substring(2, 2).Equals("cd"))
							{
								if (line[5] == '/')
									cd = fs.Root;
								else if (line[5..] == "..")
									cd = cd!.Parent;
								else
									cd = cd!.Directories.First(d => d.Name == line[5..]);
							}
							else if (line.Substring(2, 2).Equals("ls"))
							{
								// Do nothing, blindly assuming ls output down below
								continue;
							}
							else
								throw new Exception($"unexpected command: {line.Substring(2, 2)}");
							break;
						case 'd':
							cd!.Directories.Add(new FsDirectory(cd, line[4..]));
							break;
						case '0': case '1': case '2': case '3': case '4':
						case '5': case '6': case '7': case '8': case '9':
							var bits = line.Split(' ');
							cd!.Files.Add(new FsFile(cd, bits[1], long.Parse(bits[0])));
							break;
						default:
							throw new Exception($"unexpected first character '{line[0]}'");
					}

					lineNumber++;
				}
				catch (Exception e)
				{
					throw new Exception($"error processing line {lineNumber}: {e.Message} - \"{line}\"");
				}
			}

			return fs;
		}

		public IList<FsDirectory> FindDirectories(long? minimumSize = null) =>
			FindDirectories(Root, minimumSize);
		
		private static IList<FsDirectory> FindDirectories(FsDirectory current, long? minimumSize = null)
		{
			var directories = new List<FsDirectory>();
			
			if (minimumSize != null)
				if (current.GetSize() >= minimumSize)
					directories.Add(current);
			
			foreach (var dir in current.Directories)
				directories.AddRange(FindDirectories(dir, minimumSize));

			return directories;
		}
	}

	public class FsDirectory
	{
		public readonly FsDirectory? Parent;
		public IList<FsDirectory> Directories { get; } = new List<FsDirectory>();
		public IList<FsFile> Files { get; } = new List<FsFile>();
		
		public readonly string Name;
		
		public FsDirectory(FsDirectory? parent, string name)
		{
			Parent = parent ?? this;
			Name = name;
		}

		public long GetTotalSize() => Directories.Sum(dir => dir.GetTotalSize()) + Files.Sum(file => file.Size);
		
		public long GetSize() => Files.Sum(file => file.Size);
	}

	public class FsFile
	{
		public readonly string Name;
		public readonly FsDirectory ParentDirectory;
		public readonly long Size;

		public FsFile(FsDirectory parent, string name, long size)
		{
			ParentDirectory = parent;
			Name = name;
			Size = size;
		}
	}

	public class FsPath
	{
		public static char FsPathSeparator = '/';

		public static string Combine(params string[] partials) => partials?.Length == 0 ? string.Empty : string.Join(FsPathSeparator, partials!);
	}
}