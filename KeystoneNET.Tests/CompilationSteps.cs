using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;

namespace KeystoneNET.Tests
{
    [Binding]
    class CompilationSteps
    {
        [Given(@"An instance of Keystone built for (.*) in mode (.*)")]
        public void GivenAnInstanceOfKeystoneBuiltForInMode(string p0, string p1)
        {
            var arch = (KeystoneArchitecture)Enum.Parse(typeof(KeystoneArchitecture), $"KS_ARCH_{p0}");
            var mode = (KeystoneMode)Enum.Parse(typeof(KeystoneMode), $"KS_MODE_{p1}");

            var keystone = new Keystone(arch, mode, false);
            ScenarioContext.Current.Add("keystoneInstance", keystone);
        }

        [Given(@"The statements ""(.*)""")]
        public void GivenTheStatements(string p0)
        {
            ScenarioContext.Current.Add("statements", p0);
        }

        [When(@"I compile the statement with Keystone")]
        public void WhenICompileTheStatementWithKeystone()
        {
            var statements = ScenarioContext.Current["statements"] as string;
            var engine = ScenarioContext.Current["keystoneInstance"] as Keystone;
            var result = engine.Assemble(statements, 0);

            ScenarioContext.Current.Add("assembleResult", result);
        }

        [Then(@"the result is (.*)")]
        public void ThenTheResultIs(string p0)
        {
            string[] bytes = p0.Split(',');
            var expectedBytes = bytes.Select(x => byte.Parse(x, NumberStyles.HexNumber))
                                     .ToList();

            var result = ScenarioContext.Current["assembleResult"] as KeystoneEncoded;
            Assert.AreEqual(result.Buffer.Length, expectedBytes.Count);

            for(int i=0; i< expectedBytes.Count; i++)
                Assert.AreEqual(result.Buffer[i], expectedBytes[i]);
        }

        [Then(@"The last error is (.*)")]
        public void ThenTheLastErrorIs(string error)
        {
            var errorType = (KeystoneError)Enum.Parse(typeof(KeystoneError), $"KS_ERR_{error}");
            var engine = ScenarioContext.Current["keystoneInstance"] as Keystone;
            Assert.AreEqual(engine.GetLastKeystoneError(), errorType);
        }
    }
}
