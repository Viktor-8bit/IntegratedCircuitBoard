using System.Text;
using System.Text.RegularExpressions;


namespace GraphSetting.Loaders;


public class AllegroFormatLoader(string filepath)
{
    private string FilePath { get; set; } = filepath ?? throw new ArgumentNullException(nameof(filepath));
    
    public UniqueElectronicComponents ComponentsManager { get; private set; } = new UniqueElectronicComponents();
    
    public async Task FileLoad()
    {
        await using (FileStream fs = new FileStream(this.FilePath, FileMode.Open))
        {
            byte[] bytes = new byte[fs.Length];
            await fs.ReadAsync(bytes, 0, bytes.Length);

            string readedNetFile = Encoding.Default.GetString(bytes);
            
            string pattern = @"(?ms)(^[A-Za-z0-9]+;)(.*?)(?=^[A-Za-z0-9]+;|\$END|\z)";
            
            // находит строки типа: N01153;  DD1.1 R3.1 (соединения) с учетом переносов
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
                        ComponentsManager.AddUniqueElectronicComponent(searched);
                    }
                }

                foreach (var c1 in formatedIDs)
                {
                    foreach (var c2 in formatedIDs)
                    {
                        if (c1 != c2)
                        {
                            this.ComponentsManager.AddElementConnection(c1, c2);
                        }
                    }
                }
            }
        }
    }
}