namespace TextEditor.Models
{
    public static class Menu
    {
        public static void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Editor de Texto ===");
                Console.WriteLine("1 - Criar e salvar um novo arquivo");
                Console.WriteLine("2 - Abrir e editar um arquivo existente");
                Console.WriteLine("3 - Excluir um arquivo");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                if (short.TryParse(Console.ReadLine(), out short option))
                {
                    switch (option)
                    {
                        case 0:
                            Console.WriteLine("Saindo do programa...");
                            Environment.Exit(0);
                            break;
                        case 1:
                            FileManager.CreateFile();
                            break;
                        case 2:
                            FileManager.OpenFile();
                            break;
                        case 3:
                            FileManager.DeleteFile();
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Pressione Enter para tentar novamente.");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Pressione Enter para tentar novamente.");
                    Console.ReadLine();
                }
            }
        }
    }
}
