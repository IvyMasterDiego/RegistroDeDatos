using LabWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP
{
    public sealed class Person
    {
        public static List<Person> persons = new List<Person>();

        public string Id { get; }
        public string Name { get; }
        private int _packetData = 0;
        public double Ahorros;
        public string Password { get; set; }
        public int Age => (_packetData >> 4);

        public Person(in string id, in string name,in int age,in double ahorros,in string password)
        {
            Id = id;
            Name = name;
            Password = password;
            Ahorros = ahorros;
            _packetData = (age << 4);
        }
        public override string ToString() => $"{GetType().Name}({nameof(Id)}:{Id};{nameof(Name)}:{Name};{nameof(_packetData)}:{_packetData};{nameof(Age)}:{Age}; {nameof(Ahorros)}:{Ahorros};{nameof(Password)}:{Password};)";

        public override bool Equals(object obj)
        {
           if(obj is Person other)
            {
                return Id.Equals(other.Id);
            }
            return false;
        }

        internal static Person FromCsvLine(string line)
        {
            string[] tokens = line.Split(',');
            (string id,
                string name,
                string password,
                double ahorros,
                int packetData
                ) =
                (
                tokens[0],
                tokens[1],
                tokens[2],
                double.Parse(tokens[3]),
                int.Parse(tokens[4])
                );
            int age = (packetData >> 4);
            return new Person(id, name, age,ahorros,password);
        }

        internal static Person FromConsole(string record)
        {
            var tokens = record.Split(',');
            (string id,
                string name,
                string password,
                double ahorros,
                int age
                ) =
                (
                tokens[0],
                tokens[1],
                tokens[2],
                double.Parse(tokens[3]),
                int.Parse(tokens[4])
                );
            return new Person(id, name, age,ahorros,password);
        }

        internal static void SaveToCsv()
        {
            if (persons.Count() > 0)
            {
                File.WriteAllText(Program.file, "Cedula,Nombre,Edad,Ahorros,Password");
                foreach(var p in Person.persons)
                {
                    File.AppendAllText(Program.file, $"{Environment.NewLine}{p.Id},{p.Name},{p._packetData},{p.Ahorros},{p.Password}");
                }
            }
        }
        internal string Insert()
        {
            try
            {
                if (persons.Contains(this))
                    return "Ya existe una persona con esta cedula";
                else
                {
                    persons.Add(this);
                    return "Registro Guardado correctamente";
                }
            }
            catch (Exception)
            {
                return "Ha Ocurrido un error";
            }
        }
        internal static Person GetOnePerson(string id)
            => persons?.Where(a => a.Id.Trim() == id.Trim()).SingleOrDefault();

        internal string UpdateData()
        {
            try
            {
                var updating = persons.FindIndex(a => a.Id == this.Id);

                persons[updating] = this;
                return "Datos modificados correctamente";
            }
            catch (Exception)
            {
                return "No se pudieron modificar los datos";
            }
        }
        internal static int Erase(string id)
        {
            var eras = persons.RemoveAll(x => x.Id.Trim() == id.Trim());
            return eras;
        }
    }
}