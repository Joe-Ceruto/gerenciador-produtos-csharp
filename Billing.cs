namespace ProgressiveCount
{
    public static class Math
    {
        public static void CountingNumber(int number = 1)
        {
            int value = 0;
            while( value < number)
            {
                value++;
                Console.WriteLine(value);
            }
        }


        public static void Tabuada(int number)
        {
            for(int i = 1; i <= 10; i++)
            {
                int result = number * i;
                Console.WriteLine($"{number} x {i} = {result}");
            }
        }

        public static int Somar(int number1, int number2)
        {
            return number1 + number2;
        }

        public static int Multiplicar(int number1, int number2)
        {
            return number1 * number2;
        }

        public static int Dividir(int number1, int number2)
        {
            return number1 / number2;
        }

        public static int Subtrair(int number1, int number2)
        {
            return number1 - number2;
        }

    }
}
