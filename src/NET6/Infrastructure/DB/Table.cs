using System.Collections.Generic;

namespace Infrastructure.DB
{
  /// <summary>
  /// テーブル
  /// </summary>
  public class Table
  {
    /// <summary>
    /// テーブル名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// コメント
    /// </summary>
    public string Comment { get; }

    /// <summary>
    /// カラムリスト
    /// </summary>
    public List<Column> Columns { get; } = new List<Column>();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name">テーブル名</param>
    /// <param name="comment">コメント</param>
    public Table(string name, string comment)
    {
      Name = name;
      Comment = comment;
    }
  }
}