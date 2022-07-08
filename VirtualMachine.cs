using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		
		private Dictionary<char, Action<IVirtualMachine>> _actions;	

		public VirtualMachine(string program, int memorySize)
		{
			Instructions=program;
			Memory=new byte[memorySize];
			MemoryPointer= 0;
			
			if (Instructions.Length > 1)
				InstructionPointer = 0;
			
			_actions = new Dictionary<char, Action<IVirtualMachine>>();						
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			if(!_actions.ContainsKey(symbol) && execute!=null)
				_actions.Add(symbol, execute);			
		}

		public void Run()
		{
			if (InstructionPointer >= Instructions.Length)
				return;          
			
			if (_actions.Count > 0)
                for (int i = InstructionPointer; InstructionPointer < Instructions.Length; InstructionPointer++)
                {
					if(_actions.ContainsKey(Instructions[InstructionPointer]))
						_actions[Instructions[InstructionPointer]](this);					
				}
		}
	}
}



