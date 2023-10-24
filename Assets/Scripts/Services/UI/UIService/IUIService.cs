public interface IUIService
{
	T ShowWindow<T>(UIViewArguments args = null, UIViewArguments closeArgs = null) where T : Window;
	void HideCurrentWindow(UIViewArguments args);
}