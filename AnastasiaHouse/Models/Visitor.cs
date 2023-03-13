using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnastasiaHouse
{
    public class Visitor
    {
        private static string path = @$"{new FileInfo(Application.ExecutablePath).Directory.Parent.Parent.Parent.FullName}\Resources\visitors.json";
        public int Id { get; set; }
        public int Number { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Status { get; set; }
        public DateOnly Birthdate { get; set; }
        public string Pay { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public int DaysAmount { get; set; }
        public string PhotoPath { get; set; }
        public bool TakeAmimals { get; set; }
        public static List<Visitor > Visitors { get; set; } = new List<Visitor>();

        public Visitor(int id, int number, string secondName, string firstName, string patronymic, string status, DateOnly birthdate, string pay, DateOnly arrivalDate, int daysAmount, string photoPath, bool takeAmimals)
        {
            Id = id;
            Number = number;
            SecondName = secondName;
            FirstName = firstName;
            Patronymic = patronymic;
            Status = status;
            Birthdate = birthdate;
            Pay = pay;
            ArrivalDate = arrivalDate;
            DaysAmount = daysAmount;
            PhotoPath = photoPath;
            TakeAmimals = takeAmimals;
        }

        public static void Read()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Visitors = JsonConvert.DeserializeObject<List<Visitor>>(json);
            }         
        }
    }
}
