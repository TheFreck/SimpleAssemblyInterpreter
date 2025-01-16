using AssemblyInterpreter.DTO;
using AssemblyInterpreter.Entities;
using Machine.Specifications;

namespace AssemblyInterpreter.Specs
{
    public class When_Reading_Commands
    {
        Establish context = () =>
        {
            input = new string[][]
            {
                new string[]
                {
                    "mov a 1",
                    "mov b 2",
                    "mov c 3",
                    "mov d 4",
                    "mov e 5",
                    "mov f 6"
                },
                new string[]
                {
                    "mov a 5",
                    "mov b a",
                    "mov a 7",
                    "mov c a",
                    "mov d b",
                    "mov e a",
                    "mov f b"
                },
                new string[]
                {
                    "mov a 1",
                    "mov b 2",
                    "mov c 3",
                    "mov d 4",
                    "mov e 5",
                    "mov f 6",
                    "inc a",
                    "inc b",
                    "inc c",
                    "inc d",
                    "inc e",
                    "inc f"
                },
                new string[]
                {
                    "mov a 1",
                    "mov b 2",
                    "mov c 3",
                    "mov d 4",
                    "mov e 5",
                    "mov f 6",
                    "dec a",
                    "dec b",
                    "dec c",
                    "dec d",
                    "dec e",
                    "dec f"
                },
                new string[]
                {
                    "mov a 1",
                    "mov b 2",
                    "mov c 3",
                    "mov d 4",
                    "mov e 5",
                    "mov f 6",
                    "dec a",
                    "jnz a -2",
                    "jnz a -1",
                    "mov a 0",
                    "jnz a 1",
                    "mov e 5"
                },
                new string[]
                {
                    "mov a 5",
                    "inc a",
                    "dec a",
                    "dec a",
                    "jnz a -1",
                    "inc a",
                },
                new string []
                {
                    "mov g 369",
                    "mov r 2",
                    "jnz r 4",
                    "jnz g 4",
                    "dec r",
                    "inc r",
                    "inc r",
                    "dec r",
                    "dec r",
                    "inc r",
                    "dec g",
                    "inc r",
                },
                new string[]
                {
                    "mov s 85",
                    "mov u 2",
                    "jnz u 5",
                    "jnz s 3",
                    "dec u",
                    "dec s",
                    "inc u",
                    "inc s",
                    "inc s",
                    "dec u",
                    "dec s",
                    "dec u",
                    "inc s",
                    "dec u",
                    "dec s"
                },
                new string[] {
                    "mov u 364",
                    "mov c 1",
                    "jnz c 3",
                    "jnz u 4",
                    "inc u",
                    "inc u",
                    "inc c",
                    "inc c",
                    "inc u",
                    "inc c",
                    "dec c",
                    "inc c",
                    "inc c",
                },
                new string[]
                {
                    "mov a 349",
                    "mov f 2",
                    "jnz f 5",
                    "jnz a 2",
                    "dec f",
                    "dec f",
                    "dec a",
                    "inc f",
                    "dec a",
                    "inc f",
                    "dec f"
                },
                new string[]
                {
                    "mov a 10349",
                    "dec a",
                    "jnz a -1"
                },
            };
            expect = new Dictionary<string, int>[]
            {
                new Dictionary<string, int>
                {
                    {"a", 1 },
                    {"b", 2 },
                    {"c", 3 },
                    {"d", 4 },
                    {"e", 5 },
                    {"f", 6 },
                },
                new Dictionary<string, int>
                {
                    {"a", 12 },
                    {"b", 5 },
                    {"c", 12 },
                    {"d", 5 },
                    {"e", 12 },
                    {"f", 5 },
                },
                new Dictionary<string, int>
                {
                    {"a", 2 },
                    {"b", 3 },
                    {"c", 4 },
                    {"d", 5 },
                    {"e", 6 },
                    {"f", 7 },
                },
                new Dictionary<string, int>
                {
                    {"a", 0 },
                    {"b", 1 },
                    {"c", 2 },
                    {"d", 3 },
                    {"e", 4 },
                    {"f", 5 },
                },
                new Dictionary<string, int>
                {
                    { "a", 0 },
                    {"b", 2 },
                    {"c", 3 },
                    {"d", 4 },
                    {"e", 10 },
                    {"f", 6 },
                },
                new Dictionary<string, int>{
                    { "a", 1 }
                },
                new Dictionary<string, int>
                {
                    { "g", 368 },
                    { "r", 3 }
                },
                new Dictionary<string, int>
                {
                    { "s", 86 },
                    { "u", -1 }
                },
                new Dictionary<string, int>
                {
                    { "u", 366 },
                    {"c", 5 }
                },
                new Dictionary<string, int>
                {
                    { "a", 348 },
                    { "f", 3 }
                },
                new Dictionary<string, int>
                {
                    { "a", 0 }
                }
            };
            answer = new Dictionary<string, int>[input.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                answer[i] = AssemblyReader.InterpretSimple(input[i]);
            }
        };

