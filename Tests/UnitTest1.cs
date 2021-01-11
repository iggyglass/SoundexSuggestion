using System;
using Xunit;
using Soundex;

namespace Tests
{
    public class UnitTest1
    {
        [Theory] // Test data a la wikipedia
        [InlineData("robert", "r163")]
        [InlineData("rupert", "r163")]
        [InlineData("rubin", "r15")]
        [InlineData("ashcraft", "a2613")]
        [InlineData("ashcroft", "a2613")]
        [InlineData("tymczak", "t522")]
        [InlineData("pfister", "p236")]
        [InlineData("honeyman", "h555")]
        public void SoundexTest(string input, string expected)
        {
            Assert.Equal(expected, Soundex.Soundex.StringToSoundex(input));
        }
    }
}
