namespace TextEditor.Models
{
    public class TextEditor
    {
        public static string Edit(string filePath, string content)
        {
            Console.Clear();
            Console.WriteLine("=== Editor de Texto ===");
            Console.WriteLine("Edite o conteúdo abaixo. Pressione ESC para finalizar a edição.");
            Console.WriteLine();
            Console.WriteLine(content);
            Console.WriteLine("--------------------------------------------------");

            string newContent = content;

            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape)
                    break;

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    newContent += Environment.NewLine;
                }
                else
                {
                    Console.Write(key.KeyChar);
                    newContent += key.KeyChar;
                }
            }

            return newContent.TrimEnd();
        }
    }
}