using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit.Abstractions;
using System.Drawing.Text;

namespace CreditCards.UITests
{
    [Trait("Category", "Applicatons")]
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:5258/";
        private const string ApplyUrl = "http://localhost:5258/Apply";
        private const string PageTitle = "Credit Card Application - Credit Cards";

        private readonly ITestOutputHelper output;

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output;
        }

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
                DemoHelper.Pause();

                //explicity clicking the next buttom on the carousel, can ignore the three lines and use the WebdriverWait to automate the process,
                //however will take time as it is hardcoded to slide through the carousel slides after 10 secs.
                IWebElement carouselnext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselnext.Click();
                DemoHelper.Pause(1000); //allowing the carousel to load the next page

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement applyLink =
                    wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

             //   IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
               // applyLink.Click();
                
                DemoHelper.Pause(5000);

            }

        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplicaion_Prebuilt_Conditons()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause(1000);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));
                //explicitly wait until the element is clickable
                IWebElement applyLink =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

            }
        }


        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                //Implicit wait code
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(38);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                //IWebElement carouselnext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                //carouselnext.Click();
                //DemoHelper.Pause(1000);
                //carouselnext.Click();       //go to the last silde
                //DemoHelper.Pause(1000);   


                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding Element");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                IWebElement applyLink =
                    wait.Until((d) => d.FindElement(By.ClassName("customer-service-apply-now")));


                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding Element");
                //IWebElement applyLink = driver.FindElement(By.ClassName("customer-service-apply-now"));


                //implicit waits, we can't use complex logic like can with explicit wait.
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Element Found Displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking Element");
                applyLink.Click(); 
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

// working with waits:
// 1st way-using demohelper class to cause that thread to sleep for required period of time. Could use but causes the automation tests to break easily.
// 2nd way-Explicit waits using WebDriver waits which uses timespan but has a conditional logic
// 3rd way-Implicit waits using IWebdriver property 