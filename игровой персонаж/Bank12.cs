using System;

class GameCharacter
{
    // Свойства класса
     string Name { get; set; }
     int MaxHealth { get; set; }
     int CurrentHealth { get; set; }
     bool IsAlly { get; set; }
     int Coordinate { get; set; }
     int Wins { get; set; }

    // Методы класса
    public void InputInformation()
    {
        Console.WriteLine("Введите информацию о персонаже:");
        Console.Write("Имя: ");
        Name = Console.ReadLine();
        Console.Write("Максимальное здоровье: ");
        MaxHealth = int.Parse(Console.ReadLine());
        CurrentHealth = MaxHealth;
        Console.Write("Принадлежность к лагерю (true - союзник, false - враг): ");
        IsAlly = bool.Parse(Console.ReadLine());
        Console.Write("Расположение в координате: ");
        Coordinate = int.Parse(Console.ReadLine());
        Wins = 0;
    }

    public void DisplayInformation()
    {
        Console.WriteLine("Информация о персонаже:");
        Console.WriteLine($"Имя: {Name}");
        Console.WriteLine($"Максимальное здоровье: {MaxHealth}");
        Console.WriteLine($"Текущее здоровье: {CurrentHealth}");
        Console.WriteLine($"Принадлежность к лагерю: {(IsAlly ? "Союзник" : "Враг")}");
        Console.WriteLine($"Расположение в координате: {Coordinate}");
        Console.WriteLine($"Количество побед: {Wins}");
    }

    public bool IsInSameCoordinate(GameCharacter otherCharacter)
    {
        return Coordinate == otherCharacter.Coordinate;
    }

    public void Attack(GameCharacter enemy)
    {
        Console.WriteLine($"Персонаж {Name} атакует персонажа {enemy.Name}");
        enemy.CurrentHealth -= 10;
        if (enemy.CurrentHealth <= 0)
        {
            enemy.CurrentHealth = 0;
            enemy.Wins++;
        }
        Console.WriteLine($"Персонаж {enemy.Name} получает урон, текущее здоровье: {enemy.CurrentHealth}");
    }

    public void RestoreHealth()
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

    public void Heal(GameCharacter teammate)
    {
        if (IsAlly && IsInSameCoordinate(teammate))
        {
            Console.WriteLine($"Персонаж {Name} лечит персонажа {teammate.Name}");
            int healAmount = Math.Min(CurrentHealth, teammate.MaxHealth - teammate.CurrentHealth);
            CurrentHealth -= healAmount;
            teammate.CurrentHealth += healAmount;
            Console.WriteLine($"Персонаж {teammate.Name} восстановил здоровье на {healAmount}, текущее здоровье: {teammate.CurrentHealth}");
        }
        else
        {
            Console.WriteLine("Невозможно применить лечение к данному персонажу.");
        }
    }
}

