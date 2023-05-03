using System;
using System.Text.RegularExpressions;
namespace MyNamespace{
    class Car
    {
        public string Brand { get; set; } // марка автомобиля
        public string Model { get; set; } // модель автомобиля
        public int Year { get; set; } // год выпуска автомобиля
        public double EngineCapacity { get; set; } // объем двигателя автомобиля

        // конструктор класса
        public Car(string brand, string model, int year, double engineCapacity)
        {
            if (!Regex.IsMatch(year.ToString(), @"^19\d{2}|20[01]\d|202[0-9]$"))
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Год выпуска автомобиля должен быть в пределах от 1900 до текущего года");
            }
            if (year < 1900 || year > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Год выпуска автомобиля должен быть в пределах от 1900 до текущего года");
            }
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Марка автомобиля не может быть пустой или состоять только из пробелов", nameof(brand));
            }
            if (engineCapacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(engineCapacity), "Объем двигателя автомобиля должен быть положительным числом");
            }
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentException("Модель автомобиля не может быть пустой или состоять только из пробелов", nameof(model));
            }
            this.Brand = brand;
            this.Model = model;
            this.Year = year;
            this.EngineCapacity = engineCapacity;
        }
        public override string ToString()
        {
            return $"Марка: {Brand}, Модель: {Model}, Год выпуска: {Year}, Объем двигателя: {EngineCapacity} л";
        }
    }
    class Catalog
    {
        // массив автомобилей
        private Car[] cars;

        // конструктор класса
        public Catalog()
        {
            cars = new Car[0];
        }

        // метод добавления автомобиля в каталог
        public void AddCar(Car car)
        {
            Array.Resize(ref cars, cars.Length + 1);
            cars[cars.Length - 1] = car;
        }

        // метод удаления автомобиля из каталога по индексу
        public void RemoveCar(int index)
        {
            if (index >= 0 && index < cars.Length)
            {
                for (int i = index; i < cars.Length - 1; i++)
                {
                    cars[i] = cars[i + 1];
                }
                Array.Resize(ref cars, cars.Length - 1);
            }
        }

        // метод изменения данных об автомобиле по индексу
        public void EditCar(int index, Car car)
        {
            if (index >= 0 && index < cars.Length)
            {
                cars[index] = car;
            }
        }

        // метод вывода информации обо всех автомобилях в каталоге
        public int PrintCatalog()
        {
            if (cars.Length == 0)
            {
                Console.WriteLine("Каталог пуст!");
            }
            else
            {
                Console.WriteLine("Автомобили в каталоге:");
                for (int i = 0; i < cars.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {cars[i]}");
                }
            }
        return cars.Length;
        }
    }
    struct MenuItem
    {
        public string Title;
        public Action Handler;
    }
    class Program{

        static void Main(string[] args)
        {
            Catalog catalog = new Catalog();
            catalog.AddCar(new Car("BMW", "X5", 2021, 3.0));
            catalog.AddCar(new Car("Audi", "Q7", 2019, 2.0));
            catalog.AddCar(new Car("Mercedes-Benz", "GLE", 2020, 3.5));

            // массив пунктов меню
            MenuItem[] menuItems = new MenuItem[]
            {
                new MenuItem { Title = "Добавить автомобиль", Handler = () => {
                    Console.Write("Марка: ");
                    string brand = Console.ReadLine();
                    Console.Write("Модель: ");
                    string model = Console.ReadLine();
                    Console.Write("Год выпуска: ");
                    int year = Int32.Parse(Console.ReadLine());
                    Console.Write("Объем двигателя: ");
                    double engineCapacity = Double.Parse(Console.ReadLine());
                    catalog.AddCar(new Car(brand, model, year, engineCapacity));
                    Console.WriteLine("Автомобиль успешно добавлен!");
                }},
                new MenuItem { Title = "Удалить автомобиль", Handler = () => {
                    if (catalog.PrintCatalog() == 0)
                    {
                        Console.WriteLine("Каталог пуст!");
                    }
                    else
                    {
                        Console.Write("Введите номер автомобиля, который нужно удалить: ");
                        int index = Int32.Parse(Console.ReadLine()) - 1;
                        catalog.RemoveCar(index);
                        Console.WriteLine("Автомобиль удален!");
                    }
                }},
                new MenuItem { Title = "Изменить данные об автомобиле", Handler = () => {
                    if (catalog.PrintCatalog() == 0)
                    {
                        Console.WriteLine("Каталог пуст!");
                    }
                    else
                    {
                        Console.Write("Введите номер автомобиля, данные которого нужно изменить: ");
                        int index = Int32.Parse(Console.ReadLine()) - 1;
                        Console.Write("Марка: ");
                        string brand = Console.ReadLine();
                        Console.Write("Модель: ");
                        string model = Console.ReadLine();
                        Console.Write("Год выпуска: ");
                        int year = Int32.Parse(Console.ReadLine());
                        Console.Write("Объем двигателя: ");
                        double engineCapacity = Double.Parse(Console.ReadLine());
                        catalog.EditCar(index, new Car(brand, model, year, engineCapacity));
                        Console.WriteLine("Данные об автомобиле успешно изменены!");
                    }
                }},
                new MenuItem { Title = "Показать все автомобили", Handler = () => {
                    if (catalog.PrintCatalog() == 0)
                    {
                        Console.WriteLine("Каталог пуст!");
                    }
                }},
                new MenuItem { Title = "Выход", Handler = () => Environment.Exit(0) }
            };

            // цикл вывода меню и обработки выбранных пунктов
            while (true)
            {
                Console.WriteLine("Меню:");
                for (int i = 0; i < menuItems.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {menuItems[i].Title}");
                }
                Console.Write("Выберите пункт меню: ");
                int choice = Int32.Parse(Console.ReadLine()) - 1;
                if (choice >= 0 && choice < menuItems.Length)
                {
                    menuItems[choice].Handler();
                }
                else
                {
                    Console.WriteLine("Некорректный выбор!");
                }
                Console.WriteLine();
            }
        }
    }
}