using Domain.Exceptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Classes
{
  /// <summary>
  /// クラスエンティティ
  /// </summary>
  public class ClassEntity
  {
    /// <summary>
    /// 名称
    /// </summary>
    /// <value>プロパティ名</value>
    public string Name { get; private set; }

    /// <summary>
    /// コメント
    /// </summary>
    /// <value>コメント文字列</value>
    public string Comment { get; private set; }

    /// <summary>
    /// プロパティリスト
    /// </summary>
    /// <returns>プロパティリスト</returns>
    public ReadOnlyCollection<PropertyEntity> Properties { get; private set; }

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private ClassEntity()
    {
    }

    /// <summary>
    /// インスタンス生成
    /// </summary>
    /// <param name="name">クラス名称</param>
    /// <param name="comment">コメント文字列</param>
    /// <param name="properties">プロパティリスト</param>
    /// <returns>クラスエンティティ インスタンス</returns>
    public static ClassEntity Create(string name, string comment, ReadOnlyCollection<PropertyEntity> properties)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (string.IsNullOrEmpty(name)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(name)}[{name}]", DomainExceptionMessage.ExceptionType.Empty));
      if (string.IsNullOrEmpty(comment)) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(comment)}[{comment}]", DomainExceptionMessage.ExceptionType.Empty));
      if (properties.Count == 0) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(properties)}[{properties}]", DomainExceptionMessage.ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      return new ClassEntity()
      {
        Name = name,
        Comment = comment,
        Properties = properties
      };
    }
  }
}
