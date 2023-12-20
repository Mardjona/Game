using System;
using System.Collections.Generic;
using System.Numerics;
public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите количество персонажей: ");
        int characterCount = int.Parse(Console.ReadLine());

        if (characterCount >= 2)
        {
            List<GameCharacter> characters = new List<GameCharacter>();

            for (int i = 0; i < characterCount; i++)
            {
                Console.WriteLine("Введите информацию о персонаже " + (i + 1));
                GameCharacter character = new GameCharacter();
                character.InputInformation();
                characters.Add(character);
            }
            Console.Write("Выберете персонажа: ");
            int ans = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Игра начинается!");
            characters[ans].PlayGame(characters);  // Пример вызова игры для первого персонажа
        }
        else
        {
            Console.WriteLine("Для начала игры нужно создать минимум двух игроков!");
        }
    }
}




