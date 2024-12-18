using System;
using AdventOfCode.Day17.Problem01.SubTypes.Instructions;

namespace AdventOfCode.Day17.Problem01.SubTypes;

internal abstract class InstructionBase
{
    public static InstructionBase GetInstruction(int instructionCode)
    {
        var instructionId = (InstructionIds)instructionCode;
        InstructionBase result;
        switch (instructionId)
        {
            case InstructionIds.Adv:
                result = new Adv();
                break;
            case InstructionIds.Bxl:
                result = new Bxl();
                break;
            case InstructionIds.Bst:
                result = new Bst();
                break;
            case InstructionIds.Jnz:
                result = new Jnz();
                break;
            case InstructionIds.Bxc:
                result = new Bxc();
                break;
            case InstructionIds.Out:
                result = new Out();
                break;
            case InstructionIds.Bdv:
                result = new Bdv();
                break;
            case InstructionIds.Cdv:
                result = new Cdv();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    public void DoThings(int operand, Registers registers, ref int instructionPointer)
    {
       DoThingsImpl(operand, registers, ref instructionPointer);
    }

    protected int GetComboOperand(int operand, Registers registers)
    {
        switch (operand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return operand;

            case 4:
                return registers.A;
            case 5:
                return registers.B;
            case 6:
                return registers.C;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected abstract void DoThingsImpl(int operand, Registers registers, ref int instructionPointer);

    public abstract InstructionIds InstructionId { get; }
}