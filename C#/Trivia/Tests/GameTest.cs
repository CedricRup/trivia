using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Trivia;
using Xunit;


namespace Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class GameTest
    {
        [Fact]
        public void Test1()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            var game = new Game();
            game.Add("Cedric");
            game.Roll(12);
            game.Yes();
            game.Roll(2);
            game.Roll(13);
            game.No();
            game.Roll(13);
            Approvals.Verify(fakeconsole.ToString());
        }
        
        [Fact]
        public void Test2()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            var game = new Game();
            game.Add("Cedric");
            game.Add("Eloïse");
            game.Roll(1);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            Approvals.Verify(fakeconsole.ToString());
        }
        
        [Fact]
        public void Test3()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            var game = new Game();
            game.Add("Cedric");
            game.Add("Eloïse");
            game.Roll(1);
            game.Yes();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            game.Roll(2);
            game.No();
            Approvals.Verify(fakeconsole.ToString());
        }
    }
}
