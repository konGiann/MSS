using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler 
{
	public GameObject item 
	{
		get
		{
			if(transform.childCount > 0 )
			{
				return transform.GetChild (0).gameObject;
			}
			return null;
		}

	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if(transform.GetChild (0).gameObject.GetComponent <EmptySkill>())
		{
			DragHandler.itemBeingDragged.transform.SetParent (transform);
			DragHandler.itemBeingDragged.transform.SetAsFirstSibling ();
		}	
	}

	#endregion
}
