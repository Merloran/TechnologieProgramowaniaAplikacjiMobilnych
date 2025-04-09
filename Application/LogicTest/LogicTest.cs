using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace LogicTest
{
    [TestClass]
    public class LogicTest
    {

        [TestMethod]
        public void CreateTest()
        {
            ILogicAbstract logic = ILogicAbstract.CreateInstance(null, null);
            Assert.IsNotNull(logic);
        }
    }
}