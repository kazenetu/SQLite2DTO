using System.Collections.Generic;
using Domain.Exceptions;

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
    public string NameSpace { get; private set; }

    /// <summary>
    /// CSファイル出力先ディレクトリパス
    /// </summary>
    public string OutputPath { get; private set; }

    /// <summary>
    /// DB接続情報：SQLiteファイルのパス
    /// </summary>
    public string SQLiteFilePath { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="nameSpace">CSファイルのクラスに設定する名前空間</param>
    /// <param name="outputPath">出力先ディレクトリパス</param>
    /// <param name="sqliteFilePath">SQLiteファイルのパス</param>
    public InputParamModel(string nameSpace, string outputPath, string sqliteFilePath)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(nameSpace)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(nameSpace)}[{nameSpace}]", DomainExceptionMessage.ExceptionType.Empty));
      if (string.IsNullOrEmpty(outputPath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(outputPath)}[{outputPath}]", DomainExceptionMessage.ExceptionType.Empty));
      if (string.IsNullOrEmpty(sqliteFilePath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(sqliteFilePath)}[{sqliteFilePath}]", DomainExceptionMessage.ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      NameSpace = nameSpace;
      OutputPath = outputPath;
      SQLiteFilePath = sqliteFilePath;
    }
  }
}
