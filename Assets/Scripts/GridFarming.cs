using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridFarming : MonoBehaviour
{
    public bool instantiate; 
    public int width;
    public int height;
    public int[,] grid;
    public float cellSize;
    public GameObject gameobject;
    public GameObject[,] gameObjects;
    public GameObject[] farm;
    public Vector3 originPos;
    private void Start()
    {
       
        int i = 0;
        if (instantiate)
        {
            originPos = this.transform.position;

            grid = new int[width, height];
            gameObjects = new GameObject[width, height];
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {

                    GameObject b = Instantiate(gameobject);


                    b.transform.position = getPosition(x, y);
                    plant comp = b.GetComponent<plant>();
                    comp.sname = x + "" + y;
                    comp.x = x;
                    comp.y = y;
                    gameObjects[x, y] = b;
                    DontDestroyOnLoad(this);
                    b.tag = "Dynamic2";
                    b.GetComponent<objectInfo>().sceneIndex = 2;
                }

            }
        }
        else 
        {
            originPos = farm[0].transform.position;
            grid = new int[width, height];
            gameObjects = new GameObject[width, height];
           
            for (int x = 0; x < grid.GetLength(0); x++)
            {
               
                for (int y = 0; y < grid.GetLength(1); y++)
                {

                    GameObject b = farm[i];

                     
                  
                    plant comp = b.GetComponent<plant>();
                    comp.sname = x + "" + y;
                    comp.x = x;
                    comp.y = y;
                    gameObjects[x, y] = b;
                    DontDestroyOnLoad(this);
                    
                   
                    i++;
                }

            }
            if (dataStatic.Instance.plows  == null)
            {
                dataStatic.Instance.plows = new int[width, height];
            }
            else
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {

                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        SetValueFromSave(x, y, dataStatic.Instance.plows[x,y]);
                    }
                }
            }
        }

   
    }

    private Vector3 getPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPos;
    }

    private void GetYX(Vector3 worldPosition, out int x, out int y)
    {
        y = Mathf.FloorToInt((worldPosition-originPos).x );
        x = Mathf.FloorToInt((worldPosition- originPos).y );
    }
    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height){
            grid[x, y] = value;
            dataStatic.Instance.plows[x, y] = value;
            plant comp =  gameObjects[x, y].GetComponent<plant>();
            comp.state = value;
            if(value == 2)
            {
                comp.plantSeed(x, y);
            }
            if(value == 1)
            {
                comp.plow(x, y);
            }
        }
    }
    public void SetValueFromSave(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            grid[x, y] = value;
            dataStatic.Instance.plows[x, y] = value;
            plant comp = gameObjects[x, y].GetComponent<plant>();
            comp.state = value;
            if (value == 2)
            {
                if (dataStatic.Instance.plantNames[x, y] != null && dataStatic.Instance.plantNames[x, y] != "")
                {
                    comp.plow(x, y);
                    comp.plantSeed(x, y, dataStatic.Instance.plantNames[x, y]);
                }
            }
            if (value == 1)
            {
                comp.plow(x, y);
            }
        }
    }
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetYX(worldPosition, out x, out y);
        SetValue(x, y, value);
    }
    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return grid[x, y];
        }
        else
        {
            return 0;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetYX(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
