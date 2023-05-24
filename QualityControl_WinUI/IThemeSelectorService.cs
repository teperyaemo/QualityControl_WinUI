
using Microsoft.UI.Xaml;

namespace QualityControl_WinUI
{
    public interface IThemeSelectorService
    {
        ElementTheme GetTheme();

        void SetTheme(ElementTheme theme);
    }
}
