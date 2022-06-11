using Domain.Classes;
using Domain.CSFiles;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PostgreSQL2DTOTest.Shared
{
  /// <summary>
  /// CSファイル出力モックリポジトリ
  /// </summary>
  /// <remarks>Applicatton用</remarks>
  public class MockCSFileRepository : ICSFileRepository
  {
    /// <summary>
    /// CSファイル出力
    /// </summary>
    /// <param name="classEntities">出力対象のクラスエンティティリスト</param>
    /// <param name="fileDataEntity">出力情報</param>
    /// <param name="useSnakeCase">スネークケースのままとするか</param>
    /// <returns>出力ファイル名リスト</returns>
    public ReadOnlyCollection<string> Generate(List<ClassEntity> classEntities, FileDataEntity fileDataEntity, bool useSnakeCase)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (classEntities.Count == 0) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(classEntities)}[{classEntities}]", DomainExceptionMessage.ExceptionType.Empty));
      if (fileDataEntity is null) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(fileDataEntity)}[{fileDataEntity}]", DomainExceptionMessage.ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      var result = new List<string>();
      try
      {
        foreach (var classEntity in classEntities)
        {
          // ファイル出力
          var message = new StringBuilder();
          message.Append($"  >>{classEntity.Name}... ");
          message.AppendLine("finish");

          result.Add(message.ToString());
        }
      }
      catch (System.Exception ex)
      {
        var messages = new List<DomainExceptionMessage>();
        messages.Add(new DomainExceptionMessage($"{ex.Message}", DomainExceptionMessage.ExceptionType.FileOutputError));
        throw new DomainException(messages.AsReadOnly(), ex);
      }

      return result.AsReadOnly();
    }
  }
}
