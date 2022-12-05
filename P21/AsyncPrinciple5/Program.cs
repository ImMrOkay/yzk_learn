using System;

namespace AsyncPrinciple5
{
    internal class Program
    {
        /* 接口中的异步方法
         * 1.async 是提示编译器为异步方法中的await代码进行分段处理（状态机）的，
         * 而一个异步方法是否修饰了async对于方法调用者来讲是没有区别的，
         * 因此对于接口中的方法或者抽象方法中不能修饰为async
         *
         * 2.异步与yield
         * yield return 不仅能够简化数据的返回，而且可以让数据处理“流水线”化，提升性能
         * static IEnumerable<string> Test()
         * {
         *      yield return "a";
         *      yield return "b";
         *      yield return "c";
         * }
         */


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
