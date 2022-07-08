using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
        static List<char> commands = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 
        'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 
        'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7',
        '8', '9' };
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
            vm.RegisterCommand('.', b => { write(Convert.ToChar(vm.Memory[vm.MemoryPointer])); });
            
            vm.RegisterCommand('+', b => { unchecked { vm.Memory[vm.MemoryPointer]++; } });
            
            vm.RegisterCommand('-', b => { unchecked { vm.Memory[vm.MemoryPointer]--; } });
            
            vm.RegisterCommand(',', b => { vm.Memory[vm.MemoryPointer] = Convert.ToByte(read()); });
            
            vm.RegisterCommand('>', b => {                 
                vm.MemoryPointer++;
                if (vm.MemoryPointer >= vm.Memory.Length)
                    vm.MemoryPointer = 0;
                else if(vm.MemoryPointer < 0)
                    vm.MemoryPointer = vm.Memory.Length - 1;
            });
            
            vm.RegisterCommand('<', b => {               
                vm.MemoryPointer--;
                if (vm.MemoryPointer >= vm.Memory.Length)
                    vm.MemoryPointer = 0;
                else if (vm.MemoryPointer < 0)
                    vm.MemoryPointer = vm.Memory.Length - 1;
            });
            
            foreach(var symbol in commands)
                vm.RegisterCommand(symbol, b => { vm.Memory[vm.MemoryPointer] = Convert.ToByte(symbol); });
            
        }
	}
}