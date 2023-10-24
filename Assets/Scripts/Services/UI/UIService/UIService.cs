using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


	public class UIService : IUIService
	{
		private Window _currentWindow;
		private Canvas _mainCanvas;

		private Dictionary<Type, IUIView> _views = new();

		public UIService(Canvas mainCanvas) { _mainCanvas = mainCanvas; }

		public T ShowWindow<T>(UIViewArguments args = null, UIViewArguments closeArgs = null)
			where T : Window
		{
			if (_currentWindow != null) _currentWindow.Hide(null);

			if (!_views.ContainsKey(typeof(T)))
			{
				var windowPrefab = Resources.Load<GameObject>($"UIPrefabs/{typeof(T)}");
				var newWindow = Object.Instantiate(windowPrefab, _mainCanvas.transform).GetComponent<T>();

				HideCurrentWindow(closeArgs);

				_currentWindow = newWindow;
				_views.Add(typeof(T), _currentWindow);
				_currentWindow.Show(args);
			}
			else
			{
				if (_views.TryGetValue(typeof(T), out var uiView))
				{
					HideCurrentWindow(closeArgs);

					_currentWindow = uiView as Window;
					_currentWindow.Show(args);
				}
				else
				{
					throw new NullReferenceException("UIManager's pool doesn't contain view of this type");
				}
			}

			return (T)_currentWindow;
		}

		public void HideCurrentWindow(UIViewArguments args)
		{
			if (_currentWindow != null)
				_currentWindow.Hide(args);
		}
	}