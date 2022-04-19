using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain.DB
{
  /// <summary>
  /// DB接続パラメータエンティティ
  /// </summary>
  public class DBParameterEntity
  {
    /// <summary>
    /// DB接続情報：SQLiteファイルのパス
    /// </summary>
    public string SQLiteFilePath { get; private set; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private DBParameterEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="sqliteFilePath">SQLiteファイルのパス</param>
    /// <returns>DB接続パラメータエンティティ インスタンス</returns>
    public static DBParameterEntity Create(string sqliteFilePath)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(sqliteFilePath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(sqliteFilePath)}[{sqliteFilePath}]", DomainExceptionMessage.ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      return new DBParameterEntity()
      {
        SQLiteFilePath = sqliteFilePath,
      };
    }
  }
}