Red Faction Game Night Map Pack Post Generator
------

Usage:
------
  RFGNMapPostBuilder.exe -input <maplist.txt> -gametype <DM|CTF|TDM> -gn <event number>

Example:
------
  RFGNMapPostBuilder.exe -input maps.txt -gametype DM -gn 157

Arguments:
------
  -input     Path to text file with .rfl map filenames, one per line
  -gametype  Game type abbreviation
  -gn        RFGN number

Output:
------
  Generates a formatted post text file: GameNight<GN>_<GAMETYPE>.txt
  Generates a formatted dedicated_server.txt $Map section: serverlist.txt

Notes:
------
  This tool exists strictly to make my life easier. It has effectively no use for anyone else.
  
  It generates the FF file description for a GN map pack, and $Map lines for the dedicated server config file.
  
  Map metadata (name, author, ID) is fetched via FactionFiles API: https://autodl.factionfiles.com