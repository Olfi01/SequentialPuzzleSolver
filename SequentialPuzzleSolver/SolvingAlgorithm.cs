using System;

namespace SequentialPuzzleSolver
{
    /// <summary>
    /// An algorithm to apply on a puzzle of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">The type of puzzle to apply this algorithm on</typeparam>
    public class SolvingAlgorithm<T> where T : IPuzzle
    {
        /// <summary>
        /// A method delegate that applies an algorithm on a puzzle
        /// </summary>
        /// <param name="puzzle"></param>
        public delegate void SolvingDelegate(T puzzle);
        private SolvingDelegate Delegate { get; }
        private Predicate<T> Predicate { get; }
        /// <summary>
        /// The name of the algorithm, user-defined
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolvingAlgorithm{T}"/> class
        /// </summary>
        /// <param name="name">The name to use for this algorithm</param>
        /// <param name="execute">The delegate that should be executed to apply this algorithm</param>
        /// <param name="isApplicable">A predicate to indicate whether or not this algorithm is applicable on a puzzle (that is, whether or not it would change the puzzle state at all)</param>
        public SolvingAlgorithm(string name, SolvingDelegate execute, Predicate<T> isApplicable)
        {
            Name = name;
            Delegate = execute;
            Predicate = isApplicable;
        }

        /// <summary>
        /// Applies the algorithm to a puzzle
        /// </summary>
        /// <param name="puzzle">The puzzle to apply the algorithm to</param>
        public void Apply(T puzzle) => Delegate.Invoke(puzzle);

        /// <summary>
        /// Returns true if the algorithm is applicable on a certain puzzle (that is, whether or not it would change the puzzle state at all)
        /// </summary>
        /// <param name="puzzle">The puzzle to apply the algorithm on</param>
        /// <returns>true if the algorithm is applicable on a certain puzzle</returns>
        public bool IsApplicable(T puzzle) => Predicate.Invoke(puzzle);
    }
}
