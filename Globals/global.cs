using Kokboken.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kokboken.Globals
{
    
    public class Global
    {
        public Global(){ }

        private static Lazy<Global> _instance = new Lazy<Global>(() => new Global());
        public static Global Data => _instance.Value;
        public List<Recipes> recipes {  get; set; } = new List<Recipes>();
        private string _fileName = "Receptlista.json";

        public static void SerilizeJson()
        {
            using (Stream s = File.Create(fname(Data._fileName)))
            using (TextWriter writer = new StreamWriter(s))
            {
                var sjson = JsonSerializer.Serialize<List<Recipes>>(Data.recipes, new JsonSerializerOptions() {WriteIndented = true});
                writer.Write(sjson);
            }
        }
        public static List<Recipes> DeserializeJson()
        {
            List<Recipes>? recipe;
            using (Stream s = File.OpenRead(fname(Data._fileName)))
            using (TextReader reader = new StreamReader(s))
            {
                var sjson = reader.ReadToEnd();
                recipe = JsonSerializer.Deserialize<List<Recipes>>(sjson);
            }
            return recipe;
        }
        public static string fname(string filename)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            documentPath = Path.Combine(documentPath, "Kokboken", "Recept");
            if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
            return Path.Combine(documentPath, filename);
        }

    }

}
