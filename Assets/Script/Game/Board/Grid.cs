using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType { Empty, HasPlayer, HasItem }

public class Grid : MonoBehaviour
{
    private GridType gridType;
    public int row { get; private set; }
    public int col { get; private set; }
    public bool isEmpty { get { return gridType == GridType.Empty; } }
    public bool canMove { get; private set; }

    [Header("Materials")]
    [SerializeField] private Material mouseOverMaterial;
    [SerializeField] private Material selectableMaterial;
    [SerializeField] private Material standartMaterial;

    private event Action<Grid> onGridClick;

    private Transform gridModel;
    private MeshRenderer renderMaterial;
    public  Collectable GridItem { get; set; }

    void Awake()
    {
        renderMaterial = GetComponentInChildren<MeshRenderer>();
        gridModel = transform.GetChild(0);
    }

    public void Init(int col, int row, Action<Grid> onGridClickAction)
    {
        this.col = col;
        this.row = row;

        name = string.Format("Grid [{0}][{1}]", col, row);

        onGridClick = onGridClickAction;
    }

    #region Grid
    public void SetGrid(GridType gridType)
    {
        this.gridType = gridType;
    }

    public void SetMoveGrid(bool state)
    {
        if (gridType != GridType.HasPlayer || canMove)
        {
            canMove = state;
            renderMaterial.material = canMove ? selectableMaterial : standartMaterial;
        }
    }
    #endregion

    #region Interactions
    private void OnMouseDown()
    {
        if (onGridClick == null)
            return;

        if (gridType != GridType.HasPlayer && canMove)
        {
            onGridClick(this);
            gridModel.transform.localPosition = Vector3.zero;
            renderMaterial.material = standartMaterial;
        }
    }

    private void OnMouseEnter()
    {
        gridModel.DOComplete(this);
        if (gridType != GridType.HasPlayer)
            gridModel.DOMoveY(.25f, .25f);

        if (canMove)
            renderMaterial.material = mouseOverMaterial;
    }

    private void OnMouseExit()
    {
        gridModel.DOComplete(this);
        gridModel.DOMoveY(0f, .25f);

        if (canMove)
            renderMaterial.material = selectableMaterial;
        else
            renderMaterial.material = standartMaterial;
    }
    #endregion

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, .5f);
    }
}
