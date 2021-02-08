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
        [InlineData("rubin", "r150")]
        [InlineData("ashcraft", "a261")]
        [InlineData("ashcroft", "a261")]
        [InlineData("tymczak", "t522")]
        [InlineData("pfister", "p236")]
        [InlineData("honeyman", "h555")]
        public void SoundexTest(string input, string expected)
        {
            Assert.Equal(expected, Soundex.Soundex.StringToSoundex(input));
        }

        [Theory]
        [InlineData("zach", "zac", 4)]
        [InlineData("lake", "blake", 3)]
        [InlineData("brad", "lad", 2)]
        [InlineData("horrible", "great", 1)]
        public void DifferenceTest(string a, string b, int expected)
        {
            Assert.Equal(expected, Soundex.Soundex.Difference(a, b));
        }
    }
}
