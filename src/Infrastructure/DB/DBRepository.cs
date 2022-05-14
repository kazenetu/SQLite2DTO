using Domain.Classes;
using Domain.DB;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Infrastructure.DB
{
  /// <summary>
  /// DBリポジトリ
  /// </summary>
  public class DBRepository : IDBRepository
  {
    /// <summary>
    /// コネクションインスタンス
    /// </summary>
    private SqliteConnection conn = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DBRepository()
    {
    }

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
      this.conn = new SqliteConnection(connectionString);
      this.conn.Open();

      // テーブルリストを取得
      List<Table> tables;
      try
      {
        tables = GetTables(connectionString);
      }
      catch (Exception exception)
      {
        var exceptionMessages = new List<DomainExceptionMessage>();
        exceptionMessages.Add(new DomainExceptionMessage($"{connectionString}", DomainExceptionMessage.ExceptionType.DBError));
        throw new DomainException(exceptionMessages.AsReadOnly(), exception);
      }

      // クラスエンティティリスト作成
      var result = new List<ClassEntity>();
      foreach (var table in tables)
      {
        var properties = table.Columns.Select(column => PropertyEntity.Create(column.Name, column.DataType, column.Comment)).ToList();
        result.Add(ClassEntity.Create(table.Name, table.Comment, properties.AsReadOnly()));
      }

      this.conn.Close();

      return result;
    }

    /// <summary>
    /// SQLiteからテーブルリストを作成し、返す
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns>テーブルリスト</returns>
    private List<Table> GetTables(string connectionString)
    {
      // テーブル一覧取得
      var tableNames = new List<string>();
      var tableResults = Fill("select tbl_name from sqlite_master where type = 'table';");
      foreach (DataRow row in tableResults.Rows)
      {
        tableNames.Add(row["tbl_name"].ToString());
      }

      // テーブル情報取得
      var result = new List<Table>();
      foreach (var tableName in tableNames)
      {
        var tableResult = Fill($"select * from {tableName};");
        var tempTable = new Table(tableName, $"{tableName}テーブル");

        foreach (DataColumn col in tableResult.Columns)
        {
          // カラム追加
          var columnName = col.ColumnName;
          var dataType = col.DataType.Name;
          var columnComment = $"{columnName}";
          tempTable.Columns.Add(new Column(columnName, dataType, columnComment));
        }
        result.Add(tempTable);
      }

      return result;
    }

    /// <summary>
    /// 検索SQL実行
    /// </summary>
    /// <param name="sql">SQLステートメント</param>
    /// <returns>検索結果</returns>
    private DataTable Fill(string sql)
    {
      // SQL未設定
      if (sql == string.Empty)
      {
        return new DataTable();
      }

      // SQL発行
      using (SqliteCommand command = conn.CreateCommand())
      {
        command.CommandText = sql;

        using (SqliteDataReader reader = command.ExecuteReader())
        {
          //スキーマ取得
          var result = this.GetShcema(reader);

          while (reader.Read())
          {
            var addRow = result.NewRow();

            foreach (DataColumn col in result.Columns)
            {
              addRow[col.ColumnName] = reader[col.ColumnName];
            }

            result.Rows.Add(addRow);
          }

          return result;
        }
      }
    }

    /// <summary>
    /// スキーマ取得
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    private DataTable GetShcema(SqliteDataReader reader)
    {
      var result = new DataTable();

      for (var i = 0; i < reader.FieldCount; i++)
      {
        result.Columns.Add(new DataColumn(reader.GetName(i)));
      }

      return result;
    }

  }
}