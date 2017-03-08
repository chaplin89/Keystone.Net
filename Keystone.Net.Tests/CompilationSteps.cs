using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace KeystoneBindingsTests
{
    [Binding]
    class CompilationSteps
    {
        [Given(@"An instance of Keystone built for (.*) in mode (.*)")]
        public void GivenAnInstanceOfKeystoneBuiltForXInMode(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"The statements ""(.*)""")]
        public void GivenTheStatements(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I compile the statement with Keystone")]
        public void WhenICompileTheStatementWithKeystone()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result is (.*)")]
        public void ThenTheResultIs(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The last error is (.*)")]
        public void ThenTheLastErrorIsEXPR_TOKEN()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
