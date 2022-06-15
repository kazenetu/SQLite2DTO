using Domain.Exceptions;
using System;
using System.Text;

namespace Application.Model
{
  /// <summary>
  /// アプリケーションレイヤー用例外クラス
  /// </summary>
  public class ExceptionModel : Exception
  {
    /// <summary>
    /// ドメインレイヤー用例外クラス インスタンス
    /// </summary>
    private DomainException domainException = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="exception">ドメインレイヤー用例外クラス</param>
    /// <remarks>ドメインレイヤー用例外クラス用途</remarks>
    public ExceptionModel(Exception exception) : base(exception.ToString())
    {
      if (exception is DomainException)
      {
        domainException = exception as DomainException;
      }
      if (exception.InnerException is DomainException)
      {
        domainException = exception.InnerException as DomainException;

      }
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="exception">ドメインレイヤー用例外クラス</param>
    /// <remarks>アプリケーションレイヤー生成用途</remarks>
    public ExceptionModel(string message) : base(message)
    {
    }

    /// <summary>
    /// 例外メッセージを返す
    /// </summary>
    /// <returns>例外メッセージ</returns>
    public override string ToString()
    {
      if (domainException is null)
      {
        return this.Message;
      }
      else
      {
        // ドメインレイヤー用例外の場合はメッセージを取得する
        var result = new StringBuilder();
        foreach (var item in domainException.Messages)
        {
          result.AppendLine(GetDomainMessage(item));
        }
        return result.ToString();
      }
    }

    /// <summary>
    /// 例外メッセージを返す
    /// </summary>
    /// <param name="message">対象項目とメッセージIDの文字列</param>
    /// <returns>例外メッセージ</returns>
    private string GetDomainMessage(DomainExceptionMessage message)
    {
      switch (message.MessageID)
      {
        case DomainExceptionMessage.ExceptionType.Empty:
          return $"Empty: {message.Target}";
        case DomainExceptionMessage.ExceptionType.DBError:
          return $"DBError: {message.Target}";
        case DomainExceptionMessage.ExceptionType.ParameterError:
          return $"parameterError: {message.Target}";
        case DomainExceptionMessage.ExceptionType.FileOutputError:
          return $"OutputFileError: {message.Target}";
        default:
          return $"UnknownError: {message.Target}";
      }
    }
  }
}
