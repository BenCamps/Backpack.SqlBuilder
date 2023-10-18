using System.Runtime.CompilerServices;
using Xunit.Abstractions;

using Assembly = System.Reflection.Assembly;
using Randomizer = Bogus.Randomizer;
using Stopwatch = System.Diagnostics.Stopwatch;

namespace Backpack.SqlBuilder.TestCommon
{
    public class TestBase
    {
        protected ISqlDialect Dialect { get; }

        private string _testTempPath;
        private Stopwatch _stopwatch;

        private Randomizer _rand;
        protected Randomizer Rand => _rand ??= new Randomizer();

        private List<string> FilesToBeDeleted { get; } = new List<string>();
        protected bool CleanUpTestFiles { get; set; }

        public string TestExecutionDirectory
        {
            get
            {
                // in .net 5 and later codeBase throws exception. This is because CodeBase is
                // depreciated and replaced with "Location" property. However Location returns
                // the location of the assembly after shadow-copy of assemblies where CodeBase
                // returns location before shadow-copy. since the testing framework uses shadow
                // copy and we want the before shadow-copy location.
                var codeBase = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                return Path.GetDirectoryName(codeBase);
            }
        }

        public string TestTempPath => _testTempPath ??= Path.Combine(Path.GetTempPath(), "TestTemp", GetType().FullName ?? GetType().ToString());
        public string TestFilesDirectory => Path.Combine(TestExecutionDirectory, "TestFiles");
        public string ResourceDirectory => Path.Combine(TestExecutionDirectory, "Resources");

        public ITestOutputHelper Output { get; }

        public TestBase(ITestOutputHelper output, ISqlDialect dialect)
        {
            Output = output;
            Dialect = dialect;
        }

        public string GetTempFilePathWithExt(string extention, [CallerMemberName] string testName = null)
        {
            var fileName = testName + "_" + Rand.Int().ToString("x") + "_" + extention;
            return GetTempFilePath(fileName);
        }

        public string GetTempFilePath(string fileName)
        {
            var path = Path.Combine(TestTempPath, fileName);
            Output.WriteLine(path);
            return path;
        }

        public string GetTestFile(string fileName) => InitializeTestFile(fileName);

        internal string InitializeTestFile(string fileName)
        {
            var sourcePath = Path.Combine(TestFilesDirectory, fileName);
            if (File.Exists(sourcePath) == false) { throw new FileNotFoundException(sourcePath); }

            var targetPath = Path.Combine(TestTempPath, fileName);

            RegesterFileForCleanUp(targetPath);
            File.Copy(sourcePath, targetPath, true);
            return targetPath;
        }

        public void RegesterFileForCleanUp(string path)
        {
            FilesToBeDeleted.Add(path);
        }

        public void StartTimer()
        {
            _stopwatch = new Stopwatch();
            Output.WriteLine("Stopwatch Started");
            _stopwatch.Start();
        }

        public void EndTimer()
        {
            _stopwatch.Stop();
            Output.WriteLine("Stopwatch Ended:" + _stopwatch.ElapsedMilliseconds.ToString() + "ms");
        }

        public void Dispose()
        {
            if (CleanUpTestFiles)
            {
                foreach (var file in FilesToBeDeleted)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }
        }
    }
}