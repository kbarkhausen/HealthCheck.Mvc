using RazorEngine;
using RazorEngine.Templating; // For extension methods.
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using HealthCheck.Mvc.Models;

namespace HealthCheck.Mvc.Controllers
{
    public class HealthController : Controller
    {
        [Route("health")]
        public string Index()
        {
            var result = string.Empty;

            try
            {
              
                var tests = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(HealthTest).IsAssignableFrom(p))
                .Where(x => !x.IsInterface)
                .Where(x => !x.IsAbstract);

                var model = new IndexPageModel();

                string loc = Assembly.GetAssembly(typeof(HealthTest)).Location;
                FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(loc);

                model.PageTitle = versionInfo.ProductName + " " + versionInfo.FileVersion;
                model.Footer = versionInfo.ProductName + " &copy; " + DateTime.Now.Year;

                foreach (var item in tests)
                {
                    var watch = Stopwatch.StartNew();

                    var displayTestResult = new DisplayTestResult();

                    HealthTest instance = null;

                    // creating an instance of the test
                    try
                    {
                        instance = Activator.CreateInstance(item) as HealthTest;
                        displayTestResult.TestName = instance.TestName;
                    }
                    catch (Exception ex)
                    {
                        displayTestResult.Successful = false;
                        displayTestResult.ErrorMessage = ex.Message;
                    }

                    // running the test
                    try
                    {
                        var testResult = instance.Run();

                        displayTestResult.Successful = testResult.Successful;
                        displayTestResult.ErrorMessage = testResult.ErrorMessage;
                    }
                    catch (Exception ex)
                    {
                        displayTestResult.Successful = false;
                        displayTestResult.ErrorMessage = ex.Message;
                    }

                    watch.Stop();

                    var elapsedMs = watch.ElapsedMilliseconds;
                    displayTestResult.ExecutionTime = elapsedMs + " ms";

                    model.TestResults.Add(displayTestResult);
                }

                var template = ReadEmbeddedResource("HealthCheck.Mvc.Views.Health.Index.html");
                var templateKey = Guid.NewGuid().ToString();
                result = Engine.Razor.RunCompile(template, templateKey, null, model);
            }
            catch (Exception ex2)
            {
                result = "An error occurred: " + ex2.Message;
            }

            return result;
        }

        private string ReadEmbeddedResource(string resourceName)
        {
            string result = null;
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                //throw;
            }
            return result;
        }
    }
}
