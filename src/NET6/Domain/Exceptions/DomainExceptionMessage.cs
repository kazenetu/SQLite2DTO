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
  /// <param name="Target">対象項目</param>
  /// <param name="MessageID">メッセージID</param>
  public record DomainExceptionMessage(string Target, ExceptionType MessageID);
}