        It Should_Return_A_Dictionary_Of_Registers = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                foreach (var kvp in answer[i])
                {
                    if (answer[i][kvp.Key] != expect[i][kvp.Key])
                    {
                        var exp = expect[i][kvp.Key];
                        var ans = answer[i][kvp.Key];
                        var inp = input[i];
                    }
                    answer[i][kvp.Key].ShouldEqual(expect[i][kvp.Key]);
                }

            }
        };

        private static string[][] input;
        private static Dictionary<string, int>[] expect;
        private static Dictionary<string, int>[] answer;
    }

    public class When_Reading_A_Single_Command
    {
        Establish context = () =>
        {
            input = new Instruction[]
            {
                // read to r0
                new Instruction 
                {
                    Command = (int)Commands.read,
                    Source1 = 17,
                    Source2 = 0,
                    Destination = (int)Locations.r0 
                },
                //copy r0 to r1
                new Instruction
                {
                    Command = (int)Commands.copy,
                    Source1 = (int)Locations.r0,
                    Source2 = 0,
                    Destination = (int)Locations.r1
                },
                // 3+29 to r2
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.add,
                    Source1 = 3,
                    Source2 = 29,
                    Destination = (int)Locations.r2
                },
                // r0 + r1 to r3
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.add,
                    Source1 = (int)Locations.r0,
                    Source2 = (int)Locations.r1,
                    Destination = (int)Locations.r3
                },
                // r2 - r1 to r4
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.sub,
                    Source1 = (int)Locations.r1,
                    Source2 = (int)Locations.r2,
                    Destination = (int)Locations.r4
                },
                // 419 - 19 to r5
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.sub,
                    Source1 = 419,
                    Source2 = 19,
                    Destination = (int)Locations.r5
                },
                // 15 & 14 to output
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.and,
                    Source1 = 15,
                    Source2 = 14,
                    Destination = (int)Locations.output
                },
                // r1 & r4 to r0
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.and,
                    Source1 = (int)Locations.r1,
                    Source2 = (int)Locations.r4,
                    Destination = (int)Locations.r0
                },
                // 41 or 37 to r1
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.or,
                    Source1 = 41,
                    Source2 = 37,
                    Destination = (int)Locations.r1
                },
                // r2 or r4 to r2
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.or,
                    Source1 = (int)Locations.r2,
                    Source2 = (int)Locations.r4,
                    Destination = (int)Locations.r2
                },
                // not 21 to r3
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.not,
                    Source1 = 21,
                    Source2 = 0,
                    Destination = (int)Locations.r3
                },
                // not r1 to r4
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.not,
                    Source1 = (int)Locations.r1,
                    Source2 = 0,
                    Destination = (int)Locations.r4
                },
                // 10 xor 14 to r5
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.xor,
                    Source1 = 10,
                    Source2 = 14,
                    Destination = (int)Locations.r5
                },
                // r2 xor r3 to output
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.xor,
                    Source1 = (int)Locations.r2,
                    Source2 = (int)Locations.r3,
                    Destination = (int)Locations.output
                },
                // 5 >> 1 to r0
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.shiftRight,
                    Source1 = 5,
                    Source2 = 1,
                    Destination = (int)Locations.r0
                },
                // r1 >> r0 to r1
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.shiftRight,
                    Source1 = (int)Locations.r1,
                    Source2 = (int)Locations.r0,
                    Destination = (int)Locations.r1
                },
                // 4 << 4 to r2
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.shiftLeft,
                    Source1 = 4,
                    Source2 = 4,
                    Destination = (int)Locations.r2
                },
                // r1 << r5 to r3
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.shiftLeft,
                    Source1 =(int)Locations.r1,
                    Source2 = (int)Locations.r5,
                    Destination = (int)Locations.r3
                },
                // 23 * 54 to r4
                new Instruction
                {
                    Command = (int)Commands.read + (int)Commands.multiply,
                    Source1 = 23,
                    Source2 = 54,
                    Destination = (int)Locations.r4
                },
                // r1 * r2 to r5
                new Instruction
                {
                    Command = (int)Commands.copy + (int)Commands.multiply,
                    Source1 =(int)Locations.r1,
                    Source2 = (int)Locations.r2,
                    Destination = (int)Locations.r5
                },
                // 125 to stack
                new Instruction
                {
                    Command = (int)Commands.read,
                    Source1 = 125,
                    Source2 = 0,
                    Destination = (int)Locations.push
                },
                // r0 to stack
                new Instruction
                {
                    Command = (int)Commands.copy,
                    Source1 = (int)Locations.r0,
                    Source2 = 0,
                    Destination = (int)Locations.push
                },
                // r1 to stack
                new Instruction
                {
                    Command = (int)Commands.copy,
                    Source1 = (int)Locations.r1,
                    Source2 = 0,
                    Destination = (int)Locations.push
                },
                // r2 to stack
                new Instruction
                {
                    Command = (int)Commands.copy,
                    Source1 = (int)Locations.r2,
                    Source2 = 0,
                    Destination = (int)Locations.push
                },
                // pop stack to output
                new Instruction
                {
                    Command = (int)Commands.stack,
                    Source1 = 0,
                    Source2 = 0,
                    Destination = (int)Locations.output
                },
                // pop stack to r0
                new Instruction
                {
                    Command = (int)Commands.stack,
                    Source1 = 0,
                    Source2 = 0,
                    Destination = (int)Locations.r0
                },
                // pop stack to r1
                new Instruction
                {
                    Command = (int)Commands.stack,
                    Source1 = 0,
                    Source2 = 0,
                    Destination = (int)Locations.r1
                },
                // pop stack to r2
                new Instruction
                {
                    Command = (int)Commands.stack,
                    Source1 = 0,
                    Source2 = 0,
                    Destination = (int)Locations.r2
                },
                // 11 to r5
                new Instruction
                {
                    Command = (int)Commands.read,
                    Source1 = 11,
                    Source2 = 0,
                    Destination = (int)Locations.r5
                },
                // 12 to RAM
                new Instruction
                {
                    Command = (int)Commands.read,
                    Source1 = 12,
                    Source2 = 0,
                    Destination = (int)Locations.ram
                },
                // 4 to r5
                new Instruction
                {
                    Command = (int)Commands.read,
                    Source1 = 4,
                    Source2 = 0,
                    Destination = (int)Locations.r5
                },
                // r4 to RAM
                new Instruction
                {
                    Command = (int)Commands.copy,
                    Source1 = (int)Locations.r4,
                    Source2 = 0,
                    Destination = (int)Locations.ram
                },
                // 11 to r5
                new Instruction
                {
                    Command = (int)Commands.read,
                    Source1 = 11,
                    Source2 = 0,
                    Destination = (int)Locations.r5
                },
                // RAM[11] to r0
                new Instruction
                {
                    Command = (int)Commands.call,
                    Source1 = 0,
                    Source2 = 0,
                    Destination = (int)Locations.r0
                },
                // 4 to r5
                new Instruction
                {
                    Command = (int)Commands.read,
                    Source1 = 4,
                    Source2 = 0,
                    Destination = (int)Locations.r5
                },
                // RAM[4] to r1
                new Instruction
                {
                    Command = (int)Commands.call,
                    Source1 = 0,
                    Source2 = 0,
                    Destination = (int)Locations.r1
                },
            };
            expectations = new List<OutputState>
            {
                // read to r0
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",0 },
                        {"r2",0 },
                        {"r3",0 },
                        {"r4",0 },
                        {"r5",0 },
                        {"out",0 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                //copy r0 to r1
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",34 },
                        {"r2",0 },
                        {"r3",0 },
                        {"r4",0 },
                        {"r5",0 },
                        {"out",0 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 3+29 to r2
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",34 },
                        {"r2",32 },
                        {"r3",0 },
                        {"r4",0 },
                        {"r5",0 },
                        {"out",0 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r0 + r1 to r3
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",34 },
                        {"r2",32 },
                        {"r3",51 },
                        {"r4",0 },
                        {"r5",0 },
                        {"out",0 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r1 - r2 to r4
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",34 },
                        {"r2",32 },
                        {"r3",51 },
                        {"r4",2 },
                        {"r5",0 },
                        {"out",0 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 419 - 19 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",34 },
                        {"r2",32 },
                        {"r3",51 },
                        {"r4",2 },
                        {"r5",400 },
                        {"out",0 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 15 & 14 to output
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",17 },
                        {"r1",34 },
                        {"r2",32 },
                        {"r3",51 },
                        {"r4",2 },
                        {"r5",400 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r1 & r4 to r0
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",34 },
                        {"r2",32 },
                        {"r3",51 },
                        {"r4",2 },
                        {"r5",400 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 41 or 37 to r1
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",32 },
                        {"r3",51 },
                        {"r4",2 },
                        {"r5",400 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r2 or r4 to r2
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",34 },
                        {"r3",51 },
                        {"r4",2 },
                        {"r5",400 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // not 21 to r3
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",34 },
                        {"r3",10 },
                        {"r4",2 },
                        {"r5",400 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // not r1 to r4
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",34 },
                        {"r3",10 },
                        {"r4",18 },
                        {"r5",400 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 10 xor 14 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",34 },
                        {"r3",10 },
                        {"r4",18 },
                        {"r5",4 },
                        {"out",14 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r2 xor r3 to output
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",34 },
                        {"r3",10 },
                        {"r4",18 },
                        {"r5",4 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 5 >> 1 to r0
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",45 },
                        {"r2",34 },
                        {"r3",10 },
                        {"r4",18 },
                        {"r5",4 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r1 >> r0 to r1
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",34 },
                        {"r3",10 },
                        {"r4",18 },
                        {"r5",4 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 4 << 4 to r2
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",10 },
                        {"r4",18 },
                        {"r5",4 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r1 << r5 to r3
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",18 },
                        {"r5",4 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 23 * 54 to r4
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",4 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // r1 * r2 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 125 to stack
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125
                    }
                },
                // r0 to stack
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125,4
                    }
                },
                // r1 to stack
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125,4,13
                    }
                },
                // r2 to stack
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",40 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125,4,13,66
                    }
                },
                // pop stack to output
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",2 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",66 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125,4,13
                    }
                },
                // pop stack to r0
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",11 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",66 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125,4
                    }
                },
                // pop stack to r1
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",64 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",66 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>
                    {
                        125
                    }
                },
                // pop stack to r2
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",704 },
                        {"out",66 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 11 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",11 },
                        {"out",66 }
                    },
                    RAM = new int[256],
                    Stack = new List<int>{}
                },
                // 12 to RAM
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",11 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0,0,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
                // 4 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",4 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0,0,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
                // r4 to RAM
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",4 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0,1255,0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
                // 11 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",13 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",11 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0, 1255, 0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
                // RAM[11] to r0
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",12 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",11 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0, 1255, 0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
                // 4 to r5
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",12 },
                        {"r1",4 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",4 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0, 1255, 0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
                // RAM[4] to r1
                new OutputState
                {
                    Registers = new Dictionary<string, int>
                    {
                        {"r0",12 },
                        {"r1",1255 },
                        {"r2",125 },
                        {"r3",176 },
                        {"r4",1242 },
                        {"r5",4 },
                        {"out",66 }
                    },
                    RAM = new int[256]{0,0,0,0, 1255, 0,0,0,0,0,0,12,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                    Stack = new List<int>{}
                },
            };
            lineSuccess = new bool[input.Length];
            reader = new AssemblyReader();
        };

        Because of = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                lineSuccess[i] = reader.InterpretInstruction(input[i]);
            }
        };

        It Should_Return_All_True = () =>
        {
            foreach (var line in lineSuccess)
            {
                line.ShouldBeTrue();
            }
        };

        It Should_Update_Each_State_To_History = () =>
        {
            for(var i=0; i<input.Length; i++)
            {
                reader.History[i].Registers["r0"].ShouldEqual(expectations[i].Registers["r0"]);
                reader.History[i].Registers["r1"].ShouldEqual(expectations[i].Registers["r1"]);
                reader.History[i].Registers["r2"].ShouldEqual(expectations[i].Registers["r2"]);
                reader.History[i].Registers["r3"].ShouldEqual(expectations[i].Registers["r3"]);
                reader.History[i].Registers["r4"].ShouldEqual(expectations[i].Registers["r4"]);
                reader.History[i].Registers["r5"].ShouldEqual(expectations[i].Registers["r5"]);
                reader.History[i].Registers["out"].ShouldEqual(expectations[i].Registers["out"]);
                for(var j=0; j < reader.History[i].RAM.Length; j++)
                {
                    reader.History[i].RAM[j].ShouldEqual(expectations[i].RAM[j]);
                }
                for(var j=0; j < reader.History[i].Stack.Count; j++)
                {
                    reader.History[i].Stack[j].ShouldEqual(expectations[i].Stack[j]);
                }
            }
        };

        private static Instruction[] input;
        private static List<OutputState> expectations;
        private static bool[] lineSuccess;
        private static AssemblyReader reader;
    }
}