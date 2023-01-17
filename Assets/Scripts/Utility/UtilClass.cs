using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UtilClass
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vector.z = 0f;
        return vector;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static GameObject GetComponentFromRay(ref bool isUI)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        isUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (hit.transform != null && !isUI)
        {
            if (hit.transform.GetComponent<ISelectable>() != null)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    public static List<Vector2> GetUnitPositions(ProductionTypeEnum productionTypeEnum)
    {
        List<Vector2> posList = new List<Vector2>();
        if (productionTypeEnum == ProductionTypeEnum.Barrack)
        {
            var mousePos = GetMouseWorldPosition();
            var xMaxLimit = mousePos.x + 1.5f;
            var xMinLimit = mousePos.x - 1.5f;

            var yMaxLimit = mousePos.y + 1.5f;
            var yMinLimit = mousePos.y - 1.5f;

            for (float i = xMinLimit; i <= xMaxLimit; i++)
            {
                for (float j = yMinLimit; j <= yMaxLimit; j++)
                {
                    var currentCheck = new Vector2(i, j);
                    posList.Add(currentCheck);
                }
            }
        }
        else if (productionTypeEnum == ProductionTypeEnum.PowerPlant)
        {
            var mousePos = GetMouseWorldPosition();
            var xMaxLimit = mousePos.x + 1f;
            var xMinLimit = mousePos.x - 1f;

            var yMaxLimit = mousePos.y + .5f;
            var yMinLimit = mousePos.y - .5f;

            for (float i = xMinLimit; i <= xMaxLimit; i++)
            {
                for (float j = yMinLimit; j <= yMaxLimit; j++)
                {
                    var currentCheck = new Vector2(i, j);
                    posList.Add(currentCheck);
                }
            }
        }

        return posList;
    }


    public static void SetUnwalkableNodes(List<NodeBase> gridNodesGoList)
    {
        foreach (var nodeBase in gridNodesGoList)
        {
            nodeBase.Walkable = false;
        }
    }

    public static Vector2 SetObjCenterPosition(List<NodeBase> gridNodesGoList, ProductionTypeEnum productionType)
    {
        Vector2 firstPoint = default, secPoint = default, centeredPosition = default;

        if (productionType == ProductionTypeEnum.Barrack)
        {
            for (int i = 0; i < gridNodesGoList.Count; i++)
            {
                if (i == 5)
                {
                    firstPoint = gridNodesGoList[i].Coords.Pos;
                }

                if (i == 10)
                {
                    secPoint = gridNodesGoList[i].Coords.Pos;
                }

                gridNodesGoList[i].Walkable = false;
            }

            centeredPosition = (firstPoint + secPoint) / 2;
        }
        else if (productionType == ProductionTypeEnum.Soldier)
        {
            centeredPosition = gridNodesGoList[0].Coords.Pos / 2;
        }
        else if (productionType == ProductionTypeEnum.PowerPlant)
        {
            for (int i = 0; i < gridNodesGoList.Count; i++)
            {
                if (i == 2)
                {
                    firstPoint = gridNodesGoList[i].Coords.Pos;
                }

                if (i == 3)
                {
                    secPoint = gridNodesGoList[i].Coords.Pos;
                }

                gridNodesGoList[i].Walkable = false;
            }

            centeredPosition = (firstPoint + secPoint) / 2;
        }

        return centeredPosition;
    }

    public static bool UnitSettlePositionCheck(
        GridManager Grid, ProductionTypeEnum productionTypeEnum)
    {
        List<Vector2> gridNodesPosList = new List<Vector2>();
        List<NodeBase> gridNodesGOList = new List<NodeBase>();

        foreach (var barrackPos in UtilClass.GetUnitPositions(productionTypeEnum))
        {
            int roundedX = Mathf.RoundToInt(barrackPos.x);
            int roundedY = Mathf.RoundToInt(barrackPos.y);

            if (Grid.Tiles.Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).Any())
            {
                gridNodesPosList.Add(Grid.Tiles
                    .Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).First().Key);
                gridNodesGOList.Add(Grid.Tiles
                    .Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).First().Value);
            }
        }

        if (productionTypeEnum == ProductionTypeEnum.Barrack)
        {
            if (gridNodesPosList.Count == 16) return true;
        }
        else if (productionTypeEnum == ProductionTypeEnum.Soldier)
        {
            if (gridNodesPosList.Count == 1) return true;
        }
        else if (productionTypeEnum == ProductionTypeEnum.PowerPlant)
        {
            if (gridNodesPosList.Count == 6) return true;
        }

        return false;
    }

    public static bool UnitSettlePositionCheckWithTarget(
        GridManager Grid, ProductionTypeEnum productionTypeEnum, Vector2 targetNodePosition)
    {
        List<Vector2> gridNodesPosList = new List<Vector2>();
        List<NodeBase> gridNodesGOList = new List<NodeBase>();


        int roundedX = Mathf.RoundToInt(targetNodePosition.x);
        int roundedY = Mathf.RoundToInt(targetNodePosition.y);

        if (Grid.Tiles.Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).Any())
        {
            gridNodesPosList.Add(Grid.Tiles
                .Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).First().Key);
            gridNodesGOList.Add(Grid.Tiles
                .Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).First().Value);
        }

        if (productionTypeEnum == ProductionTypeEnum.Barrack)
        {
            if (gridNodesPosList.Count == 16) return true;
        }
        else if (productionTypeEnum == ProductionTypeEnum.Soldier)
        {
            if (gridNodesPosList.Count == 1) return true;
        }

        return false;
    }


    public static List<NodeBase> GetUnitSettleNode(
        GridManager Grid, ProductionTypeEnum productionTypeEnum)
    {
        List<Vector2> gridNodesPosList = new List<Vector2>();
        List<NodeBase> gridNodesGOList = new List<NodeBase>();

        foreach (var barrackPos in UtilClass.GetUnitPositions(productionTypeEnum))
        {
            int roundedX = Mathf.RoundToInt(barrackPos.x);
            int roundedY = Mathf.RoundToInt(barrackPos.y);

            if (Grid.Tiles.Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).Any())
            {
                gridNodesPosList.Add(Grid.Tiles
                    .Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).First().Key);
                gridNodesGOList.Add(Grid.Tiles
                    .Where(x => x.Key == new Vector2(roundedX, roundedY) && x.Value.Walkable == true).First().Value);
            }
        }

        if (productionTypeEnum == ProductionTypeEnum.Barrack)
        {
            if (gridNodesPosList.Count == 16) return gridNodesGOList;
        }
        else if (productionTypeEnum == ProductionTypeEnum.Soldier)
        {
            if (gridNodesPosList.Count == 1) return gridNodesGOList;
        }
        else if (productionTypeEnum == ProductionTypeEnum.PowerPlant)
        {
            if (gridNodesPosList.Count == 6) return gridNodesGOList;
        }

        return null;
    }
}