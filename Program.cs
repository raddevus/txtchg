// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(args.Length);
var currentCmd = getCommand(args);

switch (currentCmd){
    case "-rc":{
        
        var targetFile = getFileNameFromArgs(args);
        var charCount = getNumberCharsToRemove(args);
        Console.WriteLine ($"removing {charCount} chars from each line");
        if (!File.Exists(targetFile)){
            Console.WriteLine($"Target file ({targetFile}) does not exist)\nPlease provide a valid file & try again.");
            return;
        }
        if (charCount == 0){
            Console.WriteLine("You requested 0 chars to be removed.  No work to be done. Exiting.");
            return;
        }
        var tempFile = Path.GetTempFileName();
        var allLines = File.ReadAllLines(targetFile);
        List<String> allOutLines = new List<String>();
        foreach (string line in allLines){
            charCount = line.IndexOf(' ');
            if (charCount >= 0){
                allOutLines.Add(line.Substring(charCount));
            }
        }
        File.WriteAllLines(tempFile, allOutLines);
        File.Move(targetFile, targetFile + ".orig");
        File.Move(tempFile, targetFile);

        break;
    }
}



string getCommand(String [] args){
    if (args.Length > 0){
        return args[0];
    }
    return string.Empty;
}

string getFileNameFromArgs(string [] args){
    if (args.Length > 1){
        return args[1];
    }
    return String.Empty;
}

Int32 getNumberCharsToRemove(string [] args){
    if (args.Length > 2){
        return Int32.Parse(args[2]);
    }
    return 0;
}
