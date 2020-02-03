using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Classes
{
	public class Point
	{
		public int X { get; }
		public int Y { get; }

		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public Point(string input)
		{
			input = input.TrimEnd();
			input = input.TrimStart();

			while (input.Contains("  ")) input = input.Replace("  ", " ");

			int[] values = input.Split(' ').Select(Int32.Parse).ToArray();

			if (values.Length != 2) throw new Exception("Only 2 values could be provided");

			this.X = values[0];
			this.Y = values[1];
		}
	}
}
