namespace Domain.Exceptions
{
  /// <summary>
  /// ドメインレイヤー用例外クラス メッセージクラス
  /// </summary>
  public class DomainExceptionMessage
  {
    /// <summary>
    /// 区切り文字
    /// </summary>
    public const string Delimiter = ":";

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
    /// 対象項目
    /// </summary>
    public string Target { get; private set; }

    /// <summary>
    /// メッセージID
    /// </summary>
    public ExceptionType MessageID { get; private set; }

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