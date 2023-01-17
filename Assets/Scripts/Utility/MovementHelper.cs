using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class MovementHelper
{
    public static IEnumerator Move(List<NodeBase> path, GameObject soldier)
    {
        Soldier Soldier = soldier.GetComponent<Soldier>();

        if (soldier.transform.CompareTag("Soldier") && Soldier.isMoving == false)
        {
            foreach (var node in path)
            {
                Soldier.isMoving = true;
                soldier.transform.DOMove(node.transform.position, 25f * Time.deltaTime);
                yield return new WaitForSeconds(0.5f);
            }
        }
        
        Soldier.isMoving = false;
    }
}