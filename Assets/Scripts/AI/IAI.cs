using System.Collections.Generic;
using UnityEngine;

public interface IAI<in T, out R>
{
    /// <summary>
    /// Call Function to generate new Move for the AI
    /// </summary>
    /// <returns>new Move</returns>
    R NewAIMove();

    /// <summary>
    /// evaluate the current state of the board. Calculates the values of all pieces on the board
    /// </summary>
    /// <returns>positiv = black favor, negative = white favor</returns>
    int EvalBoard();

    /// <summary>
    /// Method evaluates all possible Moves and the enemy Counter Moves to a certain depth. Sets best Move at the end.
    /// </summary>
    /// <param name="depth">Calculation depth, how many recursion calls</param>
    /// <param name="alpha">Alpha-Beta-Pruning, to check if path can be ignored</param>
    /// <param name="beta">Alpha-Beta-Pruning, to check if path can be ignored</param>
    /// <param name="isMaximizing">here: isMaximizing = AI</param>
    /// <returns></returns>
    int Minimax(int depth, int alpha, int beta, bool isMaximizing);

    /// <summary>
    /// Perform a fake Move, so that the AI can anticipate future Moves
    /// </summary>
    /// <param name="move"></param>
    void PerformFakeMove(T move);

    /// <summary>
    /// Reset Board State to original State
    /// </summary>
    void UndoFakeMove();
}
