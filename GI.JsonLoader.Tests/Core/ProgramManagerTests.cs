using Microsoft.VisualStudio.TestTools.UnitTesting;
using GI.JsonLoader.Core;
using GI.JsonLoader.Models;

namespace GI.JsonLoader.Tests.Core
{
    [TestClass]
    public class ProgramManagerTests
    {

        [TestMethod]
        public void TrySelectFileReturnsFalseForBadFileName()
        {
            SetUp();

            const string fileName = "badFileName";
            const bool expected = false;

            var actual = ProgramManager.ProgramManagerInstance.TrySelectFile(fileName);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TrySelectFileReturnsTrueForGoodFileName()
        {
            SetUp();

            const string fileName = "followers.json";
            const bool expected = true;

            var actual = ProgramManager.ProgramManagerInstance.TrySelectFile(fileName);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetFileContentsGetsProperObjects()
        {
            SetUp();

            // XXX: An unfortunate side effect of locking everything down is relying on other tested methods to test down the pipe.
            // XXX: It might be a good idea to revisit this for testability's sake.
            const string fileName = "followers.json";
            var actual = ProgramManager.ProgramManagerInstance.TrySelectFile(fileName);

            Assert.IsTrue(actual);

            foreach (var item in ProgramManager.ProgramManagerInstance.GetFileContents())
            {
                Assert.IsTrue((item as Followers) != null);
            }
        }

        private void SetUp()
        {
            ProgramManager.ProgramManagerInstance = new ProgramManager();
        }
    }
}
