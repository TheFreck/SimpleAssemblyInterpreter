namespace AssemblyInterpreter.DTO
{
    public class Instruction
    {
        public int Command {  get; set; }
        public int Source1 { get; set; }
        public int Source2 { get; set; }
        public int Destination {  get; set; }
    }
}
