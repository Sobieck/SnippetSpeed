This project is just something that I use to quickly test the speed of .Net code. 

## How it works

1. Create a Console application.
2. Import the SnippetSpeed NuGet package.
3. In the main method of your console app write the following line: `SnippetSpeed.SnippetSpeedConsoleInterface.Run();`
4. Create a new class that inherits from either `TestableSnippetBase<>` or `SnippetSpeedBase`
5. In the `Act()` or `T Act<T>()` method write the Snippet you would like to test.
6. Run your application! That is it. A CSV file will be generated in you bin folder at the end of the run. 

## Optional Configurations
1. Change the amount of time spent on each test class (Default is 5 minutes).
`SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound = new System.TimeSpan(0, 0, 10);`
2. Change the name and path of the results file.
`SnippetSpeedConsoleInterface.Settings.OutputPathAndFileName = @"C:\Github\README.md";`
3. Change how the output file is generated.
Create a new object that implements the IResultWriter
Instantiate that object and assign it to the results writer like so: `SnippetSpeedConsoleInterface.ResultWriter = new MdResultsWriter();`

#### Code Examples of IResultWriter implementation

```cs
namespace SpeedTest
{
    public class MdResultsWriter : IResultWriter
    {
        public void Write(ICollection<SnippetSpeedTestResult> results)
        {
            var writeList = new List<string>
            {
                "|ClassName|TypeOfTest|Iterations|NanosecondsPerIteration|LengthOfTest(ms)|",
                "|---------|----------|---------:|----------------------:|---------------:|"
            };

            foreach (var result in results)
            {
                writeList.Add($"|{result.NameOfClass}|{result.TypeOfTest}|{result.Interations}|{result.AverageTimeOfIterationInNanoseconds}|{(int)result.LengthOfTest.TotalMilliseconds}|");
            }

            File.WriteAllLines(SnippetSpeedConsoleInterface.Settings.OutputPathAndFileName, writeList.ToArray());
        }
    }
}

```

####Code Examples of Main Method

```cs
public static class Program
    {
        static void Main(string[] args)
        {
            SnippetSpeedConsoleInterface.Settings.LengthOfOneTestRound = new System.TimeSpan(0, 0, 10); // optional
            SnippetSpeedConsoleInterface.Settings.OutputPathAndFileName = @"C:\Github\README.md"; // optional
            SnippetSpeedConsoleInterface.ResultWriter = new MdResultsWriter(); //optional
            

            SnippetSpeedConsoleInterface.Run();
        }
    }

```
