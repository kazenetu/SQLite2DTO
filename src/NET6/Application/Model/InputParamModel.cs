using Domain.Exceptions;
using System.Collections.Generic;

namespace Application.Model
{
  /// <summary>
  /// 入力パラメータモデル
  /// </summary>
  public class InputParamModel
  {
    /// <summary>
    /// CSファイルのクラスに設定する名前空間
    /// </summary>
    public string NameSpace { get; init; }

    /// <summary>
    /// CSファイル出力先ディレクトリパス
    /// </summary>
    public string OutputPath { get; init; }

    /// <summary>
    /// DB接続情報：SQLiteファイルのパス
    /// </summary>
    public string SQLiteFilePath { get; init; }

    /// <summary>
    /// スネークケースのままとするか
    /// </summary>
    public bool UseSnakeCase { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="nameSpace">CSファイルのクラスに設定する名前空間</param>
    /// <param name="outputPath">出力先ディレクトリパス</param>
    /// <param name="sqliteFilePath">SQLiteファイルのパス</param>
    /// <param name="useSnakeCase">スネークケースのままとするか</param>
    public InputParamModel(string nameSpace, string outputPath, string sqliteFilePath, bool useSnakeCase)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(nameSpace)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(nameSpace)}[{nameSpace}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(outputPath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(outputPath)}[{outputPath}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(sqliteFilePath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(sqliteFilePath)}[{sqliteFilePath}]", ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      NameSpace = nameSpace;
      OutputPath = outputPath;
      SQLiteFilePath = sqliteFilePath;
      UseSnakeCase = useSnakeCase;
    }
  }
}
