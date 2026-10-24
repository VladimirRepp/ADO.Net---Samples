namespace AppCalculator.Logic
{
    public class Calculator
    {
        public float Sum(float a, float b) => a + b;
        public float Subtract(float a, float b) => a - b;
        public float Mult(float a, float b) => a * b;
        public float Difference(float a, float b)
        {
            if (b == 0)
                throw new Exception("На 0 не делим!");

            return a / b;
        }
    }
}
