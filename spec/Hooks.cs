using TechTalk.SpecFlow;

namespace spec
{
  [Binding]
  public sealed class Hooks
  {
    // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
      // TODO: implement logic that has to run before all tests
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
      //TODO: implement logic that has to run before executing each scenario
    }

    [AfterScenario]
    public void AfterScenario()
    {
      //TODO: implement logic that has to run after executing each scenario
    }
  }
}
