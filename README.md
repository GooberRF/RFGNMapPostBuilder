Red Faction Game Night Map Pack Post Generator
------

Usage:
------
- RFGNMapPostBuilder.exe -input <maplist.txt> -gn <event number> [-gametype <default type>] [-legacy]

Example:
------
- RFGNMapPostBuilder.exe -input maps.txt -gn 157 -gametype DM

Arguments:
------
- `-input`     Path to text file with .rfl map filenames, one per line
- `-gn`        RFGN number
- `-gametype`  Default game type when no prefix is recognized (default: DM)
- `-legacy`    Toggle legacy dedicated_server.txt format (default: false)

Output:
------
- Generates a formatted post text file: GameNight<GN>.txt
- Generates a formatted dedicated server config levels section: serverlist.txt

Game types are automatically determined from map filename prefixes when possible. Levels without a recognized prefix use the configured default game type (DM if unspecified).

Notes:
------
- This tool exists strictly to make my life easier. It has effectively no use for anyone else.
- It generates the FF file description for a GN map pack, and level section of the dedicated server config file.
- Map metadata (name, author, ID) is fetched via FactionFiles API: https://autodl.factionfiles.com
