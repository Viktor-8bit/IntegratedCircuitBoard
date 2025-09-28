using System.Text;
using System.Text.RegularExpressions;

namespace GraphSetting.Loaders;

public class AllegroFormatLoader
{
    public string FilePath { get; private set; }
    public AllegroFormatLoader(string filepath)
    {
        this.FilePath = filepath;
    }

    public async Task FileLoad()
    {
        using (FileStream fs = new FileStream(this.FilePath, FileMode.Open))
        {
            byte[] bytes = new byte[fs.Length];
            await fs.ReadAsync(bytes, 0, bytes.Length);

            string readedNetFile = Encoding.Default.GetString(bytes);
            
            string pattern = @"(?ms)(^[A-Za-z0-9]+;)(.*?)(?=^[A-Za-z0-9]+;|\$END|\z)";
            
            foreach (Match m in Regex.Matches(readedNetFile, pattern))
            {
                var group2 = m.Groups[2].Value;
                List<string> formatedIDs = new List<string>();

                foreach (var m2 in group2.Split(' '))
                {
                    if (m2 != "")
                    {
                        var searched = m2.Replace("\n", "")
                            .Replace(",", "")
                            .Split('.')[0];
                        formatedIDs.Add(searched);
                    }
                }

                foreach (var c1 in formatedIDs)
                {
                    foreach (var c2 in formatedIDs)
                    {
                        if (c1 != c2)
                        {
                            //  R.SetWeight(c1, c2, 3);
                            Console.WriteLine($"{c1} -> {c2}");
                        }
                    }
                }
            }
            
        }
    }
}