using System;



namespace Test_The_Evaluator_Console_App

{

    class Program

    {

        static void Main(string[] args)

        {





            //addtion,space test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("5 + 5", null));

            //minus& negative test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("1-2", null));

            //multiply order test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("1+2*2", null));

            //()test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2*(1+1)", null));

            //multiple () test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("10+9-8*7/6+((3-2)*1)", null));

            //multiply test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("3*3", null));

            //big number test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("2000000/200000", null));

            //variable test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("x1+x2", varaible));

            //0* test

            Console.WriteLine(FormulaEvaluator.Evaluator.Evaluate("0+0+0*(1+1)", null));









        }

        static int varaible(String name)

        {

            if (String.Equals(name, "x1"))

            {

                return 1;

            }

            if (String.Equals(name, "x2"))

            {

                return 2;

            }

            Console.WriteLine("can't find this varaible.");

            return 0;

        }

    }

}