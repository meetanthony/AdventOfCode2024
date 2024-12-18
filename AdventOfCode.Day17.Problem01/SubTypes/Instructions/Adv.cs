using System;

namespace AdventOfCode.Day17.Problem01.SubTypes.Instructions;

internal class Adv : InstructionBase
{
    protected override void DoThingsImpl(int operand, Registers registers, ref int instructionPointer)
    {
        var combo = GetComboOperand(operand, registers);
        var a = registers.A;
        registers.A = (int)(a / Math.Pow(2, combo));
        instructionPointer += 2;
    }

    public override InstructionIds InstructionId => InstructionIds.Adv;
}