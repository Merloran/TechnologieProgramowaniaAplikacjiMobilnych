using Task_0;

namespace Test_Task_0
{
    [TestClass]
    public class TestCalculator
    {
        [TestMethod]
        public void Test_Add()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(calculator.Add(3, 2), 5);
        }
    }
}