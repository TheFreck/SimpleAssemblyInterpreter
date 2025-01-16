namespace AssemblyInterpreter.Entities
{
    public static class Logic
    {
        private static (string, string) GetBins(int source1, int source2)
        {
            var bin1 = Convert.ToString(source1, 2);
            var bin2 = Convert.ToString(source2, 2);
            if (bin1.Length > bin2.Length)
            {
                bin2 = bin2.PadLeft(bin1.Length, '0');
            }
            else if (bin2.Length > bin1.Length)
            {
                bin1 = bin1.PadLeft(bin2.Length, '0');
            }
            return (bin1, bin2);
        }

        public static int GetAnd(int source1, int source2)
        {
            var (s1, s2) = GetBins(source1, source2);
            var output = String.Empty;
            for (var i = s1.Length-1; i >= 0; i--)
            {
                if (s1[i] == '1' && s2[i] == '1')
                {
                    output = '1' + output;
                }
                else
                {
                    output = '0' + output;
                }
            }
            return Convert.ToInt32(output, 2);
        }

        public static int GetOr(int source1, int source2)
        {
            var (s1, s2) = GetBins(source1, source2);
            var output = String.Empty;
            for (var i = s1.Length-1; i >= 0; i--)
            {
                if (s1[i] == '1' || s2[i] == '1')
                {
                    output = '1' + output;
                }
                else
                {
                    output = '0' + output;
                }
            }
            return Convert.ToInt32(output, 2);
        }

        public static int GetNot(int source1)
        {
            var (s1, s2) = GetBins(source1, source1);
            var output = String.Empty;
            for (var i = s1.Length - 1; i >= 0; i--)
            {
                if (s1[i] == '1')
                {
                    output = '0' + output;
                }
                else
                {
                    output = '1' + output;
                }
            }
            return Convert.ToInt32(output, 2);
        }

        public static int GetXor(int source1, int source2)
        {
            var (s1, s2) = GetBins(source1, source2);
            var output = String.Empty;
            for (var i = s1.Length-1; i >= 0; i--)
            {
                if (s1[i] != s2[i])
                {
                    output = '1' + output;
                }
                else
                {
                    output = '0' + output;
                }
            }
            return Convert.ToInt32(output, 2);
        }

        public static int ShiftRight(int source) => source / 2;

        public static int ShiftLeft(int source) => source * 2;

        public static int GetMultiply(int source1, int source2) => source1 * source2;

        public static bool GetEq(int source1, int source2) => source1 == source2;

        public static bool GetNeq(int source1, int source2) => source1 != source2;

        public static bool GetLt(int source1, int source2) => source1 < source2;

        public static bool GetLte(int source1, int source2) => source1 <= source2;

        public static bool GetGt(int source1, int source2) => source1 > source2;

        public static bool GetGte(int source1, int source2) => source1 >= source2;
    }
}
