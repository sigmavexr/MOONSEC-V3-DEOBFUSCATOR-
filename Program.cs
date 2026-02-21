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
            // 1. GÜVENLİK KONTROLÜ (Secrets'tan AUTH_KEY'i çeker)
            string botSecret = Environment.GetEnvironmentVariable("AUTH_KEY");

            // Botun gönderdiği ilk argüman şifreyle eşleşmeli
            if (args.Length == 0 || args[0] != botSecret)
            {
                Console.WriteLine("YETKİSİZ ERİŞİM! Bu araç sadece yetkili bot üzerinden çalışır.");
                return;
            }

            // 2. ARGÜMAN KONTROLÜ (Geri kalan 5 parametre: -dis/-dev -i <in> -o <out>)
            // Bot subprocess ile şunu yollamalı: [secret, command, -i, inputPath, -o, outputPath]
            if (args.Length != 6 || args[2] != "-i" || args[4] != "-o")
            {
                Console.WriteLine("Hatalı kullanım! Bot komutu eksik gönderdi.");
                return;
            }

            var command = args[1]; // -dev veya -dis
            var inputPath = args[3];
            var outputPath = args[5];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Geçersiz giriş yolu!");
                return;
            }

            try
            {
                if (command == "-dev")
                {
                    var result = new Deobfuscator().Deobfuscate(File.ReadAllText(inputPath));
                    using var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                    using var serializer = new Serializer(stream);
                    serializer.Serialize(result);
                    Console.WriteLine("Bytecode başarıyla çıkarıldı.");
                }
                else if (command == "-dis")
                {
                    var result = new Deobfuscator().Deobfuscate(File.ReadAllText(inputPath));
                    var cleanCode = new OptimizedLuaGenerator(result).Generate();
                    
                    // Bot imzasını ekliyoruz
                    string finalResult = "-- [[ Deobfuscated by Moonsec-B3 Bot ]]\n" + cleanCode;
                    
                    File.WriteAllText(outputPath, finalResult);
                    Console.WriteLine(finalResult); // Python'un okuması için konsola bas
                }
                else
                {
                    Console.WriteLine("Geçersiz komut!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("İşlem sırasında bir hata patladı: " + ex.Message);
            }
        }
    }
}
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
        Console.WriteLine("-- [[ Deobfuscated by epstein]]");
        // ... deobf mantığı ...
    }
}
