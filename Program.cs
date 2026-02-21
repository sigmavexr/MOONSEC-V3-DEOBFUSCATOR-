using System;
using System.IO;
using MoonsecDeobfuscator.Deobfuscation;
using MoonsecDeobfuscator.Deobfuscation.Bytecode;

namespace MoonsecDeobfuscator
{
    public static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5 || args[1] != "-i" || args[3] != "-o")
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("Devirtualize and dump bytecode to file:\n\t-dev -i <input> -o <output>");
                Console.WriteLine("Devirtualize and dump clean Lua code to file:\n\t-dis -i <input> -o <output>");
                return;
            }
            var command = args[0];
            var input = args[2];
            var output = args[4];
            if (!File.Exists(input))
            {
                Console.WriteLine("Invalid input path!");
                return;
            }
            if (command == "-dev")
            {
                var result = new Deobfuscator().Deobfuscate(File.ReadAllText(input));
                using var stream = new FileStream(output, FileMode.Create, FileAccess.Write);
                using var serializer = new Serializer(stream);
                serializer.Serialize(result);
            }
            else if (command == "-dis")
            {
                var result = new Deobfuscator().Deobfuscate(File.ReadAllText(input));
                var cleanCode = new OptimizedLuaGenerator(result).Generate();
                File.WriteAllText(output, cleanCode);
            }
            else
            {
                Console.WriteLine("Invalid command!");
            }
        }
    }
}
using System;

class Program {
    static void Main(string[] args) {
        // Replit Secrets'tan AUTH_KEY'i oku
        string botSecret = Environment.GetEnvironmentVariable("AUTH_KEY");

        // Kontrol: Şifre yoksa veya botun gönderdiği ilk argüman şifreyle eşleşmiyorsa kapat
        if (args.Length == 0 || args[0] != botSecret) {
            Console.WriteLine("YETKİSİZ ERİŞİM! Bu araç sadece yetkili bot üzerinden çalışır.");
            return;
        }

        // Eğer şifre doğruysa, deobf edilecek kod 2. argümandadır
        string inputLua = args[1];
        
        // --- BURADAN SONRASI SENİN DEOBF KODLARININ DEVAMI OLACAK ---
        Console.WriteLine("-- [[ Deobfuscated by MyBot ]]");
        // ... deobf mantığı ...
    }
}
