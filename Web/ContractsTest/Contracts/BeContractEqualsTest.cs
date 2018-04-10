using Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeRoadTest.Contracts
{
    public class BeContractEqualsTest
    {
        public void AreEquals(BeContract expected, BeContract actual)
        {
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Version, actual.Version);
            for (int i = 0; i < expected.Inputs?.Count; i++)
            {
                Assert.AreEqual(expected.Inputs[i].Description, actual.Inputs[i].Description);
                Assert.AreEqual(expected.Inputs[i].Key, actual.Inputs[i].Key);
                Assert.AreEqual(expected.Inputs[i].Required, actual.Inputs[i].Required);
                Assert.AreEqual(expected.Inputs[i].Type, actual.Inputs[i].Type);
            }
            for (int i = 0; i < expected.Queries?.Count; i++)
            {
                Assert.AreEqual(expected.Queries[i].Contract.Id, actual.Queries[i].Contract.Id);
                for (int j = 0; j < expected.Queries[i].Mappings.Count; j++)
                {
                    Assert.AreEqual(expected.Queries[i].Mappings[j].LookupInputKey, actual.Queries[i].Mappings[j].LookupInputKey);
                    Assert.AreEqual(expected.Queries[i].Mappings[j].LookupInputId, actual.Queries[i].Mappings[j].LookupInputId);
                    Assert.AreEqual(expected.Queries[i].Mappings[j].InputKey, actual.Queries[i].Mappings[j].InputKey);
                }
            }

            for (int i = 0; i < expected.Outputs?.Count; i++)
            {
                Assert.AreEqual(expected.Outputs[i].Description, actual.Outputs[i].Description);
                Assert.AreEqual(expected.Outputs[i].Key, actual.Outputs[i].Key);
                Assert.AreEqual(expected.Outputs[i].LookupInputId, actual.Outputs[i].LookupInputId);
                Assert.AreEqual(expected.Outputs[i].Type, actual.Outputs[i].Type);
            }
        }

    }
}
