using System;

namespace AdventOfCode.Day17.Problem01.SubTypes.Instructions;

internal class Out : InstructionBase
{
    static bool FirstOutput = true;

    protected override void DoThingsImpl(int operand, Registers registers, ref int instructionPointer)
    {
        if (FirstOutput)
            FirstOutput = false;
        else
            Console.Write(',');
        var combo = GetComboOperand(operand, registers);
        Console.Write(combo%8);
        instructionPointer += 2;
    }

    public override InstructionIds InstructionId => InstructionIds.Out;
}