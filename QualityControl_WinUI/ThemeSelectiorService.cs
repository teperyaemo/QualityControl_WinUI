﻿using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityControl_WinUI
{
    public class ThemeSelectiorService : IThemeSelectorService
    {
        public ElementTheme GetTheme()
        {
            if (App.Window?.Content is FrameworkElement frameworkElement)
            {
                return frameworkElement.ActualTheme;
            }
            return ElementTheme.Default;
        }

        public void SetTheme(ElementTheme theme)
        {
            if(App.Window?.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme;
            }
        }
    }
}
