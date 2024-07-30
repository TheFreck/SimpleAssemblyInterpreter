using Machine.Specifications;

namespace AssemblyReader.Specs
{
    public class When_Reading_Commands
    {
        Establish context = () =>
        {
            //input = new string[] { "mov a 5", "mov b 3", "mov c 7", "mov d 0", "mov e 0", "mov f 0", "inc b", "mov f b"};
            //expect = new Dictionary<string, int> { 
            //    { "a", 5 },
            //    {"b", 4 },
            //    {"c", 7 },
            //    {"d", 0 },
            //    {"e",0 },
            //    {"f", 4 },
            //};
            input = new string[] {"mov a 5","inc a","dec a","dec a","jnz a -1","inc a","mov a -10","mov b a","inc a","dec b","jnz a -2","mov a 1","mov b 1","mov c 0",
                    "mov d 26","jnz c 2","jnz 1 5","mov c 7","inc d","dec c","jnz c -2","mov c a","inc a","dec b","jnz b -2","mov b c","dec d","jnz d -6","mov c 18",
                    "mov d 11","inc a","dec d","jnz d -2","dec c","jnz c -"
            };
            expect = new Dictionary<string, int>
            {
                { "a", -4 },
                { "b", 0 },
                { "c", 17 },
                { "d", 39 },
            };
        };

        Because of = () => answer = AssemblyReader.Interpret(input);

        It Should_Return_A_Dictionary_Of_Registers = () =>
        {
            foreach (var kvp in answer)
            {
                answer[kvp.Key] = expect[kvp.Key];
            }
        };

        private static string[] input;
        private static Dictionary<string, int> expect;
        private static Dictionary<string, int> answer;
    }
}