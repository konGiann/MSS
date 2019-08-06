using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	public GameObject BeingDraggedParent;
	Vector3 startPosition;
	Transform startParent;

    void Awake()
    {
        BeingDraggedParent = GameObject.Find("ParentOnDrag");
    }


	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;

		startPosition = transform.position;

		startParent = transform.parent;

		GetComponent <CanvasGroup>().blocksRaycasts = false;
	}


	public void OnDrag (PointerEventData eventData)
	{
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        itemBeingDragged.transform.SetParent (BeingDraggedParent.transform);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent <CanvasGroup> ().blocksRaycasts = true;

		if(transform.parent == startParent)
		{
			transform.position = startPosition;	
		}

		if(transform.parent == BeingDraggedParent.transform)
		{
			transform.position = startPosition;
		}
	}
}
