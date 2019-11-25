using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AngieTools.Math
{
    public class MathTools
    {
        public static int Factor(int p_number)
        {
            return CalculateFactorial(p_number);
        }

        private static int CalculateFactorial(int p_number, int p_result = 0)
        {
            if (p_number > 1)
            {
                p_result += p_number * p_number - 1;
                return CalculateFactorial(p_number - 1, p_result);
            }

            return p_result;
        }
    }
}
