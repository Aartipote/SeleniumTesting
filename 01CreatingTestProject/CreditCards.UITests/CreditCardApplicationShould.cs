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
                DemoHelper.Pause(5000);

                IWebElement carouselnext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselnext.Click();
                DemoHelper.Pause(1000); //allowing the carousel to load the next page

                IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                applyLink.Click();
                DemoHelper.Pause(5000);

            }

        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(1000);

                IWebElement carouselnext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselnext.Click();
                DemoHelper.Pause(1000);
                carouselnext.Click();       //go to the last silde
                DemoHelper.Pause(1000);

                IWebElement applybutton = driver.FindElement(By.ClassName("customer-service-apply-now"));
                applybutton.Click();
                DemoHelper.Pause();

                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(PageTitle, driver.Title);

            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(1000);

                IWebElement randomgreetinglink = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomgreetinglink.Click();
                DemoHelper.Pause();

                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(PageTitle, driver.Title);

            }
        }

        [Fact]

        public void BeInitiatedFromHomePage_RandomGreeting_UsingXPath()
        {
            using (IWebDriver driver = new ChromeDriver())
            { 
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(1000);

                //could use absolute XPath by copying Xpath using inspect(Developer Tools)
                //relative XPath was grabbed by using xpather.com
                //relative XPath query is less brittle than absolute
                IWebElement randomgreetinglink = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomgreetinglink.Click();
                DemoHelper.Pause();

                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(PageTitle, driver.Title);

            }
        }
    }
}

//< a class= "btn btn-default" href = "/Apply" > Easy: Apply Now!</a>
//name="ApplyLowRate"
//Credit Card Application - Credit CardsCredit Card Application - Credit Cards