using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIService : IUIService, IInitializable
	{
		[Inject] private DiContainer _diContainer;
		
		private Window _currentWindow;
		private Canvas _mainCanvas;

		private Dictionary<Type, IUIView> _views = new();

		public void Initialize()
		{
			var canvasGo = _diContainer.CreateEmptyGameObject("[MainCanvas]");
			var canvas = canvasGo.AddComponent<Canvas>();
			var scaler = canvasGo.AddComponent<CanvasScaler>();
			canvasGo.AddComponent<GraphicRaycaster>();

			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            
			scaler.referenceResolution = new Vector2(1920, 1080);

			_mainCanvas = canvas;
		}
		
		public T ShowWindow<T>(UIViewArguments args = null, UIViewArguments closeArgs = null)
			where T : Window
		{
			if (_currentWindow != null) _currentWindow.Hide(null);

			if (!_views.ContainsKey(typeof(T)))
			{
				var windowPrefab = Resources.Load<GameObject>($"UIPrefabs/{typeof(T)}");
				var newWindow = _diContainer.InstantiatePrefab(windowPrefab, _mainCanvas.transform).GetComponent<T>();

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