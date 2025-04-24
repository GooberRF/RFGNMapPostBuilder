﻿using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace RFGNMapPostBuilder
{
	class Program
	{
		private static readonly string baseUrl = "https://autodl.factionfiles.com/findmap.php?rflName=";

		static async Task Main(string[] args)
		{
			string inputFile = null;
			string gametype = null;
			string gnNumber = null;

			for (int i = 0; i < args.Length - 1; i++)
			{
				switch (args[i])
				{
					case "-input": inputFile = args[i + 1]; break;
					case "-gametype": gametype = args[i + 1]; break;
					case "-gn": gnNumber = args[i + 1]; break;
				}
			}

			if (inputFile == null || !File.Exists(inputFile) || gametype == null || gnNumber == null)
			{
				Console.WriteLine("Usage: RFGNMapPostBuilder.exe -input maplist.txt -gametype DM -gn 157");
				return;
			}

			// Write GameNight#_X.txt
			var mapNames = File.ReadAllLines(inputFile);
			var output = new StringBuilder();
			output.AppendLine($"[b]Map pack for Red Faction Game Night {gnNumber} ({gametype})[/b]\n");
			output.AppendLine("[i]For more information and to participate in Red Faction Game Night events, join the Red Faction community on Discord: [url=https://discord.gg/factionfiles][FactionFiles Discord][/url][/i]\n");
			output.AppendLine("This game night event featured the following maps, of which all customs are included in this pack:");

			using (var httpClient = new HttpClient())
			{
				foreach (var rawMapName in mapNames)
				{
					string mapName = rawMapName.Trim();
					if (string.IsNullOrWhiteSpace(mapName))
						continue;

					var url = baseUrl + Uri.EscapeDataString(mapName);

					try
					{
						var response = await httpClient.GetStringAsync(url);
						var lines = response.Split('\n');
						if (lines.Length < 6 || lines[0] != "found")
						{
							Console.WriteLine($"Map not found: {mapName}");
							continue;
						}

						string name = lines[1];
						string author = lines[2];
						string id = lines[5];

						output.AppendLine($"[url=https://www.factionfiles.com/ff.php?action=file&id={id}]{name}[/url] by {author}");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Error fetching info for {mapName}: {ex.Message}");
					}
				}
			}

			output.AppendLine("\nWe hope you enjoy Game Night! After you play them, try to remember to stop back and leave reviews for the maps listed above. Your feedback could be really valuable to the featured creators!");

			string outputFile = $"GameNight{gnNumber}_{gametype}.txt";
			File.WriteAllText(outputFile, output.ToString());
			Console.WriteLine($"Map pack post saved to: {outputFile}");

			// Write serverlist.txt
			var serverListOutput = new StringBuilder();
			foreach (var rawMapName in mapNames)
			{
				string mapName = rawMapName.Trim();
				if (!string.IsNullOrWhiteSpace(mapName))
				{
					serverListOutput.AppendLine($"$Map: \"{mapName}\"");
				}
			}

			File.WriteAllText("serverlist.txt", serverListOutput.ToString());
			Console.WriteLine("Serverlist saved to: serverlist.txt");
		}
	}
}