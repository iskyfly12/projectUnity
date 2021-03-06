using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    [HideInInspector]
    public List<Grid> gridList = new List<Grid>();

    public bool finishGeneration { get; private set; }

    public void Generate(int gridCol, int gridRow, float offset, float height, float delay, Action<Grid> onGridClickAction)
    {
        finishGeneration = false;
        StartCoroutine(Spawn(gridCol, gridRow, offset, height, delay, onGridClickAction));
    }

    IEnumerator Spawn(int gridCol, int gridRow, float offset, float height, float delay, Action<Grid> onGridClickAction)
    {
        Grid g = ObjectPoolerSystem.SpawFromPool("Grid", Vector3.zero).GetComponent<Grid>();
        g.Init(0, 0, onGridClickAction);
        gridList.Add(g);

        yield return new WaitForSeconds(delay);

        Vector3 initPosition;
        for (int j = 1; j <= gridCol - 1; j++)
        {
            initPosition = new Vector3(0, height, j * offset);
            g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
            g.Init(0, j, onGridClickAction);
            gridList.Add(g);

            initPosition = new Vector3(0, height, (-1) * (j * offset));
            g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
            g.Init(0, (-1) * j, onGridClickAction);
            gridList.Add(g);
        }

        int num = gridRow - 1;
        for (int i = 1; i <= gridCol - 1; i++)
        {
            yield return new WaitForSeconds(delay);

            if (i % 2 == 0)
            {
                initPosition = new Vector3(i * offset, height, 0);
                g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                g.Init(i, 0, onGridClickAction);
                gridList.Add(g);

                initPosition = new Vector3((-1) * (i * offset), height, 0);
                g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                g.Init((-1) * i, 0, onGridClickAction);
                gridList.Add(g);

                for (int j = 1; j <= num; j++)
                {
                    initPosition = new Vector3(i * offset, height, j * offset);
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init(i, j, onGridClickAction);
                    gridList.Add(g);

                    initPosition = new Vector3(i * offset, height, (-1) * (j * offset));
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init(i, (-1) * j, onGridClickAction);
                    gridList.Add(g);

                    initPosition = new Vector3((-1) * (i * offset), height, j * offset);
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init((-1) * i, j, onGridClickAction);
                    gridList.Add(g);

                    initPosition = new Vector3((-1) * (i * offset), height, (-1) * (j * offset));
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init((-1) * i, (-1) * j, onGridClickAction);
                    gridList.Add(g);
                }
            }
            else
            {
                for (int j = 0; j < num; j++)
                {
                    initPosition = new Vector3(i * offset, height, (j + .5f) * offset);
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init(i, j + 1, onGridClickAction);
                    gridList.Add(g);

                    initPosition = new Vector3(i * offset, height, (-1) * ((j + .5f) * offset));
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init(i, (-1) * j - 1, onGridClickAction);
                    gridList.Add(g);

                    initPosition = new Vector3((-1) * (i * offset), height, (j + .5f) * offset);
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init((-1) * i, j + 1, onGridClickAction);
                    gridList.Add(g);

                    initPosition = new Vector3((-1) * (i * offset), height, (-1) * ((j + .5f) * offset));
                    g = ObjectPoolerSystem.SpawFromPool("Grid", initPosition).GetComponent<Grid>();
                    g.Init((-1) * i, (-1) * j - 1, onGridClickAction);
                    gridList.Add(g);
                }
                num--;
            }
        }

        yield return new WaitForSeconds(delay);
        finishGeneration = true;
    }

    public void Clear()
    {
        if (gridList == null)
            return;

        if (gridList.Count > 0)
        {
            foreach (Grid g in gridList)
                if (g)
                    g.gameObject.SetActive(false);

            gridList.Clear();
        }
    }

}
