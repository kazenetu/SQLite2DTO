using System;

namespace Infrastructure.DB
{
  /// <summary>
  /// カラム
  /// </summary>
  public class Column
  {
    /// <summary>
    /// カラム名
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// データ型
    /// </summary>
    public string DataType { get; }

    /// <summary>
    /// コメント
    /// </summary>
    public string Comment { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name">カラム型</param>
    /// <param name="dataType">DBのデータ型</param>
    /// <param name="comment">コメント</param>
    public Column(string name, string dataType, string comment)
    {
      Name = name;
      Comment = comment;

      // データ型の変換
      switch (dataType.ToLower())
      {
        case "integer":
        case "int":
        case "int4":
        case "smallint":
        case "int2":
        case "smallserial":
        case "serial2":
        case "serial":
        case "serial4":
          DataType = "int";
          break;
        case "decimal":
        case "numeric":
          DataType = "decimal";
          break;
        case "bigint":
        case "int8":
          DataType = "long";
          break;
        case "real":
        case "float4":
          DataType = "float";
          break;
        case "double precision":
        case "float8":
          DataType = "double";
          break;
        case "bool":
        case "boolean":
          DataType = "bool";
          break;
        case "date":
        case "time":
        case "timestamp":
          DataType = typeof(DateTime).Name;
          break;
        case "time without time zone":
        case "timetz":
        case "timestamp without time zone":
        case "timestamptz":
          DataType = typeof(DateTimeOffset).Name;
          break;
        default:
          DataType = "string";
          break;
      }
    }
  }
}
