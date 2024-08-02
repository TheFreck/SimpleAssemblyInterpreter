using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var counter = 0;
            var counters = String.Empty;
            do
            {
                var commands = program[counter].Split(' ');
                var val1 = commands.Length > 1 ? commands[1].ToCharArray() : new char[0];
                var val2 = commands.Length > 2 ? commands[2].ToCharArray() : new char[0];
                switch (commands[0])
                {
                    case "mov":
                        if (val2.Length > 1 || val2[0] < 58)
                        {
                            if (registers.Keys.Contains(commands[1])) registers[commands[1]] += Convert.ToInt32(commands[2]);
                            else registers.Add(commands[1], Convert.ToInt32(commands[2]));
                        }
                        else
                        {
                            if (registers.Keys.Contains(commands[1])) registers[commands[1]] += registers[commands[2]];
                            else registers.Add(commands[1], Convert.ToInt32(registers[commands[2]]));
                        }
                        break;
                    case "inc":
                        registers[commands[1]] += 1;
                        break;
                    case "dec":
                        registers[commands[1]] -= 1;
                        break;
                    case "jnz":
                        if (val1.Length > 1 || val1[0] < 58)
                        {
                            if (commands[1] == "0") break;
                            if(val2.Length > 1 || val2[0] < 58) counter += Convert.ToInt32(commands[2])-1;
                            else counter += Convert.ToInt32(registers[commands[2]])-1;
                        }
                        else
                        {
                            if (registers[commands[1]] == 0) break;
                            if(val2.Length > 1 || val2[0] < 58) counter += Convert.ToInt32(commands[2])-1;
                            else counter += Convert.ToInt32(registers[commands[2]])-1;
                        }
                        break;
                    default:
                        break;
                }
                counter++;
                counters += counter + ",";
            } while (counter < program.Length);

            return registers;
        }
    }
}
