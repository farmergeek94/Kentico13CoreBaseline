﻿using CMS.DocumentEngine.Types.Generic;
using PartialWidgetPage;

namespace Generic
{
    public class CustomPartialWidgetRenderingRetriever : IPartialWidgetRenderingRetriever
    {
        public ParitalWidgetRendering GetRenderingViewComponent(string ClassName, int DocumentID = 0)
        {
            /*if (ClassName.Equals(Tab.CLASS_NAME))
            {
                return new ParitalWidgetRendering()
                {
                    ViewComponentData = new { },
                    ViewComponentName = "TabComponent",
                    SetContextPriorToCall = true
                };
            }
            if (ClassName.Equals(ShareableContent.CLASS_NAME))
            {
                return new ParitalWidgetRendering()
                {
                    ViewComponentData = new { Testing = "Hello" },
                    ViewComponentName = "ShareableContentComponent",
                    SetContextPriorToCall = true
                };
            }*/

            return null;
        }
    }
}

