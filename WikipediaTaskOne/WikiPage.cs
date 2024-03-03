using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;

namespace WikipediaTaskOne
{
    internal class WikiPage
    {
        protected IWebDriver driver;
        public WikiPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement ContentPage => driver.FindElement(By.Id("mw-content-text"));

        public bool IsPhilosophyLinkOnThePage()
        {
            // Let's get '<a href>' attributes only from paragraphs
            var paragraphs = ContentPage.FindElements(By.TagName("p")).ToList();
            bool isPhilosophyLinkHere = false;

            foreach (var paragraph in paragraphs)
            {
                // pick only '<a href>' that are not a reference
                var elements = paragraph.FindElements(By.XPath(".//a[@href and not(ancestor::sup[@class='reference'])]"));

                if (elements.ToList().Count > 0)
                {
                    foreach (var attribute in elements)
                    {
                        if (attribute.GetAttribute("title") == "Philosophy")
                        {
                            isPhilosophyLinkHere = true;
                            Console.WriteLine("Philosophy found!!!");
                            break;
                        }
                    }
                }
                else
                {
                    continue;
                }

                if (isPhilosophyLinkHere)
                {
                    break;
                }
            }

            return isPhilosophyLinkHere;
        }


        public void ClickFirstLinkToRedirect()
        {
            var paragraphs = ContentPage.FindElements(By.TagName("p")).ToList();
            foreach (var paragraph in paragraphs)
            {
                var firstLink = paragraph.FindElements(By.XPath(".//a[@href and not(ancestor::sup[@class='reference'])]"));
                if (firstLink.Count != 0)
                {
                    firstLink[0].Click();
                    break;
                }
            }
        }

        public void ClickPhilosophyLink()
        {
            var philosophy = driver.FindElements(By.CssSelector("[href*='/wiki/Philosophy']"));
            philosophy[0].Click();
            Thread.Sleep(1000);
        }

        public bool IsThisPhilosophyPage()
        {
            var header = driver.FindElement(By.ClassName("mw-page-title-main")).Text;
            if (header == "Philosophy")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
