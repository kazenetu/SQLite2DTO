using Domain.Classes;
using Domain.Exceptions;
using System.Collections.Generic;
using System;
using Xunit;

namespace PostgreSQL2DTOTest.Domain.Classes
{
  /// <summary>
  /// プロパティエンティティのテスト
  /// </summary>
  public class TestPropertyEntity : IDisposable
  {
    /// <summary>
    /// Setup
    /// </summary>
    public TestPropertyEntity()
    {
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact]
    public void ExceptionAllNull()
    {
      string name = null;
      string typeName = null;
      string comment = null;

      var ex = Assert.ThrowsAny<DomainException>(() => PropertyEntity.Create(name, typeName, comment));
      Assert.Equal(3, ex.MessageIds.Count);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("name[]", ex.MessageIds[0].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[1].MessageID);
      Assert.Equal("typeName[]", ex.MessageIds[1].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[2].MessageID);
      Assert.Equal("comment[]", ex.MessageIds[2].Target);
    }

    [Fact]
    public void ExceptionNameNull()
    {
      string name = null;
      string typeName = "string";
      string comment = "コメント";

      var ex = Assert.ThrowsAny<DomainException>(() => PropertyEntity.Create(name, typeName, comment));
      Assert.Single(ex.MessageIds);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("name[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void ExceptionTypeNameNull()
    {
      string name = "column";
      string typeName = null;
      string comment = "コメント";

      var ex = Assert.ThrowsAny<DomainException>(() => PropertyEntity.Create(name, typeName, comment));
      Assert.Single(ex.MessageIds);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("typeName[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void ExceptionCommentNull()
    {
      string name = "column";
      string typeName = "string";
      string comment = null;

      var ex = Assert.ThrowsAny<DomainException>(() => PropertyEntity.Create(name, typeName, comment));
      Assert.Single(ex.MessageIds);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("comment[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void Success()
    {
      string name = "column";
      string typeName = "string";
      string comment = "コメント";

      var instance = PropertyEntity.Create(name, typeName, comment);

      Assert.Equal("column", instance.Name);
      Assert.Equal("string", instance.TypeName);
      Assert.Equal("コメント", instance.Comment);
    }
  }
}


