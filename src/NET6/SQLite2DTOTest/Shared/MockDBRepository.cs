using Domain.Classes;
using Domain.DB;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using Infrastructure.DB;

namespace SQLite2DTOTest.Shared
{
  /// <summary>
  /// DBモックリポジトリ
  /// </summary>
  /// <remarks>Applicatton用</remarks>
  public class MockDBRepository : IDBRepository
  {
    /// <summary>
    ////クラスエンティティリスト取得
    /// </summary>
    /// <param name="hostName">サーバーホスト</param>
    /// <param name="userID">ユーザーID</param>
    /// <param name="password">パスワード</param>
    /// <param name="database">データベース名</param>
    /// <param name="port">ポート番号</param>
    /// <returns>クラスエンティティリスト</returns>
    public List<ClassEntity> GetClasses(DBParameterEntity parameterEntity)
    {
      // 接続文字列作成
      var connectionString = $"Data Source={parameterEntity.SQLiteFilePath}";

      // テーブルリストを取得
      List<Table> tables;
      try
      {
        tables = GetTables(connectionString);
      }
      catch (Exception exception)
      {
        var exceptionMessages = new List<DomainExceptionMessage>();
        exceptionMessages.Add(new DomainExceptionMessage($"{connectionString}", ExceptionType.DBError));
        throw new DomainException(exceptionMessages.AsReadOnly(), exception);
      }

      // クラスエンティティリスト作成
      var result = new List<ClassEntity>();
      foreach (var table in tables)
      {
        var properties = table.Columns.Select(column => PropertyEntity.Create(column.Name, column.DataType, column.Comment)).ToList();
        result.Add(ClassEntity.Create(table.Name, table.Comment, properties.AsReadOnly()));
      }

      return result;
    }

    /// <summary>
    /// PostgreSQLからテーブルリストを作成し、返す
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>テーブルリスト</returns>
    private List<Table> GetTables(string connectionString)
    {
      var result = new List<Table>();
      Table tempTable = null;

      tempTable = new Table("m_test", "マスタテーブル");
      tempTable.Columns.Add(new Column("int_1", "integer", "intになる"));
      tempTable.Columns.Add(new Column("Date_1_1", "date", "Dateになる"));
      result.Add(tempTable);

      tempTable = new Table("t_test", "テストテーブル");
      tempTable.Columns.Add(new Column("String", "string", "stringになる"));
      result.Add(tempTable);

      return result;
    }
  }
}