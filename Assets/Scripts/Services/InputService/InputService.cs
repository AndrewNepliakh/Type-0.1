using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;

namespace Services.InputService
{
	public class InputService : IInputService, IInitializable
	{
		private int UILayer;

		public void Initialize()
		{
			UILayer = LayerMask.NameToLayer("UI");
		}
		
		public bool IsOverUI()
		{
			return IsPointerOverUIElement(GetEventSystemRaycastResults());
		}
		
		private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
		{
			for (int index = 0; index < eventSystemRaycastResults.Count; index++)
			{
				RaycastResult curRaysastResult = eventSystemRaycastResults[index];
				if (curRaysastResult.gameObject.layer == UILayer)
					return true;
			}
			return false;
		}
		
		static List<RaycastResult> GetEventSystemRaycastResults()
		{
			PointerEventData eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;
			List<RaycastResult> raysastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, raysastResults);
			return raysastResults;
		}
	}
}