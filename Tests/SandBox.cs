using KhelljyrBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SandBox
    {
        private string Content = @"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int owo = 42;
            Console.WriteLine(""Hello, World!"");
        }
    }
}";

        [TestMethod]
        public void TestMethod1()
        {
            Builder builder = new Builder();

            builder.Compile(Content);
        }
    }
}
