using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoldierEvents : MonoBehaviour
{
    [SerializeField] private GridManager GridManager;
    [SerializeField] private SelectManager SelectManager;
    [SerializeField] private ScriptableSquareGrid _scriptableGrid;

    private SoldierBuilder _builder;
    private Vector2 firstValidPosition;

    private void Awake()
    {
        _builder = GetComponent<SoldierBuilder>();
    }

    private void OnEnable()
    {
        GameEvents.SpawnSoldier += CreateSoldier;
    }

    private void OnDisable()
    {
        GameEvents.SpawnSoldier -= CreateSoldier;
    }

    void CreateSoldier(Soldier soldier)
    {
        firstValidPosition = new Vector2((SelectManager.cacheSelectedObject.transform.position.x - 1.5f),
            (SelectManager.cacheSelectedObject.transform.position.y - 2.5f)
        );

        Vector2 secValidPosition = new Vector2((SelectManager.cacheSelectedObject.transform.position.x - 1.5f),
            (SelectManager.cacheSelectedObject.transform.position.y + 2.5f)
        );
        Vector2 validPosition = firstValidPosition;

        for (int i = 0; i < _scriptableGrid._gridHeight / 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (UtilClass.UnitSettlePositionCheckWithTarget(GridManager, ProductionTypeEnum.Soldier,
                        validPosition))
                {
                    _builder.BuildProduction(soldier,
                        GridManager.Tiles.Where(x => x.Key == validPosition).First().Value.transform.position);

                    GridManager.Tiles.Where(x => x.Key == validPosition).First().Value.Walkable = false;
                    return;
                }

                validPosition = new Vector2(validPosition.x + 1, validPosition.y);
            }

            validPosition = new Vector2(firstValidPosition.x, validPosition.y - 1);
        }


        validPosition = secValidPosition;
        for (int i = 0; i < _scriptableGrid._gridHeight / 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (UtilClass.UnitSettlePositionCheckWithTarget(GridManager, ProductionTypeEnum.Soldier,
                        validPosition))
                {
                    _builder.BuildProduction(soldier,
                        GridManager.Tiles.Where(x => x.Key == validPosition).First().Value.transform.position);

                    GridManager.Tiles.Where(x => x.Key == validPosition).First().Value.Walkable = false;
                    return;
                }

                validPosition = new Vector2(validPosition.x + 1, validPosition.y);
            }

            validPosition = new Vector2(firstValidPosition.x, validPosition.y + 1);
        }
    }
}