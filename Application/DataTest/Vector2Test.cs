using Data;

namespace DataTest
{
    [TestClass]
    public class Vector2Test
    {
        [TestMethod]
        public void AddTest()
        {
            IVector2 a = IVector2.Create(11.0f, 22.0f);
            IVector2 b = IVector2.Create(33.0f, 44.0f);
            float cX = a.X + b.X;
            float cY = a.Y + b.Y;
            Assert.AreEqual(44.0f, cX);
            Assert.AreEqual(66.0f, cY);
        }
        
        [TestMethod]
        public void SubtractTest()
        {
            IVector2 a = IVector2.Create(11.0f, 22.0f);
            IVector2 b = IVector2.Create(33.0f, 44.0f);
            float cX = a.X - b.X;
            float cY = a.Y - b.Y;
            Assert.AreEqual(-22.0f, cX);
            Assert.AreEqual(-22.0f, cY);
        }
        
        [TestMethod]
        public void ScalarMultiplyTest()
        {
            IVector2 a = IVector2.Create(1.0f, 2.0f);
            float aX3 = a.X * 4.0f;
            float aY3 = a.Y * 3.0f;
            Assert.AreEqual(4, aX3);
            Assert.AreEqual(6, aY3);
        }
        
        [TestMethod]
        public void ScalarDivideTest()
        {
            var a = IVector2.Create(1.0f, 2.0f);
            var ax = a.X / 2.0f;
            var ay = a.Y / 2.0f;
            Assert.AreEqual(0.5f, ax);
            Assert.AreEqual(1.0f, ay);
        }
        
        [TestMethod]
        public void DistanceTest()
        {
            IVector2 a = IVector2.Create(1.0f, 2.0f);
            IVector2 b = IVector2.Create(4.0f, 6.0f);
            float distance = a.Distance(b);
            Assert.AreEqual(5.0f, distance);
        }
    }
}