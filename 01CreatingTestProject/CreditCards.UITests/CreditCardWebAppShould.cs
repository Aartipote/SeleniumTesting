using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:5258/";
        private const string AboutUrl = "http://localhost:5258/Home/About";
        private const string HomeTitle = "Home Page - Credit Cards";


        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            // ChromeDriver is called in the using statement to clean-up any unmanaged resources.
            // Type IWebDriver is used to have a level of abstraction. Here for the chrome browser.
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                //string pageTitle = driver.Title;

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }
        }

        [Fact]
        [Trait("Category", "Smoke")] 
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement generationTokenELement = driver.FindElement(By.Id("GenerationToken"));

                string initialToken = generationTokenELement.Text;

                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                string reloadToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
                Assert.NotEqual(initialToken, reloadToken);
            }

        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

            }

        }

    }

}
//< p id = "GenerationToken" > a8bbf814 - eb11 - 4185 - bec7 - 981873e036f3 </ p >

// F11 on the test class causes the browser to reload