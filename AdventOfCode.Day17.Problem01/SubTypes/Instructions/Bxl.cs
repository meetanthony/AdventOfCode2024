namespace AdventOfCode.Day17.Problem01.SubTypes.Instructions;

internal class Bxl : InstructionBase
{
    protected override void DoThingsImpl(int operand, Registers registers, ref int instructionPointer)
    {
        registers.B ^= operand;
        instructionPointer += 2;
    }

    public override InstructionIds InstructionId => InstructionIds.Bxl;
}