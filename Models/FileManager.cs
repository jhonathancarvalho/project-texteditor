namespace TextEditor.Models
{
    public static class FileManager
    {
        private static readonly string DefaultDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TextFiles");

        static FileManager()
        {

            if (!Directory.Exists(DefaultDirectory))
                Directory.CreateDirectory(DefaultDirectory);
        }

        public static void OpenFile()
        {
            Console.Clear();
            Console.WriteLine("=== Abrir Arquivo ===");
            Console.WriteLine("Arquivos disponíveis:");

            var files = Directory.GetFiles(DefaultDirectory, "*.txt");
            if (files.Length == 0)
            {
                Console.WriteLine("Nenhum arquivo encontrado. Pressione Enter para retornar ao menu.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < files.Length; i++)
                Console.WriteLine($"{i + 1} - {Path.GetFileName(files[i])}");

            Console.Write("Digite o número do arquivo que deseja abrir: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= files.Length)
            {
                string filePath = files[choice - 1];
                string content = ReadFileContent(filePath);
                Console.Clear();
                Console.WriteLine("=== Conteúdo do Arquivo ===");
                Console.WriteLine(content);
                Console.WriteLine("Pressione Enter para editar ou ESC para retornar ao menu.");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    string newContent = TextEditor.Edit(filePath, content);
                    SaveFile(filePath, newContent);
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Pressione Enter para retornar ao menu.");
                Console.ReadLine();
            }
        }

        private static string ReadFileContent(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        public static void SaveFile(string filePath, string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.Write(content);
                }
                Console.WriteLine("");
                Console.WriteLine("Arquivo atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar o arquivo: {ex.Message}");
            }

            Console.WriteLine("Pressione Enter para retornar ao menu.");
            Console.ReadLine();
        }

        public static void DeleteFile()
        {
            Console.Clear();
            Console.WriteLine("=== Excluir Arquivo ===");
            Console.WriteLine("Arquivos disponíveis:");

            var files = Directory.GetFiles(DefaultDirectory, "*.txt");
            if (files.Length == 0)
            {
                Console.WriteLine("Nenhum arquivo encontrado. Pressione Enter para retornar ao menu.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < files.Length; i++)
                Console.WriteLine($"{i + 1} - {Path.GetFileName(files[i])}");

            Console.Write("Digite o número do arquivo que deseja excluir: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= files.Length)
            {
                string filePath = files[choice - 1];
                Console.WriteLine($"Você realmente deseja excluir o arquivo '{Path.GetFileName(filePath)}'? (s/n)");
                var confirm = Console.ReadKey().Key;

                if (confirm == ConsoleKey.S)
                {
                    try
                    {
                        File.Delete(filePath);
                        Console.WriteLine($"\nArquivo '{Path.GetFileName(filePath)}' excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nErro ao excluir o arquivo: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("\nOperação cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }

            Console.WriteLine("Pressione Enter para retornar ao menu.");
            Console.ReadLine();
        }

        public static void CreateFile()
        {
            Console.Clear();
            Console.WriteLine("=== Criar Novo Arquivo ===");

            Console.Write("Digite o nome do arquivo (sem a extensão .txt): ");
            string fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("Nome do arquivo inválido. Pressione Enter para retornar ao menu.");
                Console.ReadLine();
                return;
            }

            string filePath = Path.Combine(DefaultDirectory, $"{fileName}.txt");

            Console.WriteLine("Digite o conteúdo do arquivo. Pressione ESC para finalizar a edição.");
            string content = string.Empty;

            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                    break;

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    content += Environment.NewLine;
                }
                else
                {
                    Console.Write(key.KeyChar);
                    content += key.KeyChar;
                }
            }

            SaveFile(filePath, content);
        }
    }
}
