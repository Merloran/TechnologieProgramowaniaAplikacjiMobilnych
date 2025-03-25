using Application;

namespace ApplicationTest
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void TestAdd()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(calculator.Add(3, 5), 8);
        }
    }
}