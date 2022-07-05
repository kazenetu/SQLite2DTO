using System;
using System.Collections.ObjectModel;

namespace Domain.Exceptions
{
  /// <summary>
  /// ドメインレイヤー用例外クラス
  /// </summary>
  public class DomainException : Exception
  {
    /// <summary>
    /// メッセージリスト
    /// </summary>
    public ReadOnlyCollection<DomainExceptionMessage> Messages { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="messages">メッセージリスト</param>
    /// <remarks>Presentationで補足する場合はApplicationのExceptionModelを利用する</remarks>
    public DomainException(ReadOnlyCollection<DomainExceptionMessage> messages)
    {
      Messages = messages;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="messages">メッセージリスト</param>
    /// <param name="innerException">内部例外エラー</param>
    /// <remarks>Presentationで補足する場合はApplicationのExceptionModelを利用する</remarks>
    public DomainException(ReadOnlyCollection<DomainExceptionMessage> messages, Exception innerException) : base(innerException.Message)
    {
      Messages = messages;
    }
  }
}
