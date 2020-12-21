using OOP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabWork
{
    class Program
    {
        public static string file;
        static void Main(string[] args)
        {
            file = args[0];
            var newFile = !(File.Exists(file));
            if (newFile)
            {
                File.Create(file).Close();
                File.WriteAllText(file, "Cedula,Nombre,Edad,Ahorros,Contraseña");
                Console.WriteLine("Archivo Creado Exitosamente");
            }
            else
            {
                var list = GetAllData();
                foreach (var line in list)
                {
                    int index = list.IndexOf(line);
                    if (index > 0)
                    {
                        var person = Person.FromCsvLine(line);
                        Person.persons.Add(person);
                    }
                }
            }
            int cont = 1;
            while (cont == 1)
            {
                MenuCapt();
            }
        }
        public static string DataCap(bool newFile = true, string oldId = null)
        {
            Console.WriteLine("Captura de datos");
            var id = "";
            if (newFile)
            {
                Console.Write("Cedula");
                id = OnlyInt();
            }
            else
                id = oldId;
            Console.Write("Nombre: ");
            var name = Console.ReadLine();
        ageInput:
            Console.Write("Edad");
            int age = int.Parse(Console.ReadLine());
            if (!(age >= 7 && age <= 120))
                goto ageInput;
            Console.Write("Contraseña: ");
            var password = HidePassword();
            Console.Write("Confirmar Contraseña: ");
            var confirmPswr = HidePassword();
            if ((string.IsNullOrEmpty(password)) || !(password.Equals(confirmPswr)))
            {
                Console.WriteLine("Las Contraseñas no coinciden");
            }
            Console.Write("Ahorros: ");
            var ahorros = DoubleOnly();
            return $"{id},{name},{age},{ahorros},{password}";
        }
        private static string OnlyInt()
        {
            string intVal = "";
        readingKey:
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            while (consoleKeyInfo.Key != ConsoleKey.Enter)
            {
                if (consoleKeyInfo.Key != ConsoleKey.Backspace)
                {
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.NumPad0:
                        case ConsoleKey.D0:
                            Console.Write("0");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            Console.Write("1");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            Console.Write("2");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            Console.Write("3");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            Console.Write("4");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            Console.Write("5");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.D6:
                            Console.Write("6");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            Console.Write("7");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad8:
                        case ConsoleKey.D8:
                            Console.Write("8");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            Console.Write("9");
                            intVal += consoleKeyInfo.KeyChar;
                            break;
                    }
                }
                else
                {
                    if (intVal.Length > 0)
                    {
                        intVal = intVal.Remove(intVal.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                consoleKeyInfo = Console.ReadKey(true);
            }
            if (consoleKeyInfo.Key == ConsoleKey.Enter && intVal.Length == 0)
                goto readingKey;
            Console.WriteLine();
            return intVal;
        }
        public static string DoubleOnly()
        {
            string doubleValue = "";
            int pointCount = 0;
            int decimalC = 0;
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key != ConsoleKey.Backspace)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.NumPad0:
                        case ConsoleKey.D0:
                            Console.Write("0");
                            break;
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            Console.Write("1");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            Console.Write("2");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            Console.Write("3");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            Console.Write("4");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            Console.Write("5");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.D6:
                            Console.Write("6");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            Console.Write("7");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad8:
                        case ConsoleKey.D8:
                            Console.Write("8");
                            doubleValue += key.KeyChar;
                            break;
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            Console.Write("9");
                            doubleValue += key.KeyChar;
                            break;

                        case ConsoleKey.OemPeriod:
                            if (pointCount == 0)
                            {
                                pointCount++;
                                Console.Write(".");
                                doubleValue += key.KeyChar;
                            }
                            break;
                    }
                }
                else
                {
                    if (doubleValue.Length > 0)
                    {
                        char character = doubleValue.Last();
                        if (character == '.')
                            pointCount = 0;
                        doubleValue = doubleValue.Remove(doubleValue.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                key = Console.ReadKey(true);
            }
            Console.WriteLine();
            return doubleValue;
        }

        public static string HidePassword()
        {
            string psw = "";
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    psw += key.KeyChar;
                }
                else
                {
                    if (psw.Length > 0)
                    {
                        psw = psw.Remove(psw.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                key = Console.ReadKey(true);
            }
            Console.WriteLine();
            return psw;
        }
        public static void DataMenu(string record)
        {
            Console.WriteLine("Save (S), Reload (R), Exit(E)");
            var resp = Console.ReadLine();
            switch (resp.ToUpper())
            {
                case "S":
                    var person = Person.FromConsole(record);
                    Console.WriteLine(person.Insert());
                    Person.SaveToCsv();
                    break;
                case "R":
                    DataMenu(DataCap());
                    break;
                case "E":
                    Environment.Exit(1);
                    break;
                default:
                    DataMenu(record);
                    break;
            }
        }
        public static void MenuCapt()
        {
            Console.WriteLine("1.Capturar");
            Console.WriteLine("2.Lista");
            Console.WriteLine("3.Buscar");
            Console.WriteLine("4.Modificar");
            Console.WriteLine("5.Eliminar");
            Console.WriteLine("6.Exit");
            var op = OnlyInt();
            switch (op)
            {
                case "1":
                    DataMenu(DataCap());
                    break;
                case "2":
                    foreach (var obj in Person.persons)
                    {
                        Console.WriteLine(obj.ToString());
                    }
                    break;
                case "3":
                    var id = OnlyInt();
                    var person = Person.GetOnePerson(id)?.ToString();
                    if (person == null)
                        Console.WriteLine("No existe la persona");
                    else
                        Console.WriteLine(person.ToString());
                    break;
                case "4":
                    ModData();
                    break;
                case "5":
                    EraseData();
                    break;
                case "6":
                    Environment.Exit(1);
                    break;
                default:
                    MenuCapt();
                    break;
            }
        }
        public static List<string> GetAllData()
        {
            string[] datos = File.ReadAllLines(file);
            List<string> lista = new List<string>();
            for (int i = 0; i < datos.Length; i++)
            {
                lista.Add(datos[i]);
            }
            return lista;
        }
        public static void ModData()
        {
            Console.Write("Digite su cedula: ");
            var id = OnlyInt();

            var resp = Person.GetOnePerson(id);

            if (resp == null)
            {
                Console.WriteLine("No se encontro esta cedula. Desea ingresar otra Y/N? ");
                var answer = Console.ReadLine();
                switch (answer.ToUpper())
                {
                    case "Y":
                        ModData();
                        break;
                    case "N":
                        break;
                }
            }
            else
            {
                Console.WriteLine(resp.ToString());

                var person = Person.FromConsole(DataCap(false, resp.Id));

                ModMenu(person);
            }
        }
        public static void ModMenu(Person person)
        {
            Console.WriteLine("Modificar (M), Reintentar (R), Exit (E)");
            var answer = Console.ReadLine();
            switch (answer.ToUpper())
            {
                case "M":
                    Console.WriteLine(person.UpdateData());
                    break;
                case "E":
                    break;
                case "R":
                    ModData();
                    break;
            }
        }
        public static void EraseData()
        {
            Console.Write("Digite la cedula: ");
            var id = OnlyInt();
            var resp = Person.GetOnePerson(id);

            if (resp == null)
            {
                Console.WriteLine("No se encontro el registro. Desea continuar? Y/N");
                var answer = Console.ReadLine();
                switch (answer.ToUpper())
                {
                    case "Y":
                        EraseData();
                        break;
                    case "N":
                        break;
                }
            }
            else
            {
                Console.WriteLine(resp.ToString());
                DeleteMenu(resp.Id);
            }
        }

        public static void DeleteMenu(string id)
        {
            Console.WriteLine("Desea borrar este registro? Y/N");
            var answer = Console.ReadLine();
            switch (answer.ToUpper())
            {
                case "Y":
                    var cont = Person.Erase(id);
                    Console.WriteLine($"Se han eliminado ({cont}) registro/s");
                    break;
                case "N":
                    break;
                default:
                    DeleteMenu(id);
                    break;
            }
        }
    }
}
