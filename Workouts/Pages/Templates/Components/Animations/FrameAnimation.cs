using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Pages.Templates.Components.Animations
{
    public class FrameAnimation : CommunityToolkit.Maui.Animations.BaseAnimation
    {
        Animation Sun(VisualElement view)
        {
            var animation = new Animation();

            animation.WithConcurrent((f) =>
            {
                view.BackgroundColor = Color.FromRgb(204, 204, 204);
            }, 0, 360, Microsoft.Maui.Easing.CubicInOut);


            return animation;
        }

        public override Task Animate(VisualElement view)
        {
            view.Animate("Sun", Sun(view), 16, Length);
            return Task.CompletedTask;
        }
    }
}
