using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    private int clickCount;


    [SerializeField] private SelectManager SelectManager;

    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
    }

    private SquareNode targetNode;

    // Update is called once per frame
    void Update()
    {
        if (InputHelper.LeftClickDown)
        {
            if (clickCount == 0)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider != null && hit.transform.CompareTag("SquareNode"))
                {
                    clickCount++;
                }
            }
        }

        if (InputHelper.RightClickDown && SelectManager.currentSelectedObject != null &&
            SelectManager.currentSelectedObject.GetComponent<Soldier>().isMoving != true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.transform.CompareTag("SquareNode"))
            {
                targetNode = hit.transform.GetComponent<SquareNode>();
                SelectManager.currentSelectedObject.GetComponent<Soldier>().Move(targetNode);
            }
        }
    }
}