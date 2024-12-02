using AppiumFramework.Core.Helpers;
using AppiumTestProject.Core.Base.Elements;
using AppiumTestProject.Core.Driver;

namespace AppiumTestProject.Core.Base.Screens
{
    public abstract class BaseScreen
    {
        protected abstract string Name { get; }
        protected abstract string AppName { get; }
        protected abstract BaseElement UniqueElement { get; }
        public virtual string FullScreenName => $"{Name}{(AppName is null || AppName == string.Empty ? string.Empty : $"({AppName})")})";

        public bool IsScreenDisplayed()
        {
            return UniqueElement.IsElementDisplayed();
        }

        public bool WaitToBeLoaded()
        {
            return AppiumDriver.Wait().Until(_ => IsScreenDisplayed());
        }

        public void AssertIsLoaded()
        {
            LogHelper.Step("Проверка, что страница отображается на экране",
                () => AssertHelper.AssertIsTrue(WaitToBeLoaded(), $"На экране отображается страница: '{Name}'"));

        }
    }
}