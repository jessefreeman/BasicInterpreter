namespace JesseFreeman.BasicInterpreter.Evaluators
{
    /// <summary>
    /// The RndExpression class represents a pseudo-random number generation operation,
    /// mimicking the style of random number generation in Commodore 64 BASIC.
    /// The class implements the IExpression interface, which defines a method for evaluating an expression.
    /// 
    /// The Commodore 64's RND function used a pseudo-random number generator (PRNG) based on a linear congruential generator (LCG).
    /// An LCG is a simple type of PRNG that generates a sequence of numbers that approximates the properties of random numbers.
    /// The sequence is determined by a linear equation and a seed, which is updated each time a new random number is generated.
    /// The linear equation used by the C64's RND function was Xn+1 = (a * Xn + c) mod m, where Xn is the current seed,
    /// Xn+1 is the next seed, and a, c, and m are constants. The constants were chosen such that the generator had a long period
    /// (the number of values generated before the sequence repeats) and the values appeared random.
    ///
    /// However, because the generator was deterministic, if you knew the current seed and the constants, you could predict all future values.
    /// The RND function allowed you to set a new seed by passing a negative number, which made it possible to generate repeatable sequences of random numbers.
    /// This was useful for testing and debugging programs. If you passed a positive number to the RND function, it returned the last generated random number,
    /// and if you passed zero, it generated a new random number but didn't update the seed.
    /// </summary>
    public class RndExpression : IExpression
    {
        /// <summary>
        /// The seed for the random number generator. This is a state variable that gets updated each time
        /// a new random number is generated. The initial seed is set to the current time.
        /// </summary>
        private uint seed;

        /// <summary>
        /// The last random number that was generated. This is used to implement the feature of the C64 BASIC
        /// RND function where calling RND(0) returns the last generated random number.
        /// </summary>
        private double lastRandomNumber;

        /// <summary>
        /// Constants for the linear congruential generator (LCG). The LCG generates a new random number
        /// by multiplying the current seed by the Multiplier, adding the Increment, and taking the result modulo 2^32.
        /// These particular values are chosen because they give a long period and a good distribution of random numbers.
        /// </summary>
        private const uint Multiplier = 1664525;
        private const uint Increment = 1013904223;

        /// <summary>
        /// Initializes a new instance of the RndExpression class.
        /// The seed is initialized with the current time, and the first random number is generated.
        /// </summary>
        public RndExpression()
        {
            // Initialize the seed with the current time. This makes the sequence of random numbers
            // different each time the program is run, unless a specific seed is set.
            seed = (uint)DateTime.Now.Ticks;

            // Generate the first random number.
            lastRandomNumber = GenerateRandomNumber();
        }

        /// <summary>
        /// The Evaluate method generates a new random number, updates the seed, and returns the random number.
        /// The behavior depends on the argument passed to the method:
        /// - If a positive number is passed, the method does nothing and returns the last generated random number.
        /// - If a negative number is passed, the method uses the absolute value of the number as a new seed and generates a new random number.
        /// - If zero is passed, the method generates a new random number but doesn't update the seed.
        /// - If no argument is passed, the method generates a new random number and updates the seed.
        /// </summary>
        /// <param name="operands">The operands of the expression (not used in this case).</param>
        /// <returns>A random floating-point number between 0 and 1.</returns>
        public object Evaluate(params object[] operands)
        {
            if (operands.Length > 0 && operands[0] is int n)
            {
                if (n > 0)
                {
                    // If a maximum value is provided, multiply the random number by this value and convert the result to an integer.
                    return (int)(lastRandomNumber * n);
                }
                else if (n < 0)
                {
                    // Use the absolute value of n as a new seed.
                    seed = (uint)Math.Abs(n);
                    lastRandomNumber = GenerateRandomNumber();
                    return lastRandomNumber;
                }
                else
                {
                    // Generate a new random number but don't update the seed.
                    lastRandomNumber = GenerateRandomNumber();
                    return lastRandomNumber;
                }
            }
            else
            {
                // Generate a new random number.
                lastRandomNumber = GenerateRandomNumber();
                return lastRandomNumber;
            }
        }

        /// <summary>
        /// The GenerateRandomNumber method uses a linear congruential generator to generate a new random number.
        /// It first calculates a new seed by multiplying the current seed by the Multiplier, adding the Increment,
        /// and taking the result modulo 2^32. This ensures the seed is always a 32-bit integer.
        /// It then converts the seed to a floating-point number between 0 and 1 by dividing it by 2^32.
        /// </summary>
        /// <returns>A random floating-point number between 0 and 1.</returns>
        private double GenerateRandomNumber()
        {
            // Use a linear congruential generator to generate a new random number.
            seed = unchecked(seed * Multiplier + Increment);

            // Use a modulus of 2^32 to ensure the generated value is a 32-bit integer.
            seed = seed & 0xFFFFFFFF;

            // Convert the generated value to a floating-point number between 0 and 1.
            return (double)seed / 0x100000000;
        }
    }
}
