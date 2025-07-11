namespace CW_1.DataImport;

public class YAMLDataImporter : DataImporter
{
    protected override List<Dictionary<string, string>> ParseData(string content)
    {
        var result = new List<Dictionary<string, string>>();
        var records = content.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var rec in records)
        {
            var record = new Dictionary<string, string>();
            var lines = rec.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                    record[parts[0].Trim()] = parts[1].Trim();
            }
            if (record.Count > 0)
                result.Add(record);
        }
        return result;
    }
}