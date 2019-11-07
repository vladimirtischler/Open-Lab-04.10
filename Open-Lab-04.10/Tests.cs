using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_04._10
{
    [TestFixture]
    public class Tests
    {

        private Calculator calc;
        private bool shouldStop;

        private const float Tolerance = 0.02f;
        private const float RandTolerance = 50f;

        private const int RandSeed = 410410410;
        private const int RandNumsCountMin = 1;
        private const int RandNumsCountMax = 100;
        private const int RandTestCasesCount = 97;

        [OneTimeSetUp]
        public void Init()
        {
            calc = new Calculator();
            shouldStop = false;
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome;

            if (outcome == ResultState.Failure || outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCase(new []{ 1, 0, 4, 5, 2, 4, 1, 2, 3, 3, 3 }, 2.54f)]
        [TestCase(new []{ 2, 3, 2, 3 }, 2.5f)]
        [TestCase(new []{ 3, 3, 3, 3, 3 }, 3f)]
        public void AverageTest(int[] nums, float expected) =>
            Assert.That(calc.Average(nums), Is.EqualTo(expected).Within(Tolerance));

        [TestCaseSource(nameof(GetRandom))]
        public void AverageTestRandom(int[] nums, float expected)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            Assert.That(calc.Average(nums), Is.EqualTo(expected).Within(RandTolerance));
        }

        private static IEnumerable GetRandom()
        {
            var rand = new Random(RandSeed);

            for (var i = 0; i < RandTestCasesCount; i++)
            {
                var nums = new int[rand.Next(RandNumsCountMin, RandNumsCountMax + 1)];

                for (var j = 0; j < nums.Length; j++)
                    nums[j] = rand.Next(int.MaxValue / nums.Length);

                yield return new TestCaseData(nums, nums.Sum() / (float) nums.Length);
            }
        }

    }
}
