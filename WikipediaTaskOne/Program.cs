using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WikipediaTaskOne;
using System.Drawing;
using OpenQA.Selenium;


/*********************************************************************************************************************************************
  
README:

- This project is build as a console application.
- Google Chrome is set as a default browser for runnig this project. 
- If you'd like to use Edge instead, simply uncomment "Edge option" section and comment "Chrome option" section.
- Run the application and follow the instructions.
- Feel free to use following URL as an example:
    https://en.wikipedia.org/wiki/Classical_antiquity
    https://en.wikipedia.org/wiki/Dolphin
    https://en.wikipedia.org/wiki/Philosophy

*********************************************************************************************************************************************/


Console.WriteLine("Paste any Wikipedia URL below here and I'll tell you how many redirects it takes to get to Philosophy page.");
Console.Write("Your URL: ");
string url = Console.ReadLine().Trim();


// Chrome option:
var driver = new ChromeDriver();
//// Edge option:
//var driver = new EdgeDriver();


WikiPage wiki = new WikiPage(driver);
driver.Manage().Window.Size = new Size(1920, 1200);
driver.Navigate().GoToUrl(url);


int counter = 0;

while (!wiki.IsThisPhilosophyPage())
{
    if (wiki.IsPhilosophyLinkOnThePage())
    {
        wiki.ClickPhilosophyLink();
    }
    else
    {
        wiki.ClickFirstLinkToRedirect();        
    }
    counter++;
}
driver.Quit();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Number of redirects: {counter}");
Console.ResetColor();


