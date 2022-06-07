using Domain.DB;
using Domain.Exceptions;
using System.Collections.Generic;
using System;
using Xunit;

namespace SQLite2DTOTest.Domain.DB
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
      Assert.Single(ex.Messages);

      Assert.Equal(DomainExceptionMessage.ExceptionType.Empty, ex.Messages[0].MessageID);
      Assert.Equal("sqliteFilePath[]", ex.Messages[0].Target);
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