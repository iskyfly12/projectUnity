using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType { Empty, HasPlayer, HasPossible }

public class Grid : MonoBehaviour
{
    public GridType gridType { get; private set; }
    public int row { get; private set; }
    public int col { get; private set; }
    public bool isEmpty { get { return gridType == GridType.Empty; } }

    [Header("Materials")]
    [SerializeField] private Material mouseOverMaterial;
    [SerializeField] private Material selectableMaterial;
    [SerializeField] private Material standartMaterial;

    private event Action<Grid> onGridClick;
    private event Action<Grid> onGridSelect;

    private MeshRenderer renderMaterial;

    private void Awake()
    {
        renderMaterial = GetComponent<MeshRenderer>();
    }

    public void Init(int col, int row, Action<Grid> onGridClickAction, Action<Grid> onGridSelectAction)
    {
        this.col = col;
        this.row = row;

        name = string.Format("Grid [{0}][{1}]", col, row);

        onGridClick = onGridClickAction;
        onGridSelect = onGridSelectAction;
    }

    public void Init(int col, int row)
    {
        this.col = col;
        this.row = row;

        name = string.Format("Grid [{0}][{1}]", col, row);
    }

    public void SetGrid(GridType gridType)
    {
        this.gridType = gridType;
    }

    public void PossibleMoveGrid()
    {
        if (isEmpty)
        {
            gridType = GridType.HasPossible;
            renderMaterial.material = selectableMaterial;
        }
    }

    public void Clear()
    {
        if (gridType == GridType.HasPossible)
        {
            renderMaterial.material = standartMaterial;
            gridType = GridType.Empty;
        }
    }


    private void OnMouseDown()
    {
        if (onGridSelect == null)
            return;

        if (gridType == GridType.HasPossible)
            onGridClick(this);
    }

    private void OnMouseEnter()
    {
        if(gridType != GridType.HasPlayer)
            transform.DOMoveY(.25f, .25f);

        if (gridType == GridType.HasPossible)
            renderMaterial.material = mouseOverMaterial;
    }

    private void OnMouseExit()
    {
        DOTween.Clear();
        transform.DOMoveY(0f, .1f);
        if (gridType == GridType.HasPossible)
            renderMaterial.material = selectableMaterial;
        else
            renderMaterial.material = standartMaterial;
    }

}
