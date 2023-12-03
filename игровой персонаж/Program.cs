class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите количество игроков: ");
        int playerCount = int.Parse(Console.ReadLine());

        if (playerCount >= 2)
        {
            GameCharacter[] players = new GameCharacter[playerCount];

            for (int i = 0; i < playerCount; i++)
            {
                players[i] = new GameCharacter();
                players[i].InputInformation();
            }

            Console.WriteLine("Игра началась!");

            // Пример использования методов класса
            players[0].Attack(players[1]);

            players[0].RestoreHealth();

            players[0].Heal(players[1]);

            players[0].DisplayInformation();
            players[1].DisplayInformation();
        }
        else
        {
            Console.WriteLine("Недостаточно игроков для начала игры.");
        }
    }
}