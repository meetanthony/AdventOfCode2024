namespace AdventOfCode.Day17.Problem01.SubTypes.Instructions;

internal class Bst : InstructionBase
{
    protected override void DoThingsImpl(int operand, Registers registers, ref int instructionPointer)
    {
        var combo = GetComboOperand(operand, registers);
        registers.B = combo % 8;
        instructionPointer += 2;
    }

    public override InstructionIds InstructionId => InstructionIds.Bst;
}