namespace AdventOfCode.Day17.Problem01.SubTypes.Instructions;

internal class Jnz : InstructionBase
{
    protected override void DoThingsImpl(int operand, Registers registers, ref int instructionPointer)
    {
        if (registers.A == 0)
            return;

        instructionPointer = operand;
    }

    public override InstructionIds InstructionId => InstructionIds.Jnz;
}