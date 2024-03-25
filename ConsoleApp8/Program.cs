using System;
using GeometricFigures;
using GeometricFigures.Polygons;
using GeometricFigures.Polygons.GuessNumberGame;
using GeometricFigures.Polygons.LoremIpsumGenerator;

namespace GeometricFigures
{
    public abstract class Shape
    {
        public abstract double GetArea();
        public abstract double GetPerimeter();
    }

    namespace Polygons
    {
        public abstract class Polygon : Shape
        {
            protected int sides;

            public int Sides
            {
                get { return sides; }
            }

            public Polygon(int sides)
            {
                this.sides = sides;
            }
        }

        public class Triangle : Polygon
        {
            private double side1;
            private double side2;
            private double side3;

            public double Side1
            {
                get { return side1; }
            }

            public double Side2
            {
                get { return side2; }
            }

            public double Side3
            {
                get { return side3; }
            }

            public Triangle(double side1, double side2, double side3) : base(3)
            {
                if (!IsValidTriangle(side1, side2, side3))
                    throw new ArgumentException("Некорректные значения сторон треугольника.");

                this.side1 = side1;
                this.side2 = side2;
                this.side3 = side3;
            }

            public override double GetArea()
            {
                double s = (side1 + side2 + side3) / 2;
                return Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));
            }

            public override double GetPerimeter()
            {
                return side1 + side2 + side3;
            }

            private bool IsValidTriangle(double side1, double side2, double side3)
            {
                return side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1;
            }
        }

        public class Rectangle : Polygon
        {
            private double length;
            private double width;

            public double Length
            {
                get { return length; }
            }

            public double Width
            {
                get { return width; }
            }

            public Rectangle(double length, double width) : base(4)
            {
                if (length <= 0 || width <= 0)
                    throw new ArgumentException("Некорректные значения длины и/или ширины прямоугольника.");

                this.length = length;
                this.width = width;
            }

            public override double GetArea()
            {
                return length * width;
            }

            public override double GetPerimeter()
            {
                return 2 * (length + width);
            }
        }

        public class Square : Rectangle
        {
            public Square(double side) : base(side, side)
            {
            }
        }
        namespace GuessNumberGame
        {
            public class Game
            {
                private int number;
                private int minRange;
                private int maxRange;

                public Game(int minRange, int maxRange)
                {
                    if (minRange >= maxRange)
                        throw new ArgumentException("Некорректный диапазон чисел.");

                    this.minRange = minRange;
                    this.maxRange = maxRange;
                }

                public void Start()
                {
                    Console.WriteLine($"Загадайте число от {minRange} до {maxRange}.");
                    Console.WriteLine("Компьютер будет пытаться угадать.");

                    Random rand = new Random();
                    bool isGuessCorrect = false;

                    while (!isGuessCorrect)
                    {
                        int guess = rand.Next(minRange, maxRange + 1);

                        Console.WriteLine($"Компьютер предполагает, что вы загадали число {guess}.");

                        Console.WriteLine("1. Верно");
                        Console.WriteLine("2. Загаданное число меньше");
                        Console.WriteLine("3. Загаданное число больше");

                        int userChoice = GetUserChoice();

                        switch (userChoice)
                        {
                            case 1:
                                isGuessCorrect = true;
                                Console.WriteLine("Компьютер угадал число!");
                                break;

                            case 2:
                                maxRange = guess - 1;
                                break;

                            case 3:
                                minRange = guess + 1;
                                break;
                        }
                    }
                }

                private int GetUserChoice()
                {
                    int userChoice;

                    do
                    {
                        Console.Write("Выберите вариант ответа (1-3): ");
                    }
                    while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > 3);

                    return userChoice;
                }
            }
        }
        namespace LoremIpsumGenerator
        {
            public class Generator
            {
                private Random rand;

                public Generator()
                {
                    rand = new Random();
                }

                public string GenerateText(int vowelCount, int consonantCount, int maxWordLength)
                {
                    string vowels = "aeiou";
                    string consonants = "bcdfghjklmnpqrstvwxyz";

                    string text = "";
                    int totalLength = 0;

                    while (totalLength < (vowelCount + consonantCount))
                    {
                        string word = "";

                        
                        for (int i = 0; i < vowelCount; i++)
                        {
                            char vowel = vowels[rand.Next(vowels.Length)];
                            word += vowel;
                            totalLength++;
                        }

                        
                        for (int i = 0; i < consonantCount; i++)
                        {
                            char consonant = consonants[rand.Next(consonants.Length)];
                            word += consonant;
                            totalLength++;
                        }

                        
                        if (word.Length > maxWordLength)
                        {
                            word = word.Substring(0, maxWordLength);
                        }

                        text += word + " ";
                    }

                    return text.Trim();
                }
            }
        }
        public class Program
        {
            public static void Main()
            {
                try
                {
                    Triangle triangle = new Triangle(3, 4, 5);
                    Console.WriteLine($"Площадь треугольника: {triangle.GetArea()}");
                    Console.WriteLine($"Периметр треугольника: {triangle.GetPerimeter()}");

                    Rectangle rectangle = new Rectangle(4, 6);



                    Console.WriteLine($"Площадь прямоугольника: {rectangle.GetArea()}");
                    Console.WriteLine($"Периметр прямоугольника: {rectangle.GetPerimeter()}");

                    Square square = new Square(4);
                    Console.WriteLine($"Площадь квадрата: {square.GetArea()}");
                    Console.WriteLine($"Периметр квадрата: {square.GetPerimeter()}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                try
                {
                    Game game = new Game(1, 100);
                    game.Start();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                Generator generator = new Generator();

                Console.Write("Введите количество гласных: ");
                int vowelCount = int.Parse(Console.ReadLine());

                Console.Write("Введите количество согласных: ");
                int consonantCount = int.Parse(Console.ReadLine());

                Console.Write("Введите максимальную длину слова: ");
                int maxWordLength = int.Parse(Console.ReadLine());

                string text = generator.GenerateText(vowelCount, consonantCount, maxWordLength);
                Console.WriteLine($"Сгенерированный текст: {text}");
            }
        }
    }
}

