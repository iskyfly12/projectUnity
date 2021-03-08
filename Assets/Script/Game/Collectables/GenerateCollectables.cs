using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCollectables : MonoBehaviour
{
    [Header("Collectables")]
    [SerializeField] private Collectable[] collectablesPrefabs;

    private GameObject[] collectablesOnBoard;

    public bool finishGeneration { get; private set; }

    public int collectablesCount { get; set; } 

    public void Generate(Grid[] grids, int numCollectables)
    {
        collectablesOnBoard = new GameObject[numCollectables];
        finishGeneration = false;
        StartCoroutine(Spawn(grids, numCollectables));
    }

    IEnumerator Spawn(Grid[] grids, int numCollectables)
    {
        for (int i = 0; i < numCollectables; i++)
        {
            bool findEmpty = false;
            while (!findEmpty)
            {
                int randomGrid = UnityEngine.Random.Range(0, grids.Length);

                if (grids[randomGrid].isEmpty)
                {
                    int randomCollectable = UnityEngine.Random.Range(0, collectablesPrefabs.Length);

                    Grid grid = grids[randomGrid];
                    GameObject obj = ObjectPoolerSystem.SpawFromPool(collectablesPrefabs[randomCollectable].GetItemTag());
                    obj.transform.SetParent(grid.transform.GetChild(0));
                    obj.transform.localPosition = Vector3.zero;
                    collectablesOnBoard[i] = obj;

                    grid.SetGrid(GridType.HasItem);

                    Collectable co = obj.GetComponent<Collectable>();
                    co.PlayEffect();
                    grid.GridItem = co;

                    findEmpty = true;
                }
            }

            yield return new WaitForSeconds(.05f);
        }

        collectablesCount = numCollectables;
        finishGeneration = true;
    }

    public void Clear()
    {
        if (collectablesOnBoard == null)
            return;

        for (int i = 0; i < collectablesOnBoard.Length; i++)
        {
            if (collectablesOnBoard[i].activeInHierarchy)
            {
                collectablesOnBoard[i].GetComponentInParent<Grid>().SetGrid(GridType.Empty);
                collectablesOnBoard[i].SetActive(false);
            }
        }

        collectablesCount = 0;
        collectablesOnBoard = new GameObject[0];
    }
}
