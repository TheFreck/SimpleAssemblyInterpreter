using AssemblyInterpreter.DTO;
using AssemblyInterpreter.Entities;

namespace AssemblyInterpreter
{
    public class AssemblyReader
    {
        public List<OutputState> History {  get; set; }
        public Dictionary<string,int> Registers { get; set; }
        public int[] RAM { get; set; }
        public List<int> Stack { get; set; }
        public AssemblyReader()
        {
            History = new List<OutputState>();
            Registers = new Dictionary<string, int>
            {
                {"r0",0 },
                {"r1",0 },
                {"r2",0 },
                {"r3",0 },
                {"r4",0 },
                {"r5",0 },
                {"out",0 }
            };
            RAM = new int[256];
            Stack = new List<int>();
        }
        public static Dictionary<string, int> InterpretSimple(string[] program)
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
                            if (val2.Length > 1 || val2[0] < 58) counter += Convert.ToInt32(commands[2]) - 1;
                            else counter += Convert.ToInt32(registers[commands[2]]) - 1;
                        }
                        else
                        {
                            if (registers[commands[1]] == 0) break;
                            if (val2.Length > 1 || val2[0] < 58) counter += Convert.ToInt32(commands[2]) - 1;
                            else counter += Convert.ToInt32(registers[commands[2]]) - 1;
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

        public bool InterpretInstruction(Instruction instruction)
        {
            var isGood = true;
            var value = 0;
            switch (instruction.Command)
            {
                case (int)Commands.readAdd:
                    value = instruction.Source1 + instruction.Source2;
                    break;
                case (int)Commands.copyAdd:
                    value = Registers[((Locations)instruction.Source1).ToString()] 
                        + Registers[((Locations)instruction.Source2).ToString()];
                    break;
                case (int)Commands.readSub:
                    value = instruction.Source1 - instruction.Source2;
                    break;
                case (int)Commands.copySub:
                    value = Registers[((Locations)instruction.Source1).ToString()]
                        - Registers[((Locations)instruction.Source2).ToString()];
                    break;
                case (int)Commands.readMultiply:
                    value = instruction.Source1 * instruction.Source2; ;
                    break;
                case (int)Commands.copyMultiply:
                    value = Registers[((Locations)instruction.Source1).ToString()]
                        * Registers[((Locations)instruction.Source2).ToString()];
                    break;
                case (int)Commands.readAnd:
                    value = Logic.GetAnd(instruction.Source1, instruction.Source2);
                    break;
                case (int)Commands.copyAnd:
                    value = Logic.GetAnd(Registers[((Locations)instruction.Source1).ToString()], 
                        Registers[((Locations)instruction.Source2).ToString()]);
                    break;
                case (int)Commands.readOr:
                    value = Logic.GetOr(instruction.Source1, instruction.Source2);
                    break;
                case (int)Commands.copyOr:
                    value = Logic.GetOr(Registers[((Locations)instruction.Source1).ToString()],
                        Registers[((Locations)instruction.Source2).ToString()]);
                    break;
                case (int)Commands.readNot:
                    value = Logic.GetNot(instruction.Source1);
                    break;
                case (int)Commands.copyNot:
                    value = Logic.GetNot(Registers[((Locations)instruction.Source1).ToString()]);
                    break;
                case (int)Commands.readXor:
                    value = Logic.GetXor(instruction.Source1, instruction.Source2);
                    break;
                case (int)Commands.copyXor:
                    value = Logic.GetXor(Registers[((Locations)instruction.Source1).ToString()],
                        Registers[((Locations)instruction.Source2).ToString()]);
                    break;
                case (int)Commands.readShiftRight:
                    value = instruction.Source1 >> instruction.Source2;
                    break;
                case (int)Commands.copyShiftRight:
                    value = Registers[((Locations)instruction.Source1).ToString()]
                        >> Registers[((Locations)instruction.Source2).ToString()];
                    break;
                case (int)Commands.readShiftLeft:
                    value = instruction.Source1 << instruction.Source2;
                    break;
                case (int)Commands.copyShiftLeft:
                    value = Registers[((Locations)instruction.Source1).ToString()] 
                        << Registers[((Locations)instruction.Source2).ToString()];
                    break;
                case (int)Commands.call:
                    value = RAM[Registers["r5"]];
                    break;
                case (int)Commands.stack:
                    value = Stack.LastOrDefault();
                    Stack.Remove(Stack.LastOrDefault());
                    break;
                default: 
                    isGood = false;
                    break;
            }

            switch (instruction.Destination)
            {
                case (int)Locations.r0:
                case (int)Locations.r1: 
                case (int)Locations.r2: 
                case (int)Locations.r3: 
                case (int)Locations.r4: 
                case (int)Locations.r5:
                    Registers[((Locations)instruction.Destination).ToString()] = value;
                    break;
                case (int)Locations.ram:
                    RAM[Registers["r5"]] = value;
                    break;
                case (int)Locations.output:
                    Registers["out"] = value;
                    break;
                case (int)Locations.push:
                    Stack.Add(value);
                    break;
                default:
                    isGood = false;
                    break;
            }

            var ram = new int[256];
            RAM.CopyTo(ram, 0);
            var stack = new List<int>(Stack);
            History.Add(new OutputState
            {
                Registers = new Dictionary<string, int>
                {
                    {"r0", Registers["r0"]+0 },
                    {"r1", Registers["r1"]+0 },
                    {"r2", Registers["r2"]+0 },
                    {"r3", Registers["r3"]+0 },
                    {"r4", Registers["r4"]+0 },
                    {"r5", Registers["r5"]+0 },
                    {"out", Registers["out"]+0 },
                },
                RAM = ram,
                Stack = stack,
            });
            return isGood;
        }
    }
}
