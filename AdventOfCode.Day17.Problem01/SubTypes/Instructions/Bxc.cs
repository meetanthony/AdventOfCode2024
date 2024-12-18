namespace AdventOfCode.Day17.Problem01.SubTypes.Instructions;

internal class Bxc : InstructionBase
{
    protected override void DoThingsImpl(int operand, Registers registers, ref int instructionPointer)
    {
        registers.B ^= registers.C;
        instructionPointer += 2;
    }

    public override InstructionIds InstructionId => InstructionIds.Bxc;
}