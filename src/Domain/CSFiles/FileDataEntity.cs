using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain.CSFiles
{
  /// <summary>
  /// ファイル作成パラメータエンティティ
  /// </summary>
  public class FileDataEntity
  {
    /// <summary>
    /// 出力パス
    /// </summary>
    public string OutputPath { get; private set; }

    /// <summary>
    /// 名前空間
    /// </summary>
    public string NameSpace { get; private set; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private FileDataEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="outputPath">出力パス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <returns>ファイル作成パラメータエンティティ インスタンス</returns>
    public static FileDataEntity Create(string outputPath, string nameSpace)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(outputPath)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(outputPath)}[{outputPath}]", DomainExceptionMessage.ExceptionType.Empty));
      if (string.IsNullOrEmpty(nameSpace)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(nameSpace)}[{nameSpace}]", DomainExceptionMessage.ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      return new FileDataEntity()
      {
        OutputPath = outputPath,
        NameSpace = nameSpace
      };
    }
  }
}