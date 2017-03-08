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
        public void GivenAnInstanceOfKeystoneBuiltForInMode(string p0, string p1)
        {
        }

        [Given(@"The statements ""(.*)""")]
        public void GivenTheStatements(string p0)
        {
        }

        [When(@"I compile the statement with Keystone")]
        public void WhenICompileTheStatementWithKeystone()
        {
            
        }

        [Then(@"the result is (.*)")]
        public void ThenTheResultIs(string p0)
        {
            
        }

        [Then(@"The last error is (.*)")]
        public void ThenTheLastErrorIs()
        {
            
        }

    }
}
