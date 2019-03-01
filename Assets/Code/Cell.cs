using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Developer : Dustin Kaban
/// Date : February 27th, 2019
/// Purpose : Re-Create the Game Of Life in Unity using C#
/// </summary>

public class Cell : MonoBehaviour
{
    [NonSerialized]
    public Vector2 Position;
    [NonSerialized]
    public bool Alive = false;
    [NonSerialized]
    public string CellLocation = "";
    [NonSerialized]
    public int CellIndex = 0;
    [NonSerialized]
    public float X = 0;
    [NonSerialized]
    public float Y = 0;

    public SpriteRenderer Renderer;

    public void InitCell(Vector2 position, bool alive, string cellLocation, int cellIndex,float x, float y)
    {
        Position = position;
        Alive = alive;
        CellLocation = cellLocation;
        this.gameObject.transform.position = Position;
        this.gameObject.name = cellLocation;
        CellIndex = cellIndex;
        X = x;
        Y = y;
    }

    public void SetAlive()
    {
        Alive = true;
        Renderer.color = Color.white;
    }

    public void SetDead()
    {
        Alive = false;
        Renderer.color = Color.black;
    }

    private void Start()
    {
        StartCoroutine("CheckNeighbours");
    }

    private IEnumerator CheckNeighbours()
    {
        while (Board.Instance.Running)
        {
            yield return new WaitForSeconds(Board.Instance.TurnDuration);

            if (Board.Instance.BoardCompleted)
            {
                int neighbourAliveCount = 0;
                int neighbourCount = 0;
                for(int i=0;i<Board.Instance.CellArray.Length;i++)
                {
                    if (i != CellIndex)
                    {
                        if ((Board.Instance.CellArray[i].X == X + 1 && Board.Instance.CellArray[i].Y == Y)
                            || (Board.Instance.CellArray[i].X == X - 1 && Board.Instance.CellArray[i].Y == Y)
                            || (Board.Instance.CellArray[i].Y == Y + 1 && Board.Instance.CellArray[i].X == X)
                            || (Board.Instance.CellArray[i].Y == Y - 1 && Board.Instance.CellArray[i].X == X)
                            || (Board.Instance.CellArray[i].Y == Y + 1 && Board.Instance.CellArray[i].X == X + 1)
                            || (Board.Instance.CellArray[i].Y == Y + 1 && Board.Instance.CellArray[i].X == X - 1)
                            || (Board.Instance.CellArray[i].Y == Y - 1 && Board.Instance.CellArray[i].X == X + 1)
                            || (Board.Instance.CellArray[i].Y == Y - 1 && Board.Instance.CellArray[i].X == X - 1))
                        {
                            if (Board.Instance.CellArray[i].Alive)
                            {
                                neighbourAliveCount++;
                            }

                            neighbourCount++;

                            if (neighbourCount >= 8) i = Board.Instance.CellArray.Length;
                        }
                    }
                }
                ApplyRules(neighbourAliveCount);
            }
        }
    }

    private void ApplyRules(int aliveCount)
    {
        if(Alive)
        {
            if (aliveCount <= 1)
            {
                SetDead();
            }
            else if(aliveCount == 2 || aliveCount == 3)
            {
                SetAlive();
            }
            else if(aliveCount >= 3)
            {
                SetDead();
            }
        }
        else
        {
            if(aliveCount == 3)
            {
                SetAlive();
            }
        }
    }

    //Loop through the cell list and find neighbours
    //A Neighbour is
    //__________________________
    //1. is only 1 X away + or -
    //2. is only 1 Y away + or -
    //3. is only 1 Y away + or - && is only 1 X away + or -
    //4. is only 1 X away + or - && is only 1 Y away + or -
    //__________________________

    // Wikipedia Rules
    // Any live cell with fewer than two live neighbors dies, as if by underpopulation.
    // Any live cell with two or three live neighbors lives on to the next generation.
    // Any live cell with more than three live neighbors dies, as if by overpopulation.
    // Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.

    //** NEED TO IMPLEMENT THIS **
    // The initial pattern constitutes the seed of the system. The first generation is created by applying the above 
    // rules simultaneously to every cell in the seed; births and deaths occur simultaneously, and the discrete moment at 
    // which this happens is sometimes called a tick. Each generation is a pure function of the preceding one. The rules 
    // continue to be applied repeatedly to create further generations.
}
