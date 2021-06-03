using System.Drawing;
using NUnit.Framework;

namespace TheGame.tests
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void TestPLayerMovement()
        {
            var map = new Map(new[]
            {
                "  ",
                "  "
            });
            var gameModel = new GameModel(new Level(map));
            gameModel.MovePlayer(MoveDirection.Right);
            Assert.AreEqual(new Point(1,0), gameModel.Player.Position);
            gameModel.MovePlayer(MoveDirection.Left);
            Assert.AreEqual(new Point(0,0), gameModel.Player.Position);
            gameModel.MovePlayer(MoveDirection.Down);
            Assert.AreEqual(new Point(0,1), gameModel.Player.Position);
            gameModel.MovePlayer(MoveDirection.Up);
            Assert.AreEqual(new Point(0,0), gameModel.Player.Position);
        }
        
        
    }
}