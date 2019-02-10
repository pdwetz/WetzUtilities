# WetzUtilities
[![NuGet Version](https://img.shields.io/nuget/v/WetzUtilities.svg)](https://www.nuget.org/packages/WetzUtilities/)

Common utility and extension methods for .NET projects to improve quality-of-life for the developer.

## Quick Usage Example
```c#
using WetzUtilities;
  
public class Example
{
  public string Foo { get; set; }
  public bool IsFooMissing => Foo.IsEmpty();
  public string TruncatedFoo => Foo.Shave(20);
  public string FooUrl => Foo.UrlFriendly();
  
  public DateTimeOffset? Date { get; set; }
  public string DateFormatted => Date.SafeShortDate();
}
```
