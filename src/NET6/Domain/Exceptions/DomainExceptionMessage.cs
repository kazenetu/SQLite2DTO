namespace Domain.Exceptions
{
  /// <summary>
  /// 例外種別
  /// </summary>
  public enum ExceptionType
  {
    Empty,
    ParameterError,
    DBError,
    FileOutputError
  }

  /// <summary>
  /// ドメインレイヤー用例外クラス メッセージクラス
  /// </summary>
  public class DomainExceptionMessage
  {
    /// <summary>
    /// 例外種別
    /// </summary>
    [System.Obsolete]
    public enum ExceptionType
    {
      Empty,
      ParameterError,
      DBError,
      FileOutputError
    }


    /// <summary>
    /// 対象項目
    /// </summary>
    public string Target { get; init; }

    /// <summary>
    /// メッセージID
    /// </summary>
    public ExceptionType MessageID { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="target">対象項目</param>
    /// <param name="messageId">メッセージID</param>
    public DomainExceptionMessage(string target, ExceptionType messageId)
    {
      Target = target;
      MessageID = messageId;
    }
  }
}