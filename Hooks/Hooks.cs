using MarsOnBoardingReqnRollProject.Drivers;
using Reqnroll;

namespace Mars_Onboarding_Project.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            Driver.Initialize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Close();
        }
    }
}

