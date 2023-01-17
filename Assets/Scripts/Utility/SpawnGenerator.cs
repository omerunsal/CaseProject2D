using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnGenerator : MonoBehaviour
{
    [SerializeField] private GridManager Grid;

    private Vector2 mousePosition;


    private Building spawned;
    private bool isSpawned;

    private int clickCount;


    private BuildingBuilder _buildingBuilder;


    private void Awake()
    {
        _buildingBuilder = GetComponent<BuildingBuilder>();
    }

    void Start()
    {
        isSpawned = false;
        clickCount = 0;
    }

    private void OnEnable()
    {
        GameEvents.SpawnBuilding += SpawnBuilding;
    }

    private void OnDisable()
    {
        GameEvents.SpawnBuilding -= SpawnBuilding;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isSpawned == true && spawned != null)
        {
            if (spawned != null && spawned.isDropped == false && this.spawned.GetComponent<Barrack>())
            {
                this.spawned.transform.position = mousePosition;
                this.spawned.transform.GetComponentInChildren<SpriteRenderer>().color =
                    UtilClass.UnitSettlePositionCheck(Grid, ProductionTypeEnum.Barrack)
                        ? Color.green
                        : Color.red;
            }

            if (spawned != null && spawned.isDropped == false && this.spawned.GetComponent<PowerPlant>())
            {
                this.spawned.transform.position = mousePosition;
                this.spawned.transform.GetComponentInChildren<SpriteRenderer>().color =
                    UtilClass.UnitSettlePositionCheck(Grid, ProductionTypeEnum.PowerPlant)
                        ? Color.green
                        : Color.red;
            }
        }

        if (InputHelper.LeftClickDown && clickCount == 1 && spawned != null && (this.spawned.GetComponent<Barrack>() &&
                UtilClass.UnitSettlePositionCheck(Grid, ProductionTypeEnum.Barrack))
           )
        {
            List<NodeBase> settlePositions = UtilClass.GetUnitSettleNode(Grid, ProductionTypeEnum.Barrack);

            UtilClass.SetUnwalkableNodes(settlePositions);

            spawned.transform.position =
                UtilClass.SetObjCenterPosition(settlePositions, ProductionTypeEnum.Barrack);
            spawned.isDropped = true;
            clickCount = 0;
            spawned = null;
        }
        else if (InputHelper.LeftClickDown && clickCount == 1 && spawned != null &&
                 (this.spawned.GetComponent<PowerPlant>() &&
                  UtilClass.UnitSettlePositionCheck(Grid, ProductionTypeEnum.PowerPlant))
                )
        {
            List<NodeBase> settlePositions = UtilClass.GetUnitSettleNode(Grid, ProductionTypeEnum.PowerPlant);

            UtilClass.SetUnwalkableNodes(settlePositions);

            spawned.transform.position =
                UtilClass.SetObjCenterPosition(settlePositions, ProductionTypeEnum.PowerPlant);
            spawned.isDropped = true;
            clickCount = 0;
            spawned = null;
        }
    }

    private void SpawnBuilding(BuildingData buildingData)
    {
        if (spawned == null)
        {
            var spawnedBuilding = _buildingBuilder.BuildProduction(buildingData.BuildingPrefab, mousePosition);
            spawnedBuilding.SetData(buildingData);
            this.spawned = spawnedBuilding;

            isSpawned = true;
            clickCount++;
        }
    }
}