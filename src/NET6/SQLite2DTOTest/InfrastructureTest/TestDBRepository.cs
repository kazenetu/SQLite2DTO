using Domain.DB;
using Domain.Exceptions;
using Infrastructure.DB;
using System;
using System.Linq;
using Xunit;

namespace SQLite2DTOTest.InfrastructureTest
{
  /// <summary>
  /// DBリポジトリのテスト
  /// </summary>
  public class TestDBRepository : IDisposable
  {
    /// <summary>
    /// SQLiteファイル例の参照パス
    /// </summary>
    private const string SQLitePath = "SQLiteExample/Test.db";

    /// <summary>
    /// 正しくないSQLiteファイルの参照パス
    /// </summary>
    /// <remarks>SQLiteファイルが自動生成される</remarks>
    private const string FailedSQLitePath = "SQLiteExample/db.db";

    /// <summary>
    /// テスト対象
    /// </summary>
    private DBRepository repository;

    /// <summary>
    /// Setup
    /// </summary>
    public TestDBRepository()
    {
      repository = new DBRepository();
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact]
    public void ExceptionDBConnect()
    {
      var classes = repository.GetClasses(DBParameterEntity.Create(FailedSQLitePath));

      // classes Empty
      Assert.Empty(classes);
    }

    [Fact]
    public void DBConnect()
    {
      var classes = repository.GetClasses(DBParameterEntity.Create(SQLitePath));

      // Class Count
      Assert.Equal(4, classes.Count);

      // MUser Property Count
      var mUser = classes.Where(item => item.Name == "m_user").FirstOrDefault();
      Assert.NotNull(mUser);
      Assert.Equal(5, mUser.Properties.Count);

      // MReceiver Property Count
      var mReceiver = classes.Where(item => item.Name == "m_receiver").FirstOrDefault();
      Assert.NotNull(mReceiver);
      Assert.Equal(6, mReceiver.Properties.Count);
    }
  }
}
