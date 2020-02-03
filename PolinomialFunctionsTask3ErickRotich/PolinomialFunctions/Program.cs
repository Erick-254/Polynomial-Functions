using Project3.Classes;
using System;
using System.Collections.Generic;
 
namespace Project3
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Please input points in x y representation.");
			Console.WriteLine("Type END to finish.");

			int index = 1;

			try
			{
				VandermondeMatrix matrix = new VandermondeMatrix();

				while (true)
				{
					Console.Write($"P#{index++}: ");

					String input = Console.ReadLine();

					if (input.Trim().ToUpper() == "END" || input.Trim() == "")
						break;

					matrix.AddEquation(new Point(input));
				}

				matrix.toVandermonde();

				Console.WriteLine("Resulting polynomial will be of the order - " + Polynomial.CalculateOrder(matrix));
				Console.WriteLine("Calculated polynomial:");

				double[] coefficients = matrix.GetResults();

				Console.WriteLine(Polynomial.Format(coefficients));

				Dictionary<int, double> calculatedPolynomial = Polynomial.Calculate(coefficients);

				foreach(KeyValuePair<int, double> value in calculatedPolynomial)
					Console.WriteLine($"f({value.Key}) = {value.Value.ToString("0.000")}");

				Console.WriteLine("Derivative:\n" + Polynomial.CalculateDerivative(coefficients));
				double inititalGuess = 2;
				Console.WriteLine("Looking for a root with initial guess 2");
				Console.WriteLine("Root found for x = " + Polynomial.CalculateRoot(coefficients, inititalGuess).ToString("0.00000"));
			}
			catch (Exception e)
			{
				Console.WriteLine("Error occured: " + e.Message);
			}
		}
	}
}
