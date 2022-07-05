using Domain.Exceptions;
using System.Collections.Generic;

namespace Domain.Classes
{
  /// <summary>
  /// プロパティエンティティ
  /// </summary>
  public class PropertyEntity
  {
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; init; }

    /// <summary>
    /// 型名
    /// </summary>
    /// <value>型名称</value>
    public string TypeName { get; init; }

    /// <summary>
    /// コメント
    /// </summary>
    /// <value>コメント文字列</value>
    public string Comment { get; init; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private PropertyEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="typeName">型名称</param>
    /// <param name="comment">コメント文字列</param>
    /// <returns>プロパティエンティティ インスタンス</returns>
    public static PropertyEntity Create(string name, string typeName, string comment)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(name)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(name)}[{name}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(typeName)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(typeName)}[{typeName}]", ExceptionType.Empty));
      if (string.IsNullOrEmpty(comment)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(comment)}[{comment}]", ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      return new PropertyEntity()
      {
        Name = name,
        TypeName = typeName,
        Comment = comment
      };
    }
  }
}


