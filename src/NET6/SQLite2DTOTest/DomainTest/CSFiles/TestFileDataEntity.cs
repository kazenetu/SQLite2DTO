using Domain.Exceptions;
using Domain.CSFiles;
using System.Collections.Generic;
using System;
using Xunit;

namespace PostgreSQL2DTOTest.Domain.CSFiles
{
  /// <summary>
  /// ファイル作成パラメータエンティティのテスト
  /// </summary>
  public class TestFileDataEntity : IDisposable
  {
    /// <summary>
    /// Setup
    /// </summary>
    public TestFileDataEntity()
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
      string outputPath = null;
      string nameSpace = null;

      var ex = Assert.ThrowsAny<DomainException>(() => FileDataEntity.Create(outputPath, nameSpace));
      Assert.Equal(2, ex.MessageIds.Count);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("outputPath[]", ex.MessageIds[0].Target);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[1].MessageID);
      Assert.Equal("nameSpace[]", ex.MessageIds[1].Target);
    }

    [Fact]
    public void ExceptionOutputPathNull()
    {
      string outputPath = null;
      string nameSpace = "NameSpace";

      var ex = Assert.ThrowsAny<DomainException>(() => FileDataEntity.Create(outputPath, nameSpace));
      Assert.Single(ex.MessageIds);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("outputPath[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void ExceptionNameSpacehNull()
    {
      string outputPath = "CSOutput";
      string nameSpace = null;

      var ex = Assert.ThrowsAny<DomainException>(() => FileDataEntity.Create(outputPath, nameSpace));
      Assert.Single(ex.MessageIds);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("nameSpace[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void Success()
    {
      string outputPath = "CSOutput";
      string nameSpace = "NameSpace";

      var instance = FileDataEntity.Create(outputPath, nameSpace);

      Assert.Equal("CSOutput", instance.OutputPath);
      Assert.Equal("NameSpace", instance.NameSpace);
    }
  }
}