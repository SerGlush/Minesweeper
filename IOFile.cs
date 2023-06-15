using System.IO;

namespace LABB
{
    partial class IO
    {
        public static string ReadWholeFile(IOFile Method)
        {
            return Method.Istream.ReadToEnd();
        }

        public static void Output(object Output, IOFile Method)
        {
            Method.Ostream.WriteLine(Output);
        }

        public static string InputString(IOFile Method)
        {
            return Method.Istream.ReadLine();
        }

        public static int InputInt(IOFile Method)
        {
            int input;
            Method.Ostream.WriteLine(Properties.Resources.AskIntegerInput);
            if (int.TryParse(Method.Istream.ReadLine(), out input)) return input;
            else
            {
                Method.Ostream.WriteLine(Properties.Resources.InvalidInput);
                return InputInt(Method);
            }
        }

        public static long InputLong(IOFile Method)
        {
            long input;
            Method.Ostream.WriteLine(Properties.Resources.AskIntegerInput);
            if (long.TryParse(Method.Istream.ReadLine(), out input)) return input;
            else
            {
                Method.Ostream.WriteLine(Properties.Resources.InvalidInput);
                return InputLong(Method);
            }
        }

        public static long InputLong(string otherwords, IOFile Method)
        {
            long input;
            Method.Ostream.WriteLine(otherwords);
            if (long.TryParse(Method.Istream.ReadLine(), out input)) return input;
            else
            {
                Method.Ostream.WriteLine(Properties.Resources.InvalidInput);
                return InputLong(otherwords, Method);
            }
        }

        public static float InputFloat(IOFile Method)
        {
            float input;
            Method.Ostream.WriteLine(Properties.Resources.AskIntegerInput);
            if (float.TryParse(Method.Istream.ReadLine(), out input)) return input;
            else
            {
                Method.Ostream.WriteLine(Properties.Resources.InvalidInput);
                return InputFloat(Method);
            }
        }

        public static double InputDouble(IOFile Method)
        {
            double input;
            Method.Ostream.WriteLine(Properties.Resources.AskIntegerInput);
            if (double.TryParse(Method.Istream.ReadLine(), out input)) return input;
            else
            {
                Method.Ostream.WriteLine(Properties.Resources.InvalidInput);
                return InputDouble(Method);
            }
        }
    }

    class IOFile
    {
        public StreamReader Istream = null;
        public StreamWriter Ostream = null;

        public void Close()
        {
            if (Istream != null && Ostream != null) { Istream.Close(); Ostream.Close(); }
        }
    }
}
