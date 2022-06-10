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
    /// メッセージIDリスト
    /// </summary>
    [Obsolete]
    public ReadOnlyCollection<DomainExceptionMessage> MessageIds { get; private set; }

    /// <summary>
    /// メッセージリスト
    /// </summary>
    public ReadOnlyCollection<DomainExceptionMessage> Messages { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="messageIds">メッセージIDリスト</param>
    /// <remarks>Presentationで補足する場合はApplicationのExceptionModelを利用する</remarks>
    public DomainException(ReadOnlyCollection<DomainExceptionMessage> messageIds)
    {
      MessageIds = messageIds;
      Messages = messageIds;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="messageIds">メッセージIDリスト</param>
    /// <param name="innerException">内部例外エラー</param>
    /// <remarks>Presentationで補足する場合はApplicationのExceptionModelを利用する</remarks>
    public DomainException(ReadOnlyCollection<DomainExceptionMessage> messageIds, Exception innerException) : base(innerException.Message)
    {
      MessageIds = messageIds;
      Messages = messageIds;
    }
  }
}
