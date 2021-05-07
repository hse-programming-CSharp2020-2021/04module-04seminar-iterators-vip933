using System;
using System.Collections;
using System.Linq;

/* На вход подается число N.
 * На каждой из следующих N строках записаны ФИО человека, 
 * разделенные одним пробелом. Отчество может отсутствовать.
 * Используя собственноручно написанный итератор, выведите имена людей,
 * отсортированные в лексико-графическом порядке в формате 
 *      <Фамилия_с_большой_буквы> <Заглавная_первая_буква_имени>.
 * Затем выведите имена людей в исходном порядке.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield.
 * 
 * Пример входных данных:
 * 3
 * Banana Bill Bananovich
 * Apple Alex Applovich
 * Carrot Clark Carrotovich
 * 
 * Пример выходных данных:
 * Apple A.
 * Banana B.
 * Carrot C.
 * 
 * Banana B.
 * Apple A.
 * Carrot C.
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
*/
namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
                if (!int.TryParse(Console.ReadLine(), out int N) || N < 0)
                    throw new ArgumentException();
                Person[] people = new Person[N];
                Person[] people2 = new Person[N];
                for (int i = 0; i< N; i++)
                {
                    string[] c = Console.ReadLine().Split();
                    if (c.Length < 2)
                        throw new ArgumentException();
                    people[i] = new Person(c[0], c[1]);
                    people2[i] = new Person(c[0], c[1]);
                }

                People peopleList = new People(people);
                People peopleList2 = new People(people2);

                foreach (Person p in peopleList.GetPeople)
                    Console.WriteLine(p);

                Console.WriteLine();

                foreach (Person p in peopleList2)
                    Console.WriteLine(p);
            }
            catch (ArgumentException)
            {
                Console.Write("error");
            }
        }
    }

    public class Person: IComparable<Person>
    {
        public string firstName;
        public string lastName;

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public int CompareTo(Person other)
        {
            if (firstName.Length > other.firstName.Length)
                return 1;
            if (firstName.Length < other.firstName.Length)
                return -1;
            return (firstName+lastName).CompareTo(other.firstName+other.lastName);
        }
        public override string ToString() => $"{char.ToUpper(firstName[0])}{firstName.Substring(1)} {char.ToUpper(lastName[0])}.";
    }


    public class People : IEnumerable
    {
        private Person[] _people;
        public Person[] GetPeople
        {
            get
            {
                Array.Sort(_people);
                return _people;
            }
        }

        public People(Person[] people)
        {
            this._people = people;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }
    }

    public class PeopleEnum : IEnumerator
    {
        public Person[] _people;
        int position = -1;
        public PeopleEnum(Person[] people)
        {
            this._people = people;
        }

        public bool MoveNext()
        {
            if (position < _people.Length - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            position = -1;
        }


        public object Current
        {
            get
            {
                if (position == -1 || position >= _people.Length)
                    throw new ArgumentException();
                return _people[position];
            }
        }
    }
}
