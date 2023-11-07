static class BasicMenu
{
    public static List<dynamic> MenuBasic(List<string> options, string MenuName)
    {
        if (options is null) return null!;
        if (options.Count == 0) return null!;
        int selectedIndex = 0;
        ConsoleKeyInfo keyInfo;

        do 
        {

            Console.Clear();
            System.Console.WriteLine(MenuName);

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine("--> " + options[i]);
                }
                else
                {
                    Console.WriteLine("    " + options[i]);
                }
            }
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.W && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (keyInfo.Key == ConsoleKey.S && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }

        } while (keyInfo.Key != ConsoleKey.Enter & keyInfo.Key != ConsoleKey.Escape);

        bool isKeyEsc;
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            isKeyEsc = true;
        }
        else isKeyEsc = false;
        List<dynamic> ToReturn = new() {isKeyEsc, options[selectedIndex]};
        return ToReturn;
    }
}