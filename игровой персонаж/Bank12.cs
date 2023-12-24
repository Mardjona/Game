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

    public GameCharacter()
    {
        Name = "";
        MaxHealth = 0;
        CurrentHealth = 0;
        this.IsAlly = IsAlly;
        this.CoorX = CoorX;
        this.CoorY = CoorY;
        Wins = 0;
        Damage = MaxHealth * 0.4;
    }
     // метод ввода информации
    public void InputInformation()
    {
        // Console.WriteLine("Введите информацию о персонаже:");
        Console.Write("Имя персонажа: ");
        Name = Console.ReadLine();
        Console.Write("Максимальное здоровье: ");
        this.MaxHealth = int.Parse(Console.ReadLine());
        this.Damage = this.MaxHealth * 0.4;
        Console.Write("Принадлежность к лагерю (+/-): ");
        string s = Console.ReadLine();
        if (s == "+")
            IsAlly = true;
        else
            IsAlly = false;
        Console.Write("Координата X: ");
        this.CoorX = int.Parse(Console.ReadLine());
        Console.Write("Координата Y: ");
        this.CoorY = int.Parse(Console.ReadLine());
        CurrentHealth = MaxHealth; // Устанавливаем текущее здоровье равным максимальному при создании персонажа
    }
    void DisplayInformation()
    {
        Console.WriteLine("Информация о персонаже:");

        {
            Console.WriteLine("Информация о персонаже:");
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Максимальное здоровье: " + MaxHealth);
            Console.WriteLine("Текущее здоровье: " + CurrentHealth);
            Console.WriteLine("Принадлежность к лагерю: " + (IsAlly ? "Союзник" : "Враг"));
            Console.WriteLine("Координаты: (" + CoorX + ", " + CoorY + ")");
        }
    }
    // Драка с врагом

    private void Fight(List<GameCharacter> characters)
    {
        foreach (GameCharacter enemy in characters)
        {
            // Проверяем, кто находится на той же координате что и выбранный перснаж 
                if (CoorX == enemy.CoorX && CoorY == enemy.CoorY )
                {
                    if (enemy.IsAlly != IsAlly)
                    {

                        Console.WriteLine("Драка началась!");
                        // Применяем урон 
                        enemy.CurrentHealth -= this.Damage;
                        Console.WriteLine($"Оставшееся здоровье врага: {enemy.CurrentHealth}");
                        Console.ReadKey();
                        if (enemy.CurrentHealth <= 0 || CurrentHealth <= 0)
                        {
                            Wins++;
                            Console.WriteLine("Вы победили противника " + Wins + " раз(а)");

                            Console.ReadKey();
                        }
                    }
                    else
                    Console.WriteLine("Врагов нет");
                

                

                }
              
                else
                {
                    Console.WriteLine("На этих координатах никого нет");
                }





        }
        
    }

private void Move(List<GameCharacter> characters, int x, int y)
        {
        Console.Write("Введите новую координату X:");
        x = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите новую координату Y:");
        y = int.Parse(Console.ReadLine());
        int previousX = CoorX;
        int previousY = CoorY;
        CoorX = x;
        CoorY = y;
        foreach (GameCharacter character in characters)
            if( character != this && character.CoorX == CoorX && character.CoorY==CoorY)
            {
                Fight(characters);
                break;
            }
        Console.WriteLine( $"Персонаж { Name} переместился с координат {previousX},{previousY} на {CoorX},{CoorY}");
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
    void Heal(List<GameCharacter> game) // лечение команды 
    {
        foreach (GameCharacter ally in game)
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

    
  
    public void PlayGame(List<GameCharacter> characters)
    {
        bool isGameActive = true;
      
        while (isGameActive)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("0.Перемещение");
            Console.WriteLine("1. драка");
            Console.WriteLine("2. Лечение");
            Console.WriteLine("3. Закончить игру");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    Move(characters);
                    break;
                case 1:
                    this.Fight(characters);
                    break;
                case 2:
                    Heal(characters);
                    break;
                case 3:
                    isGameActive = false;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор! Попробуйте снова.");
                    break;
            }

            bool isAllyDead = AreAllPlayersDead(characters, true);
            bool isEnemyDead = AreAllPlayersDead(characters, false);

            if (isAllyDead || isEnemyDead)
            {
                Console.WriteLine("Игра завершена!");
                isGameActive = false;
            }
        }

       
    }
}

