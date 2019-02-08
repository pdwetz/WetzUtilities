using System.IO;
using System.Reflection;
using Xunit;

namespace WetzUtilities.Test
{
    public class FileUtilitiesTests
    {
        private readonly string _assetPath;

        public FileUtilitiesTests()
        {
            var appPath = Path.GetDirectoryName(typeof(FileUtilitiesTests).GetTypeInfo().Assembly.Location);
            _assetPath = Path.Combine(appPath, "assets");
        }

        [Fact]
        public void GetNextNameTest_singlematch()
        {
            var renameFilepath = FileUtilities.GetNextName(_assetPath, "alfa.txt");
            var renameFile = Path.GetFileName(renameFilepath);
            Assert.Equal("alfa-1.txt", renameFile);
        }

        [Fact]
        public void GetNextNameTest_multimatch()
        {
            var renameFilepath = FileUtilities.GetNextName(_assetPath, "bravo.txt");
            var renameFile = Path.GetFileName(renameFilepath);
            Assert.Equal("bravo-2.txt", renameFile);
        }

        [Fact]
        public void GetNextNameTest_nomatch()
        {
            var renameFilepath = FileUtilities.GetNextName(_assetPath, "charlie.txt");
            var renameFile = Path.GetFileName(renameFilepath);
            Assert.Equal("charlie.txt", renameFile);
        }
    }
}