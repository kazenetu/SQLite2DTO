using Domain.DB;
using Domain.Exceptions;
using System.Collections.Generic;
using System;
using Xunit;

namespace PostgreSQL2DTOTest.Domain.DB
{
  /// <summary>
  /// DB接続パラメータエンティティのテスト
  /// </summary>
  public class TestDBParameterEntity : IDisposable
  {
    /// <summary>
    /// Setup
    /// </summary>
    public TestDBParameterEntity()
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
      string sqlitePath = null;

      var ex = Assert.ThrowsAny<DomainException>(() => DBParameterEntity.Create(sqlitePath));
      Assert.Single(ex.MessageIds);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.MessageIds[0].MessageID);
      Assert.Equal("sqliteFilePath[]", ex.MessageIds[0].Target);
    }

    [Fact]
    public void Success()
    {
      string sqlitePath = "sqlitePath";

      var instance = DBParameterEntity.Create(sqlitePath);

      Assert.Equal("sqlitePath", instance.SQLiteFilePath);
    }
  }
}