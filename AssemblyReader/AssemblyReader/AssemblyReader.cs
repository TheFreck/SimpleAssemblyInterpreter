using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssemblyReader
{
    public class AssemblyReader
    {
        public static Dictionary<string, int> Interpret(string[] program)
        {
            var registers = new Dictionary<string, int>();
            for(var i= 0; i < program.Length; i++)
            {
                var splits = program[i].Split(' ');
                if (splits.Length == 3 && splits[2] == "26")
                {
                    var wha = 4;
                }
                var val1 = 0;
                var val2 = 0;
                var isIntA = int.TryParse(splits[1], out val1);
                var isIntB = false;
                if(splits.Length == 3)
                {
                    if (splits[2] == "0") isIntB = true;
                    else if (int.TryParse(splits[2], out val2)) isIntB = true;
                    else isIntB = false;
                }
                switch (splits[0])
                {
                    case "mov":
                        if (!registers.Keys.Contains(splits[1]))
                        {
                            registers.Add(splits[1], isIntB ? val2 : registers[splits[2]]);
                        }
                        else
                        {
                            registers[splits[1]] += isIntB ? val2 : registers[splits[2]];
                        }
                        break;
                    case "inc":
                        registers[splits[1]] += 1;
                        break;
                    case "dec":
                        registers[splits[1]] -= 1;
                        break;
                    case "jnz":
                        if (int.TryParse(splits[1], out var first))
                        {
                            if (first != 0)
                            {
                                if (int.TryParse(splits[2], out var second))
                                {
                                    i--;
                                    i += second;
                                }
                                else
                                {
                                    i--;
                                    i += registers[splits[2]];
                                }
                            }
                        }
                        else
                        {
                            if (registers[splits[1]] != 0)
                            {
                                if (int.TryParse(splits[2], out var second))
                                {
                                    if (registers[splits[1]] != 0)
                                    {
                                        i--;
                                        i += second;
                                    }
                                }
                                else
                                {
                                    i--;
                                    i += registers[splits[2]];
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return registers;
        }
    }
}
