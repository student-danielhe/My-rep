using System;

using System.Collections;

using System.Text.RegularExpressions;

namespace FormulaEvaluator

{

    /// <summary>

    /// This class take any formula in string with variable and calculate them to int value

    /// </summary>





    public static class Evaluator
    {

        public delegate int Lookup(String variable_name);

        ///<summary>

        ///This method take a formula in form of string and evaluate it.

        ///</summary>

        /// <return>

        /// The result of the formula string

        /// </return>

        /// <param name="s">

        /// A formual in string format.

        /// </param>

        /// <param name="variableEvaluator">

        /// A function take a string variable and convert it to a preset value.

        /// </param>

        public static int Evaluate(String s, Lookup variableEvaluator)

        {

            //get rid of the ' 's

            s = s.Replace(" ", String.Empty);

            //get a list of tokens each represent a number or operation

            string[] substrings = Regex.Split(s, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            //split numbers and operations into 2 stacks to calculate them

            Stack values = new Stack();

            Stack operation = new Stack();



            //go through the substring and calculate it as how assignment instruction says..

            for (int i = 0; i < substrings.Length; i++)

            {

                if (string.Equals(substrings[i], "+") || string.Equals(substrings[i], "-"))

                {

                    //If + or - is at the top of the operator stack, pop the value stack twice and the operator stack once, then apply the popped operator to the popped numbers, then push the result onto the value stack.



                    //Push t onto the operator stack

                    if (string.Equals(StackTop(operation, false), "+") || string.Equals(StackTop(operation, false), "-"))

                    {

                        values.Push(compute((int)StackTop(values, true), (int)StackTop(values, true), (string)StackTop(operation, true)));



                    }

                    operation.Push(substrings[i]);



                }

                else if (string.Equals(substrings[i], "*") || string.Equals(substrings[i], "/"))

                {

                    operation.Push(substrings[i]);

                }



                else if (string.Equals(substrings[i], "("))

                {

                    operation.Push(substrings[i]);

                }

                else if (string.Equals(substrings[i], ")"))

                {

                    if (values.Count < 2)

                    {

                        Console.WriteLine("invalid");

                    }

                    //If + or - is at the top of the operator stack, pop the value stack twice and the operator stack once. Apply the popped operator to the popped numbers. Push the result onto the value stack.



                    if (string.Equals(StackTop(operation, false), "+") || string.Equals(StackTop(operation, false), "-"))

                    {

                        values.Push(compute((int)StackTop(values, true), (int)StackTop(values, true), (string)StackTop(operation, true)));



                    }//Next, the top of the operator stack should be a '('. Pop it.

                    if (string.Equals(StackTop(operation, false), "("))

                    {

                        operation.Pop();

                    }

                    //Finally, if * or / is at the top of the operator stack, pop the value stack twice and the operator stack once. Apply the popped operator to the popped numbers. Push the result onto the value stack.

                    if (string.Equals(StackTop(operation, false), "*") || string.Equals(StackTop(operation, false), "/"))

                    {

                        values.Push(compute((int)StackTop(values, true), (int)StackTop(values, true), (string)StackTop(operation, true)));



                    }

                }



                else

                {

                    if (substrings[i].Length != 0)
                    {

                        int val = 0;

                        string number = substrings[i];



                        if (!Char.IsNumber(number[0]))

                        {

                            val = variableEvaluator(substrings[i]);

                        }

                        else

                        {

                            //get the value

                            for (int i2 = 0; i2 < number.Length; i2++)

                            {

                                val = val * 10;

                                val += Convert.ToInt32(Char.GetNumericValue(number, i2));



                            }

                        }

                        //If * or / is at the top of the operator stack, pop the value stack, pop the operator stack, and apply the popped operator to the popped number and t. Push the result onto the value stack.

                        if (string.Equals(StackTop(operation, false), "*") || string.Equals(StackTop(operation, false), "/"))

                        {

                            values.Push(compute(val, (int)StackTop(values, true), (string)StackTop(operation, true)));

                        }

                        else

                        {

                            values.Push(val);

                        }

                    }



                }













            }

            //There should be exactly one operator on the operator stack, and it should be either + or -. There should be exactly two values on the value stack. Apply the operator to the two values and report the result as the value of the expression.

            while (values.Count > 1)

            {

                values.Push(compute((int)StackTop(values, true), (int)StackTop(values, true), (string)StackTop(operation, true)));

            }

            //Value stack should contain a single number



            //Pop it and report as the value of the expression

            return (int)StackTop(values, true);

        }

        ///<summary>

        ///This method take a 2 ints and an operation then apply them

        ///</summary>

        /// <return>

        /// The result of x operate y

        /// </return>

        /// <param name="y">

        /// the int at last to compute.

        /// </param>

        /// <param name="x">

        /// the first operation to compute.

        /// </param>

        /// <param name="operation">

        ///the operation to apply to the ints.

        /// </param>>

        public static int compute(int y, int x, string operation)

        {

            int result = 0;

            if (String.Equals(operation, "+"))

            {

                result = x + y;

            }

            else if (String.Equals(operation, "-"))

            {

                result = x - y;

            }

            else if (String.Equals(operation, "*"))

            {

                result = x * y;

            }

            else if (String.Equals(operation, "/"))

            {

                if (y != 0)

                {

                    result = x / y;

                }

                else

                {



                    Console.WriteLine("can't divide 0");

                }

            }

            else

            {

                Console.WriteLine("invalid operation"); ;

            }

            return result;



        }

        ///<summary>

        ///This method return top of stack if there is one, and remove it if pop is true

        ///</summary>

        /// <return>

        /// The top of stack, or null if nothing is there

        /// </return>

        /// <param name="stak">

        /// the stack to look at.

        /// </param>

        /// <param name="pop">

        /// true to remove the top false to just peek the top.

        /// </param>

        public static object StackTop(Stack stak, bool pop)

        {

            if (stak.Count != 0)

            {



                if (pop)

                {

                    return stak.Pop();

                }

                else

                {

                    return stak.Peek();

                }

            }

            Console.WriteLine("Stack is empty");

            return null;

        }

    }







}