namespace AssemblyInterpreter.Entities
{
    public class OutputState
    {
        public Dictionary<string, int> Registers { get; set; }
        public int[] RAM { get; set; }
        public List<int> Stack { get; set; }
    }
}
