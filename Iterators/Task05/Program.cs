using System;
using System.Collections;
using System.Text;

/* На вход подается число N.
 * Нужно создать коллекцию из N элементов последовательного ряда натуральных чисел, возведенных в 10 степень, 
 * и вывести ее на экран ТРИЖДЫ. Инвертировать порядок элементов при каждом последующем выводе.
 * Элементы коллекции разделять пробелом. 
 * Очередной вывод коллекции разделять переходом на новую строку.
 * Не хранить всю коллекцию в памяти.
 * 
 * Код, данный в условии, НЕЛЬЗЯ стирать, его можно только дополнять.
 * Не использовать yield и foreach. Не вызывать метод Reset() в классе Program.
 * 
 * Пример входных данных:
 * 2
 * 
 * Пример выходных данных:
 * 1 1024
 * 1024 1
 * 1 1024
 * 
 * В случае ввода некорректных данных выбрасывайте ArgumentException.
 * В других ситуациях выбрасывайте 
*/
namespace Task05
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out int value) || value < 1)
                    throw new ArgumentException();

                MyDigits myDigits = new MyDigits();
                IEnumerator enumerator = myDigits.MyEnumerator(value);

                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
                Console.WriteLine();
                IterateThroughEnumeratorWithoutUsingForeach(enumerator);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
            catch (ArithmeticException)
            {
                Console.WriteLine("ooops");
            }
        }

        static void IterateThroughEnumeratorWithoutUsingForeach(IEnumerator enumerator)
        {
            StringBuilder sb = new StringBuilder();
            while (enumerator.MoveNext())
            {
                sb.Append($"{enumerator.Current} ");
            }
            Console.Write(sb.Remove(sb.Length - 1, 1));
        }
    }

    class MyDigits : IEnumerator // НЕ МЕНЯТЬ ЭТУ СТРОКУ
    {
        bool IsForward = true;
        int stop;
        int position = 0;

        public IEnumerator MyEnumerator(int value)
        {
            this.stop = value;
            return GetEnumerator();
        }

        public MyDigits GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (IsForward)
            {
                if (position < stop)
                {
                    position++;
                    return true;
                }
                else
                {
                    IsForward = false;
                    position++;
                    return false;
                }
            }
            else
            {
                if (position > 1)
                {
                    position--;
                    return true;
                }
                else
                {
                    IsForward = true;
                    position--;
                    return false;
                }
            }
        }

        public object Current
        {
            get
            {
                if (position == 0 || position > stop)
                    throw new ArgumentException();
                return Math.Pow(position, 10);
            }
        }

        public void Reset()
        {
            position = 0;
        }

    }
}
