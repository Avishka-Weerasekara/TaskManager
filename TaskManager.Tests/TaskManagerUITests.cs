using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace TaskManager.Tests
{
    [TestFixture]
    public class TaskManagerUITests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            // Initialize Chrome in headless mode for CI/CD
            var options = new ChromeOptions();
            options.AddArgument("--headless");               // Run without GUI
            options.AddArgument("--no-sandbox");             // Required for GitHub Actions
            options.AddArgument("--disable-dev-shm-usage");  // Avoid shared memory issues
            options.AddArgument("--window-size=1920,1080");  // Ensure proper viewport
            _driver = new ChromeDriver(options);
        }

        [Test, Category("Selenium")]
        public void AddTask_ShouldAppearInList()
        {
            _driver.Navigate().GoToUrl("http://localhost:5024/");

            var input = _driver.FindElement(By.Id("taskName"));
            var button = _driver.FindElement(By.Id("addTaskBtn"));

            string taskName = "Selenium Test Task";
            input.SendKeys(taskName);
            button.Click();

            Thread.Sleep(1000); // Wait for task to appear

            var taskList = _driver.FindElement(By.Id("taskList"));
            Assert.IsTrue(taskList.Text.Contains(taskName), "Task was not added to the list.");
        }

        [Test, Category("Selenium")]
        public void ExistingTasks_ShouldLoadOnPage()
        {
            _driver.Navigate().GoToUrl("http://localhost:5024/");

            Thread.Sleep(1000);

            var taskList = _driver.FindElement(By.Id("taskList"));
            Assert.IsNotNull(taskList, "Task list element not found.");
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}
