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

        public double Ahorros;
        public string Password { get; set; }
        private int bitpacking = 0;
        public int Age => (bitpacking >> 4);
        public Gender gender => (Gender)(bitpacking & 0b1000);
        public MStatus mStatus => (MStatus)(bitpacking & 0b100);
        public AGrad aGrad => (AGrad)(bitpacking & 0b11);


        public Person(in string id, in string name, in int age, in double ahorros, in Gender gender, in MStatus mStatus, in AGrad aGrad, in string password)
        {
            Id = id;
            Name = name;
            Ahorros = ahorros;
            Password = password;
            bitpacking = (age << 4) | (int)gender | (int)mStatus | (int)aGrad;
        }
        public override string ToString() => $"{GetType().Name}({nameof(Id)}:{Id}; {nameof(Name)}:{Name}; {nameof(bitpacking)}:{bitpacking}; {nameof(Age)}:{Age}; {nameof(Gender)}:{gender}; {nameof(MStatus)}:{mStatus}; {nameof(AGrad)}:{aGrad}; {nameof(Ahorros)}:{Ahorros}; {nameof(Password)}:{Password};)";

        public override bool Equals(object obj)
        {
            if (obj is Person other)
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
                double ahorros,
              int bitpacking,
                string password
                ) =
                (
                tokens[0],
                tokens[1],
                double.Parse(tokens[2]),
                int.Parse(tokens[3]),
                tokens[4]
                );
            int age = (bitpacking >> 4);
            Gender gender = (Gender)(bitpacking & 0b1000);
            MStatus mStatus = (MStatus)(bitpacking & 0b100);
            AGrad aGrad = (AGrad)(bitpacking & 0b11);
            return new Person(id, name, age, ahorros, gender, mStatus, aGrad, password);
        }

        internal static Person FromConsole(string record)
        {
            var tokens = record.Split(',');
            (string id,
                string name,
                int age,
                double ahorros,
                string password,
                Gender gender,
                MStatus mStatus,
                AGrad aGrad
                ) =
                (
                tokens[0],
                tokens[1],
                int.Parse(tokens[2]),
                double.Parse(tokens[3]),
                tokens[4],
                (Gender)int.Parse(tokens[5]),
            (MStatus)int.Parse(tokens[6]),
            (AGrad)int.Parse(tokens[7])
                );
            return new Person(id, name, age, ahorros, gender, mStatus, aGrad, password);
        }

        internal static void SaveToCsv()
        {
            if (persons.Count() > 0)
            {
                File.WriteAllText(Program.file, "Cedula,Nombre,Ahorros,Password,Data");
                foreach (var p in Person.persons)
                {
                    File.AppendAllText(Program.file, $"{Environment.NewLine}{p.Id},{p.Name},,{p.Ahorros},{p.Password},{p.Age}");
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
    public enum Gender
    {
        Male = 0,
        Female = 8
    }
    public enum MStatus
    {
        Single = 0,
        Married = 4
    }
    public enum AGrad
    {
        Inicial = 0,
        Bachiller = 1,
        Grado = 2,
        PostGrado = 3
    }
}
