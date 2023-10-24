using UnityEngine;


public abstract class Window : MonoBehaviour, IUIView
{
	public virtual void Show(UIViewArguments arguments)
	{
		gameObject.SetActive(true);
	}

	public virtual void Hide(UIViewArguments arguments)
	{
		gameObject.SetActive(false);
	}

	public abstract void Reset();
}