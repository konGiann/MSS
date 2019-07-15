using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ToolTip :  MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public string info;
	public bool hovered;

	public void OnPointerEnter (PointerEventData eventData) 
	{
		hovered = true;

	}

	public void OnPointerExit (PointerEventData eventData) 
	{
		hovered = false;
	}

	void OnGUI()
	{
		GUIStyle style = new GUIStyle ();
		Font minecraft = (Font)Resources.Load ("Fonts/Minecraft", typeof(Font));
		style.font = minecraft;
		style.normal.textColor = Color.red;

		if(hovered)
			GUI.Label (new Rect (Event.current.mousePosition.x + 20, Event.current.mousePosition.y, 100, 100), info, style);
	}
}
