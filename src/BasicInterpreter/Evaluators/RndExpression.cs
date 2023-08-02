using System;

namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The RndExpression class represents a random number generation operation. 
    /// It implements the IExpression interface, which defines a method for evaluating an expression.
    /// </summary>
    public class RndExpression : IExpression
    {
        private Random random;

        /// <summary>
        /// Initializes a new instance of the RndExpression class with the default seed.
        /// </summary>
        public RndExpression()
        {
            random = new Random();
        }

        /// <summary>
        /// Initializes a new instance of the RndExpression class with the specified seed.
        /// </summary>
        /// <param name="seed">The seed value used for random number generation.</param>
        public RndExpression(int seed)
        {
            random = new Random(seed);
        }

        /// <summary>
        /// Evaluates the random number generation expression.
        /// </summary>
        /// <param name="operands">The operands of the expression (not used in this case).</param>
        /// <returns>A random double value between 0.0 and 1.0.</returns>
        public object Evaluate(params object[] operands)
        {
            return random.NextDouble();
        }
    }
}
