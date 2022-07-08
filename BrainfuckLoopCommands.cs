using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var hooks = new Dictionary<int, int>();
			var stack = new Stack<int>();

			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				if (vm.Instructions[i] == '[')
					stack.Push(i);
				if (vm.Instructions[i] == ']')
				{
					int indexHook = stack.Pop();
					hooks.Add(indexHook, i);
					hooks.Add(i, indexHook);
				}
			}

			vm.RegisterCommand('[', b => {			
				if(vm.Memory[vm.MemoryPointer] == 0)
					vm.InstructionPointer = hooks[vm.InstructionPointer];					
			});
			
			vm.RegisterCommand(']', b => {			
				if(vm.Memory[vm.MemoryPointer] != 0)				
					vm.InstructionPointer = hooks[vm.InstructionPointer];
			});
		}
	}
}