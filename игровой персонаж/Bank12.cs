using System;
using static System.Net.Mime.MediaTypeNames;

class GameCharacter
{
   // Свойства класса
    private string Name { get; set; }
    private int MaxHealth { get; set; }
    private double CurrentHealth { get; set; }
    private bool IsAlly { get; set; }
    private int CoorX { get; set; }
    private int CoorY { get; set; }
    private int Wins { get; set; }
    private double Damage;

    public GameCharacter() => InputInformation();
     // метод ввода информации
    public void InputInformation()
    {
        Console.Write("Имя персонажа: ");
        Name = Console.ReadLine();
        Console.Write("Максимальное здоровье: ");
        MaxHealth = int.Parse(Console.ReadLine());
        Damage = MaxHealth * 0.4;

          Console.Write("Принадлежность к лагерю (+/-): ");
            string s = Console.ReadLine();
        if (s == "+")
            IsAlly = true;
        else if (s == "-")
            IsAlly = false;
        else
            Console.WriteLine(" Oшибка выбора повторите попытку");
        Console.Write("Координата X: ");
        this.CoorX = int.Parse(Console.ReadLine());
        Console.Write("Координата Y: ");
        this.CoorY = int.Parse(Console.ReadLine());
        CurrentHealth = MaxHealth; // Устанавливаем текущее здоровье равным максимальному при создании персонажа
    }
    void DisplayInformation()
    {
        Console.WriteLine("Информация о персонаже:");
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Максимальное здоровье: " + MaxHealth);
            Console.WriteLine("Текущее здоровье: " + CurrentHealth);
            Console.WriteLine("Принадлежность к лагерю: " + (IsAlly ? "Союзник" : "Враг"));
            Console.WriteLine("Координаты: (" + CoorX + ", " + CoorY + ")");
            Console.WriteLine(" Количество побед  " + Wins);
        
    }
    private void Fight(List<GameCharacter> characters)
    {
        foreach (GameCharacter enemy in characters)
        {
          // Проверяем, кто находится на той же координате что и выбранный перснаж 
            if (CoorX == enemy.CoorX && CoorY == enemy.CoorY)
            {
                if (enemy.IsAlly != IsAlly)
                {
                     Console.WriteLine("Драка началась!");
                    // Применяем урон 
                    enemy.CurrentHealth -= Damage;
                    this.CurrentHealth -= enemy.Damage;
                    Console.WriteLine($"Оставшееся здоровье врага: {enemy.CurrentHealth}");
                    Console.ReadKey();

                    if (enemy.CurrentHealth <= 0)
                    {
                        Wins++;
                        Console.WriteLine("Вы победили противника " + Wins + " раз(а)");

                        Console.ReadKey();
                    }
                }
            }
                    
        }
              
    }
    private void Move(List<GameCharacter> characters, int x, int y)
    {
        Console.Write("Введите новую координату X: ");
        x = int.Parse(Console.ReadLine());
        Console.Write("Введите новую координату Y: ");
        y = int.Parse(Console.ReadLine());
        int previousX = CoorX;
        int previousY = CoorY;
        CoorX = x;
        CoorY = y;
        Console.WriteLine($"Персонаж {Name} переместился с координат {previousX},{previousY} на {CoorX},{CoorY}");
        foreach (GameCharacter enemy in characters)
        {

            if (x == enemy.CoorX && y == enemy.CoorY)
            {
                if (enemy.IsAlly == IsAlly && enemy!=this)
                { Console.WriteLine("Это ваш тиммейт"); }
                else
                { Fight(characters); }
                    
               
            }
            else 
            {
                Console.WriteLine("На этих координатах никого нет");
            }
        }


    }


     void RestoreHealth()
    {
        if (Wins >= 5)
        {
            Console.WriteLine($"Персонаж {Name} использовал полное лечение.");
            CurrentHealth = MaxHealth;
        }
        else
        {
            Console.WriteLine("Недостаточно побед для использования полного лечения.");
        }
    }
    void Heal(List<GameCharacter> characters) // лечение команды 
    {
        foreach (GameCharacter ally in characters)
        {
            // Проверяем принадлежность команды персонажа к той же команде, что и текущий персонаж
            if (ally.IsAlly == IsAlly)
            {
                // Проверяем, достаточно ли здоровья у текущего персонажа для передачи его союзнику
                if (CurrentHealth > ally.MaxHealth)
                {
                    CurrentHealth -= ally.MaxHealth;
                    ally.CurrentHealth = ally.MaxHealth;
                    Console.WriteLine($"Вы передали своё здоровье персонажу {ally.Name}");
                }
                else
                {
                    Console.WriteLine("Недостаточно здоровья для передачи союзнику");
                }
            }
            else
            {
                Console.WriteLine($"Персонаж {ally.Name} не является союзником");
            }
        }
    }
    private void CheckWhoWin(List<GameCharacter> characters)
    {
        bool isAnyEnemyAlive = false;
        bool isAnyAllyAlive = false;
        foreach ( GameCharacter enemy in characters)
        foreach (GameCharacter ally in characters)
        {
            if(enemy.Wins > ally.Wins)
                {
                    Console.WriteLine("Победили враги");
                }
            else if (enemy.Wins < ally.Wins)
                {
                    Console.WriteLine("Победили союзники");
                }
            else
                {
                    Console.WriteLine("Ничья");
                }
        }
    }



    private static bool AreAllPlayersDead(List<GameCharacter> characters, bool isAlly)
    {
        foreach (var character in characters)
        {
            if (character.IsAlly == isAlly && character.CurrentHealth > 0)
            {
                return false; // Хотя бы один живой игрок из команды
            }
        }

        return true; // Нет живых игроков из команды
    }
   

    public void PlayGame(List<GameCharacter> characters , int x, int y)
    {
       

        bool isGameActive = true;
        
        while (isGameActive)
        {
            Console.WriteLine("Выберите действие:");
            
            Console.WriteLine("0.Перемещение");
            Console.WriteLine("1. драка");
            Console.WriteLine("2. Лечение команды");
            Console.WriteLine("3. Закончить игру");
          
            Console.WriteLine("5. информация ");
            Console.WriteLine("6. Победитель");
            Console.WriteLine("7.Лечение себя");



          int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    Move(characters, x, y); break;
                    
                case 1:
                    this.Fight(characters); break;

                case 2:
                    Heal(characters); break;

                case 3:
                    isGameActive = false; break;

                
                case 5:
                    DisplayInformation(); break;
                case 6:
                    CheckWhoWin(characters); break;
                case 7:
                    RestoreHealth(); break;

                default:
                    Console.WriteLine("Некорректный выбор! Попробуйте снова.");
                    break;
            }

            
        }

       
    }
}
