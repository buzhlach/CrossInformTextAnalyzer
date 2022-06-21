using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CrossInformTextAnalyzer.Tests
{
    [TestClass]
    public class TextAnalyzerTest
    {
        [TestMethod]
        public void TextAnalyzerCorrectCreate()
        {
            var textAnalyzer = new TextAnalyzer("jdjddj");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TextAnalyzerIncorrectCreate()
        {
            var textAnalyzer = new TextAnalyzer(null);
        }

        [TestMethod]
        public void SetTextFromFileCorrect(){
            var textAnalyzer = new TextAnalyzer();
            
            string filePath = @"D:\Work\Crossinform\TextAnalyzer\TextAnalyzer\1.txt";
            textAnalyzer.SetTextFromFile(filePath);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentNullException))]
        public void SetTextFromFileIncorrectNullArgumentException()
        {
            var textAnalyzer = new TextAnalyzer();

            string filePath = null;
            textAnalyzer.SetTextFromFile(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void SetTextFromFileIncorrectArgumentException()
        {
            var textAnalyzer = new TextAnalyzer();

            string filePath = @"D:\Workjkjdkjdk1.txt";
            textAnalyzer.SetTextFromFile(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetTheMostCommonWordsSubstringsOutOfRangeSubstringLength()
        {
            var textAnalyzer = new TextAnalyzer();

            textAnalyzer.GetTheMostCommonWordsSubstrings(0, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetTheMostCommonWordsSubstringsOutOfRangeSubstringsCount()
        {
            var textAnalyzer = new TextAnalyzer();

            textAnalyzer.GetTheMostCommonWordsSubstrings(1, 0);
        }
    }
}
