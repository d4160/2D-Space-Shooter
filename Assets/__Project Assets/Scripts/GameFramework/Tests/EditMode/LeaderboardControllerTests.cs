namespace GameFramework.Tests
{
    using UnityEngine;
    using NUnit;
    using NUnit.Framework;

    public class LeaderboardControllerTests
    {
        LeaderboardController _leaderboardController;

        [OneTimeSetUp]
        public void SetUp()
        {
            _leaderboardController = GameObject.FindObjectOfType<LeaderboardController>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _leaderboardController = null;
        }

        private void Login(string username)
        {
            var auth = _leaderboardController.Authenticator as AuthenticatorController;
            if (auth)
            {
                auth.Username = username;
                auth.Logout();
                auth.Login();
            }
        }

        [Test]
        public void SubmitPlayer1HighScore10()
        {
            if (_leaderboardController)
            {
                Login("Player1");
                _leaderboardController.SubmitStat("highScore", 10);

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void SubmitPlayer1HighScore7()
        {
            if (_leaderboardController)
            {
                Login("Player1");
                _leaderboardController.SubmitStat("highScore", 7);

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void SubmitPlayer1HighScore14()
        {
            if (_leaderboardController)
            {
                Login("Player1");
                _leaderboardController.SubmitStat("highScore", 14);

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void SubmitPlayer2HighScore10()
        {
            if (_leaderboardController)
            {
                Login("Player2");
                _leaderboardController.SubmitStat("highScore", 10);

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void SubmitPlayer2HighScore7()
        {
            if (_leaderboardController)
            {
                Login("Player2");
                _leaderboardController.SubmitStat("highScore", 7);

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void SubmitPlayer2HighScore14()
        {
            if (_leaderboardController)
            {
                Login("Player2");
                _leaderboardController.SubmitStat("highScore", 14);

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}