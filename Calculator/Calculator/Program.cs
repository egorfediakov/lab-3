using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string s = Console.ReadLine();
                Console.WriteLine("{0}", Calculator.Calculate(s));

            }
        }
    }
    class Calculator
    {
        static public string Mul_Div(string str)
        {
            double firstNum, secondNum;
            Regex reg = new Regex(@"(\-*[\d,]+)([\*\/]{1})(\-*[\d,]+)");
            Match mat = reg.Match(str);
            if (mat.Groups.Count == 4)
            {
                firstNum = double.Parse(mat.Groups[1].ToString());
                secondNum = double.Parse(mat.Groups[3].ToString());
                if (mat.Groups[2].ToString()[0] == '*')
                    firstNum *= secondNum;
                else if (mat.Groups[2].ToString()[0] == '/')
                    firstNum /= secondNum;
                return str.Replace(mat.Groups[0].ToString(), firstNum.ToString());
            }
            return str;
        }

        static public string Add_Sub(string str)
        { // сложение и вычитание
            double firstNum, secondNum;
            Regex reg = new Regex(@"(\-*[\d,]+)([\+\-]{1})(\-*[\d,]+)");
            Match mat = reg.Match(str);
            if (mat.Groups.Count > 2)
            {
                firstNum = double.Parse(mat.Groups[1].ToString());
                secondNum = double.Parse(mat.Groups[3].ToString());
                if (mat.Groups[2].ToString()[0] == '+')
                    firstNum += secondNum;
                else if (mat.Groups[2].ToString()[0] == '-')
                    firstNum -= secondNum;
                return str.Replace(mat.Groups[0].ToString(), firstNum.ToString());
            }
            return str;
        }

        static public string Chars(string str)
        { // разбор скобок
            char[] buf = { '*', '/' };
            Regex reg = new Regex(@"\(([^\(\)]+)\)");
            if (!reg.IsMatch(str))
                return str;
            Match mat = reg.Match(str);
            string cm = mat.Groups[1].ToString();
            while (cm.IndexOfAny(buf) != -1)
                cm = Calculator.Mul_Div(cm);
            Regex test = new Regex(@"^(\-*[\d,]+)$");
            while (!test.IsMatch(cm))
                cm = Calculator.Add_Sub(cm);
            str = str.Replace(mat.Groups[0].ToString(), cm);
            return Chars(str);
        }

        static public double Calculate(string str)
        {
            str = Chars(str);
            char[] buf = { '*', '/' };
            while (str.IndexOfAny(buf) != -1)
                str = Calculator.Mul_Div(str);
            Regex test = new Regex(@"^(\-*[\d,]+)$");
            while (!test.IsMatch(str))
                str = Calculator.Add_Sub(str);
            return double.Parse(str);
        }
    }
}
