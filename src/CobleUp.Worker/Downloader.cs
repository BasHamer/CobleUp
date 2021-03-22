using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace CobleUp.Worker
{
	class Downloader
	{
		public void Download()
		{
			RemoteWebDriver driver;


			//CHROME and IE            
			ChromeOptions Options = new ChromeOptions();

			driver = new RemoteWebDriver(
			  new Uri("http:10.0.0.127:4444/wd/hub/"), Options.ToCapabilities(), TimeSpan.FromSeconds(30));
			try
			{
	 
				driver.Navigate().GoToUrl("https://www.google.com/ncr");
				IWebElement query = driver.FindElement(By.Name("q"));
				query.SendKeys("webdriver");
				query.Submit();
			}
			finally
			{
				Console.WriteLine("Video: " + "" + driver.SessionId);
				driver.Quit();
			}
		}
	}
}
