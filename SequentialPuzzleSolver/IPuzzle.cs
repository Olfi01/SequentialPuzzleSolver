using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequentialPuzzleSolver
{
    /// <summary>
    /// Interface containing the methods necessary to use this library to solve it
    /// </summary>
    public interface IPuzzle
    {
        /// <summary>
        /// Should return true if the puzzle is solved in the current state
        /// </summary>
        /// <returns>true if the puzzle is solved in the current state</returns>
        bool IsSolved();
        /// <summary>
        /// Should return true if the puzzle contains mistakes (not just missing information)
        /// </summary>
        /// <returns>true if the puzzle contains mistakes</returns>
        bool ContainsMistakes();
    }
}
