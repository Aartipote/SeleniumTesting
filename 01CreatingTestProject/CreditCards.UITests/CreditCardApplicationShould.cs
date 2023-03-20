using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    [Trait("Category", "Applicatons")]
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:5258/";
        private const string ApplyUrl = "http://localhost:5258/Apply";
        private const string PageTitle = "Credit Card Application - Credit Cards";


        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement button = driver.FindElement(By.Name("ApplyLowRate"));
                button.Click();

                DemoHelper.Pause();

                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(PageTitle, driver.Title);

            }
        }


        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(11000);

                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLink.Click();


                DemoHelper.Pause();

            }

        }
    }
}

//< a class= "btn btn-default" href = "/Apply" > Easy: Apply Now!</a>
//name="ApplyLowRate"
//Credit Card Application - Credit CardsCredit Card Application - Credit Cards