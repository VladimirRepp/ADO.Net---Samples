namespace AppCalculator.Logic
{
    public class TranslatorTemperature
    {
        public double CelsiusToFahrenheit(double c)
        {
            return c * 9.0 / 5.0 + 32;
        }

        public double FahrenheitToCelsius(double f)
        {
            return (f - 32) * 5.0 / 9.0;
        }

        public double CelsiusToKelvin(double c)
        {
            return c + 273.15;
        }

        public double KelvinToCelsius(double k)
        {
            return k - 273.15;
        }

        public bool TryConvert(double value, EStateTemperature from, EStateTemperature to, out double result)
        {
            if (from == EStateTemperature.Celsius)
            {
                if (to == EStateTemperature.Fahrenheit)
                {
                    result = CelsiusToFahrenheit(value);
                }
                else if (to == EStateTemperature.Kelvin)
                {
                    result = CelsiusToKelvin(value);
                }
                else
                {
                    result = 0;
                    return false;
                }
            }
            else if(from == EStateTemperature.Fahrenheit)
            {
                if (to == EStateTemperature.Celsius)
                {
                    result = FahrenheitToCelsius(value);
                }
                else if(to == EStateTemperature.Kelvin){
                    value = FahrenheitToCelsius(value);
                    result = CelsiusToKelvin(value);
                }
                else
                {
                    result = 0;
                    return false;
                }
            }
            else 
            {
                if(to == EStateTemperature.Celsius)
                {
                    result = KelvinToCelsius(value);
                }
                else if(to == EStateTemperature.Fahrenheit)
                {
                    value = KelvinToCelsius(value);
                    result = CelsiusToFahrenheit(value);
                }
                else
                {
                    result = 0;
                    return false;
                }
            }
            
            return true;
        }
    }

    public enum EStateTemperature
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }
}
