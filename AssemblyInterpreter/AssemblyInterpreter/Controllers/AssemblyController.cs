using AssemblyInterpreter.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AssemblyInterpreter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssemblyController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReadProgram([FromBody] string[] program)
        {
            var outcome = AssemblyReader.InterpretSimple(program);
            return Ok(outcome);
        }

        //[HttpPost("/line/")]
        //public IActionResult ReadLine()
        //{
        //    var reader = new AssemblyReader();
        //    var outcomes = reader.InterpretInstruction(new DTO.Instruction
        //    {
        //        Command = (int)Commands.shiftRight,
        //        Source1 = 4,
        //        Source2 = 0,
        //        Destination = (int)Locations.output
        //    });
        //    foreach(var outcome in outcomes)
        //    {
        //        Console.WriteLine(outcome);
        //    }
        //    return Ok(outcomes);
        //}
    }
}
