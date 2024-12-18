using AdventOfCode.Day17.Problem01.SubTypes;
using System.IO;
using AdventOfCode.Day17.Problem01.SubTypes.Instructions;

namespace AdventOfCode.Day17.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "TestData\\day17.txt";
        var lines = File.ReadAllLines(fileName);

        var registers = new Registers(lines);

        var operations = new Operations(lines[^1]);

        var operationPointer = 0;

        while (operationPointer < operations.Length)
        {
            var instruction = InstructionBase.GetInstruction(operations[operationPointer]);
            var operand = 0;
            if (operationPointer + 1 < operations.Length)
                operand = operations[operationPointer + 1];
            var oldOperationPointer = operationPointer;
            instruction.DoThings(operand, registers, ref operationPointer);
            if (instruction is Jnz && operationPointer == oldOperationPointer)
                break;
        }
    }
}