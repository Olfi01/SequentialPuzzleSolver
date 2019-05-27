using System;
using System.Collections.Generic;
using System.Linq;

namespace SequentialPuzzleSolver
{
    /// <summary>
    /// A solver for the puzzle of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">The class of the puzzle. Must implemente <see cref="IPuzzle"/>. In this class you have to model the puzzle body and state.</typeparam>
    public class PuzzleSolver<T> where T : IPuzzle
    {
        /// <summary>
        /// The collection of algorithms used to solve a puzzle
        /// </summary>
        public List<SolvingAlgorithm<T>> Algorithms { get; } = new List<SolvingAlgorithm<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PuzzleSolver{T}"/> class
        /// </summary>
        /// <param name="solvingAlgorithms">Initial algorithms to be added to the <see cref="Algorithms"/> list</param>
        public PuzzleSolver(params SolvingAlgorithm<T>[] solvingAlgorithms)
        {
            Algorithms.AddRange(solvingAlgorithms);
        }

        /// <summary>
        /// Solves a puzzle.
        /// </summary>
        /// <param name="puzzle">The puzzle to solve</param>
        /// <returns>The solved puzzle</returns>
        /// <exception cref="FaultyPuzzleException">Is thrown when a puzzle already contains mistakes when entered</exception>
        /// <exception cref="PuzzleNotSolvedException">Is thrown when a puzzle cannot be solved with the current list of algorithms</exception>
        /// <exception cref="FaultyAlgorithmException{T}">Is thrown when an algorithm produced a mistake within the puzzle</exception>
        public T Solve(T puzzle)
        {
            if (puzzle.ContainsMistakes()) throw new FaultyPuzzleException("Puzzle already contains mistakes");
            while (!puzzle.IsSolved())
            {
                if (!Algorithms.Any(x => x.IsApplicable(puzzle))) throw new PuzzleNotSolvedException();
                SolvingAlgorithm<T> solvingAlgorithm = Algorithms.First(x => x.IsApplicable(puzzle));
                solvingAlgorithm.Apply(puzzle);
                if (puzzle.ContainsMistakes()) throw new FaultyAlgorithmException<T>(solvingAlgorithm);
            }
            return puzzle;
        }

        /// <summary>
        /// Tries to solve a puzzle. Returns true if it was successfully solved
        /// </summary>
        /// <param name="puzzle">The puzzle to solve</param>
        /// <returns>true if it was successfully solved</returns>
        /// <exception cref="FaultyPuzzleException">Is thrown when a puzzle already contains mistakes when entered</exception>
        /// <exception cref="FaultyAlgorithmException{T}">Is thrown when an algorithm produced a mistake within the puzzle</exception>
        public bool TrySolve(T puzzle)
        {
            try
            {
                Solve(puzzle);
                return true;
            }
            catch (PuzzleNotSolvedException)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// An exception that indicates that the algorithms supplied weren't enough to solve the puzzle
    /// </summary>
    public class PuzzleNotSolvedException : Exception { }

    /// <summary>
    /// An exception that indicates that a certain algorithm produced a mistake within the puzzle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FaultyAlgorithmException<T> : Exception where T : IPuzzle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaultyAlgorithmException{T}"/> class
        /// </summary>
        /// <param name="algorithm">The algorithm that produced a mistake</param>
        public FaultyAlgorithmException(SolvingAlgorithm<T> algorithm) : base($"Algorithm {algorithm.Name} produced a mistake in the puzzle") { }
    }

    /// <summary>
    /// An exception that indicates that a certain puzzle already contained mistakes when it was first supplied
    /// </summary>
    public class FaultyPuzzleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaultyPuzzleException"/> classs
        /// </summary>
        /// <param name="message">A message containing additional information</param>
        public FaultyPuzzleException(string message) : base(message) { }
    }
}
