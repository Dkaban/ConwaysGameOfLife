using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Developer : Dustin Kaban
/// Date : February 27th, 2019
/// Purpose : Re-Create the Game Of Life in Unity using C#
/// </summary>

public class Board : MonoBehaviour
{
    [NonSerialized]
    public Cell[] CellArray;
    [NonSerialized]
    public bool BoardCompleted = false;
    [NonSerialized]
    public bool Running = true;
    [NonSerialized]
    public float TurnDuration = 0.1f;

    public GameObject CellPrefabObject;

    private readonly int _width = 50;
    private readonly int _height = 50;
    private readonly float _percentAlive = 0.10f;

    public static Board Instance { get; protected set; }

    private void Awake()
    {
        Instance = this;
        CellArray = new Cell[_width * _height];
    }

    private void Start()
    {
        CreateGrid();
        RandomizeAliveOrDead();
        BoardCompleted = true;
    }

    private void CreateGrid()
    {
        int cellIndex = 0;
        for(int w=0;w<_width;w++)
        {
            for(int h=0;h<_height;h++)
            {
                CreateCell(w, h, cellIndex);
                cellIndex++;
            }
        }
    }

    private void CreateCell(float w, float h, int index)
    {
        Vector2 cellPosition = new Vector2(w, h);
        GameObject cellGameObject = GameObject.Instantiate(CellPrefabObject) as GameObject;
        cellGameObject.transform.SetParent(this.transform);
        Cell cell = cellGameObject.GetComponent<Cell>();
        cell.InitCell(cellPosition, false,(w+" "+h),index,w,h);
        CellArray[index] = cell;
    }

    private void RandomizeAliveOrDead()
    {
        for(int i=0;i<CellArray.Length;i++)
        {
            if(UnityEngine.Random.value <= _percentAlive)
            {
                CellArray[i].SetAlive();
            }
            else
            {
                CellArray[i].SetDead();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.S))
        {
            Running = false;
        }

        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
