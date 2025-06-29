using CW_1.DomainModelClasses;
using CW_1.Facades;

namespace CW_1.DataImport;

public abstract class DataImporter
{
    
        private bool CheckFileExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Путь к файлу пустой.");
                return false;
            }

            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Console.WriteLine($"Директория {dir} не существует.");
                return false;
            }

            return true;
        }

        public void Import(string path, BankAccountFacade accFacade, CategoryFacade catFacade, OperationFacade opFacade)
        {
            if (!CheckFileExists(path)) return;
            string content = File.ReadAllText(path);
            var records = ParseData(content);
            ProcessData(records, accFacade, catFacade, opFacade);
        }

        protected abstract List<Dictionary<string, string>> ParseData(string content);

        protected virtual void ProcessData(List<Dictionary<string, string>> records, BankAccountFacade accFacade, CategoryFacade catFacade, OperationFacade opFacade)
        {
            foreach (var record in records)
            {
                try
                {
                    string typeStr = record["Type"];
                    var opType = typeStr.ToLower() == "income" ? Operation.OperationType.Income : Operation.OperationType.Expense;

                    string accName = record["AccountName"];
                    var acc = accFacade.GetAll().FirstOrDefault(a => a.Name == accName) ?? accFacade.Create(accName);

                    decimal amount = decimal.Parse(record["Amount"]);
                    DateTime date = DateTime.Parse(record["Date"]);
                    string desc = record.ContainsKey("Description") ? record["Description"] : "";

                    string catName = record["CategoryName"];
                    var catType = record["CategoryType"].ToLower() == "income" ? Category.CategoryType.Income : Category.CategoryType.Expense;
                    var cat = catFacade.GetAll().FirstOrDefault(c => c.Name == catName && c.Type == catType) ?? catFacade.Create(catType, catName);

                    opFacade.Create(opType, acc, amount, date, cat, desc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обработке записи: {ex.Message}");
                }
            }
        }
}