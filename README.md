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
## DateTime and DateTimeOffset
Methods in both classes mirror each other and cover both normal and nullable types when appropriate.
* IsEmpty
* IsNotEmpty
* SafeYear
* SafeShortDate

## File
* SetupDirectory
* LoadTextFile
* LoadTextFileAsync
* WriteTextFile
* WriteTextFileAsync
* GetNextName
* SafeMoveFile
* SafeCopyFile
* SafeRenameFile

## Guid
* IsEmpty
* IsNotEmpty
* GetNextGuid

## Nullable
* SafeEquals
* SafeHashCode

## String
* IsEmpty
* IsNotEmpty
* IsUnorderedSubstring
* SafeEquals
* SafeHashCode
* Shave
* ParsePrefix
* TryParseDate/Offset
* ParseTimeSpan
* Contains
* URLFriendly
* RemapInternationalCharToAscii

## Utf8StringWriter
Inherits StringWriter with an override to explicitly encode using UTF8.
