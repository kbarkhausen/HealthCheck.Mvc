# HealthCheck.Mvc
A NuGet package that allows you to easily test the current health of your MVC application.

To add a new test, simply add a class that implements `IHealthTest`.

```
public interface IHealthTest
{
   TestResult Run();
}
```

Any class that implements this interface will be instantiated and the method `Run()` will be executed anytime that someone accesses the URL: `/health` on your website.

When any test fails, the health page will prominantly display the message: 

"One or more errors occurred during testing!"

You can easily set a service to monitor the URL `/health` and look for the phrase "One or more errors occurred during testing!". The monitoring service can then alert you when this occurs.
