using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Classes
{
	class Matrix
	{
		protected List<Equation> listOfEquations = new List<Equation>();

		private int width = 0;
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		public int Length
		{
			get
			{
				return listOfEquations.Count;
			}
		}

		public Equation GetEquation(int index)
		{
			return listOfEquations[index];
		}

		public Equation SetEquation(int index, Equation e)
		{
			return listOfEquations[index] = e;
		}

		public void ForwardElimination()
		{
			int N0 = this.Length;
			int N1 = this.Width;

			for (int k = 0; k < N0; k++)
			{
				for (int i = k + 1; i < N1 - 1; i++)
				{
					double factor = this.GetEquation(i).Array[k] / this.GetEquation(k).Array[k];

					for (int j = k; j < N1; j++)
						this.GetEquation(i).Array[j] -= factor * this.GetEquation(k).Array[j];
				}
			}
		}

		private double[] solveMatrix()
		{
			int length = this.GetEquation(0).Array.Length;

			for (int i = 0; i < this.Length - 1; i++)
			{
				if (this.GetEquation(i).Array[i] == 0 && !pivotProcedure(this, i, i))
					throw new Exception("Unable to calculate");
				
				for (int j = i; j < this.Length; j++)
				{
					double[] d = new double[length];

					for (int x = 0; x < length; x++)
					{
						d[x] = this.GetEquation(j).Array[x];
						if (this.GetEquation(j).Array[i] != 0)
							d[x] = d[x] / this.GetEquation(j).Array[i];
					}
					this.SetEquation(j, new Equation(d));
				}
				for (int z = i + 1; z < this.Length; z++)
				{
					double[] f = new double[length];
					for (int g = 0; g < length; g++)
					{
						f[g] = this.GetEquation(z).Array[g];
						if (this.GetEquation(z).Array[i] != 0)
							f[g] = f[g] - this.GetEquation(i).Array[g];
					}
					this.SetEquation(z, new Equation(f));
				}
			}

			return BackwardSubstitution(this);
		}

		private bool pivotProcedure(Matrix matrix, int row, int column)
		{
			bool swapped = false;
			for (int z = matrix.Length - 1; z > row; z--)
			{
				if (matrix.GetEquation(z).Array[row] != 0)
				{
					double[] temp = new double[matrix.GetEquation(0).Array.Length];
					temp = matrix.GetEquation(z).Array;
					matrix.SetEquation(z, matrix.GetEquation(column));
					matrix.SetEquation(column, new Equation(temp));
					swapped = true;
				}
			}

			return swapped;
		}
		public double[] BackwardSubstitution(Matrix matrix)
		{
			double val = 0;
			int length = matrix.GetEquation(0).Array.Length;
			double[] result = new double[matrix.Length];
			for (int i = matrix.Length - 1; i >= 0; i--)
			{
				val = matrix.GetEquation(i).Array[length - 1];
				for (int j = length - 2; j > i - 1; j--)
					val -= matrix.GetEquation(i).Array[j] * result[j];

				result[i] = val / matrix.GetEquation(i).Array[i];
			}
			return result;
		}
		private bool Validate(double result)
		{
			return !(double.IsNaN(result) || double.IsInfinity(result));
		}

		public void PrintResults()
		{
			double[] result = this.solveMatrix();

			for (int i = 0; i < result.Length; i++)
				Console.WriteLine($"x{i + 1} = {result[i].ToString("0.0000")}");
		}

		public double[] GetResults()
		{
			return this.solveMatrix();
		}

		public void AddEquation(Equation e)
		{
			if (this.listOfEquations.Count == 0)
			{
				this.width = e.Array.Length;
			}

			if (e.Array.Length != this.width)
				throw new Exception("Invalid equation added, wrong dimensions!");

			this.listOfEquations.Add(e);
		}

		public void AddEquation(Point p)
		{
			this.listOfEquations.Add(new Equation($"{p.X} {p.Y}"));
		}

		public void RemoveEquation(Equation e)
		{
			this.listOfEquations.Remove(e);
		}

		public override string ToString()
		{
			String res = "";
			foreach (Equation e in listOfEquations)
				res += String.Join(" ", e.Array) + "\n";

			return res;
		}
	}
}
