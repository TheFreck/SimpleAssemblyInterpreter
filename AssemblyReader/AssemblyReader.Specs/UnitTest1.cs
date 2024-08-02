using Machine.Specifications;

namespace AssemblyReader.Specs
{
    public class When_Reading_Commands
    {
        Establish context = () =>
        {
            input = new string[][]
            {
                //new string[]
                //{
                //    "mov a 1",
                //    "mov b 2",
                //    "mov c 3",
                //    "mov d 4",
                //    "mov e 5",
                //    "mov f 6"
                //},
                //new string[]
                //{
                //    "mov a 5",
                //    "mov b a",
                //    "mov a 7",
                //    "mov c a",
                //    "mov d b",
                //    "mov e a",
                //    "mov f b"
                //},
                //new string[]
                //{
                //    "mov a 1",
                //    "mov b 2",
                //    "mov c 3",
                //    "mov d 4",
                //    "mov e 5",
                //    "mov f 6",
                //    "inc a",
                //    "inc b",
                //    "inc c",
                //    "inc d",
                //    "inc e",
                //    "inc f"
                //},
                //new string[]
                //{
                //    "mov a 1",
                //    "mov b 2",
                //    "mov c 3",
                //    "mov d 4",
                //    "mov e 5",
                //    "mov f 6",
                //    "dec a",
                //    "dec b",
                //    "dec c",
                //    "dec d",
                //    "dec e",
                //    "dec f"
                //},
                //new string[]
                //{
                //    "mov a 1",
                //    "mov b 2",
                //    "mov c 3",
                //    "mov d 4",
                //    "mov e 5",
                //    "mov f 6",
                //    "dec a",
                //    "jnz a -2",
                //    "jnz a -1",
                //    "mov a 0",
                //    "jnz a 1",
                //    "mov e 5"
                //},
                //new string[]
                //{
                //    "mov a 5",
                //    "inc a",
                //    "dec a",
                //    "dec a",
                //    "jnz a -1",
                //    "inc a",
                //},
                //new string []
                //{
                //    "mov g 369",
                //    "mov r 2",
                //    "jnz r 4",
                //    "jnz g 4",
                //    "dec r",
                //    "inc r",
                //    "inc r",
                //    "dec r",
                //    "dec r",
                //    "inc r",
                //    "dec g",
                //    "inc r",
                //},
                //new string[]
                //{
                //    "mov s 85",
                //    "mov u 2",
                //    "jnz u 5",
                //    "jnz s 3",
                //    "dec u",
                //    "dec s",
                //    "inc u",
                //    "inc s",
                //    "inc s",
                //    "dec u",
                //    "dec s",
                //    "dec u",
                //    "inc s",
                //    "dec u",
                //    "dec s"
                //},
                //new string[] {
                //    "mov u 364",
                //    "mov c 1",
                //    "jnz c 3",
                //    "jnz u 4",
                //    "inc u",
                //    "inc u",
                //    "inc c",
                //    "inc c",
                //    "inc u",
                //    "inc c",
                //    "dec c",
                //    "inc c",
                //    "inc c",
                //},
                //new string[]
                //{
                //    "mov a 349",
                //    "mov f 2",
                //    "jnz f 5",
                //    "jnz a 2",
                //    "dec f",
                //    "dec f",
                //    "dec a",
                //    "inc f",
                //    "dec a",
                //    "inc f",
                //    "dec f"
                //},
                //new string[]
                //{
                //    "mov a 10349",
                //    "dec a",
                //    "jnz a -1"
                //},
                //new string[]
                //{"mov a -10", "mov b a", "inc a", "dec b", "jnz a -2"},
                new string[]
                {
                    "mov a 5",
                    "inc a",
                    "dec a",
                    "dec a",
                    "jnz a -1",
                    "inc a",
                    "mov a -10",
                    "mov b a",
                    "inc a",
                    "dec b",
                    "jnz a -2",
                    "mov a 1",
                    "mov b 1",
                    "mov c 0",
                    "mov d 26",
                    "jnz c 2",
                    "jnz 1 5",
                    "mov c 7",
                    "inc d",
                    "dec c",
                    "jnz c -2",
                    "mov c a",
                    "inc a",
                    "dec b",
                    "jnz b -2",
                    "mov b c",
                    "dec d",
                    "jnz d -6",
                    "mov c 18",
                    "mov d 11",
                    "inc a",
                    "dec d",
                    "jnz d -2",
                    "dec c",
                    "jnz c -5",
                }
            };
            expect = new Dictionary<string, int>[]
            {
                //new Dictionary<string, int>
                //{
                //    {"a", 1 },
                //    {"b", 2 },
                //    {"c", 3 },
                //    {"d", 4 },
                //    {"e", 5 },
                //    {"f", 6 },
                //},
                //new Dictionary<string, int>
                //{
                //    {"a", 12 },
                //    {"b", 5 },
                //    {"c", 12 },
                //    {"d", 5 },
                //    {"e", 12 },
                //    {"f", 5 },
                //},
                //new Dictionary<string, int>
                //{
                //    {"a", 2 },
                //    {"b", 3 },
                //    {"c", 4 },
                //    {"d", 5 },
                //    {"e", 6 },
                //    {"f", 7 },
                //},
                //new Dictionary<string, int>
                //{
                //    {"a", 0 },
                //    {"b", 1 },
                //    {"c", 2 },
                //    {"d", 3 },
                //    {"e", 4 },
                //    {"f", 5 },
                //},
                //new Dictionary<string, int>
                //{
                //    { "a", 0 },
                //    {"b", 2 },
                //    {"c", 3 },
                //    {"d", 4 },
                //    {"e", 10 },
                //    {"f", 6 },
                //},
                //new Dictionary<string, int>{
                //    { "a", 1 }
                //},
                //new Dictionary<string, int>
                //{
                //    { "g", 368 },
                //    { "r", 3 }
                //},
                //new Dictionary<string, int>
                //{
                //    { "s", 86 },
                //    { "u", -1 }
                //},
                //new Dictionary<string, int>
                //{
                //    { "u", 366 },
                //    {"c", 5 }
                //},
                //new Dictionary<string, int>
                //{
                //    { "a", 348 },
                //    { "f", 3 }
                //},
                //new Dictionary<string, int>
                //{
                //    { "a", 0 }
                //},
                //new Dictionary<string, int>{ { "a", 0 }, { "b", -20 } }
            };
            answer = new Dictionary<string, int>[input.Length];
        };

        Because of = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                answer[i] = AssemblyReader.Interpret(input[i]);
            }
        };

        It Should_Return_A_Dictionary_Of_Registers = () =>
        {
            for (var i = 0; i < input.Length; i++)
            {
                foreach (var kvp in answer[i])
                {
                    if(answer[i][kvp.Key] != expect[i][kvp.Key])
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
}