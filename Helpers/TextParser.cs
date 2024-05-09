using System.Text.RegularExpressions;


namespace AutomatedMessagesPlugin;
public class TextParser {

    // Thanks to https://github.com/Challengermode/cm-cs2-colorsay
    public static string GetColoredText(string message)
    {
        Dictionary<string, int> colorMap = new()
        {
            { "{default}", 1 },
            { "{white}", 1 },
            { "{darkred}", 2 },
            { "{lightpurple}", 3},
            { "{green}", 4 },
            { "{lightgreen}", 5 },
            { "{slimegreen}", 6 },
            { "{red}", 7 },
            { "{grey}", 8 },
            { "{yellow}", 9 },
            { "{invisible}", 10 },
            { "{lightblue}", 11 },
            { "{blue}", 12 },
            { "{purple}", 13 },
            { "{pink}", 14 },
            { "{fadedred}", 15 },
            { "{gold}", 16 },
        };

        string pattern = "{(\\w+)}";
        string replaced = Regex.Replace(message, pattern, match =>
        {
            string colorCode = match.Groups[1].Value;
            if (colorMap.TryGetValue("{" + colorCode + "}", out int replacement))
            {
                // A little hack to get the color code to work
                return Convert.ToChar(replacement).ToString();
            }
            return match.Value;
        });
        // Non-breaking space - a little hack to get all colors to show
        return $"\xA0{replaced}";
    }
}
