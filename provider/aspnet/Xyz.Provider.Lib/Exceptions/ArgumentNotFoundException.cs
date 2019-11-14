using System;

namespace Xyz.Provider.Lib.Exceptions
{
  /// <summary>
  /// An exception that is thrown when an entity referred to by a method's argument cannot be found.
  /// </summary>
  public class ArgumentNotFoundException : ArgumentException
  {
    public string Name { get; }
    public object Id { get; }

    public ArgumentNotFoundException()
    { }

    public ArgumentNotFoundException(string message)
      : base(message)
    { }

    public ArgumentNotFoundException(string message, string paramName)
      : base(message, paramName)
    { }

    public ArgumentNotFoundException(string name, object id)
      : base($"{name} with ID {id} was not found.")
    {
      Name = name;
      Id = id;
    }

    public ArgumentNotFoundException(string name, object id, string paramName)
      : base($"{name} with ID {id} was not found.", paramName)
    {
      Name = name;
      Id = id;
    }

    public ArgumentNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    { }

    public ArgumentNotFoundException(string message, string paramName, Exception innerException)
      : base(message, paramName, innerException)
    { }

    public ArgumentNotFoundException(string name, object id, Exception innerException)
      : base($"{name} with ID {id} was not found.", innerException)
    {
      Name = name;
      Id = id;
    }

    public ArgumentNotFoundException(string name, object id, string paramName, Exception innerException)
      : base($"{name} with ID {id} was not found.", paramName, innerException)
    {
      Name = name;
      Id = id;
    }
  }
}
