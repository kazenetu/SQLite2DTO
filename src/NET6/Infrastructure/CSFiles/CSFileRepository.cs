using Domain.Classes;
using Domain.CSFiles;
using Domain.Exceptions;
using Infrastructure.CSFiles.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Infrastructure.CSFiles
{
  /// <summary>
  /// CSファイル出力リポジトリ
  /// </summary>
  public class CSFileRepository : ICSFileRepository
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
      if (classEntities.Count == 0) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(classEntities)}[{classEntities}]", ExceptionType.Empty));
      if (fileDataEntity is null) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(fileDataEntity)}[{fileDataEntity}]", ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      var result = new List<string>();
      try
      {
        foreach (var classEntity in classEntities)
        {
          // フォルダの存在確認とフォルダ作成
          if (!Directory.Exists(fileDataEntity.OutputPath))
          {
            Directory.CreateDirectory(fileDataEntity.OutputPath);
          }

          var createCS = new CreateCS(classEntity, fileDataEntity.NameSpace, useSnakeCase);
          var filePath = Path.Combine(fileDataEntity.OutputPath, createCS.FileName);

          // ファイル出力
          var message = new StringBuilder();
          message.Append($"  >>{classEntity.Name}... ");
          CreateFile(createCS, filePath, fileDataEntity.NameSpace, useSnakeCase);
          message.AppendLine("finish");

          result.Add(message.ToString());
        }
      }
      catch (System.Exception ex)
      {
        var messages = new List<DomainExceptionMessage>();
        messages.Add(new DomainExceptionMessage($"{ex.Message}", ExceptionType.FileOutputError));
        throw new DomainException(messages.AsReadOnly(), ex);
      }

      return result.AsReadOnly();
    }

    /// <summary>
    /// ファイル出力
    /// </summary>
    /// <param name="createCS">C#ソースコード生成クラス</param>
    /// <param name="filePath">C#ファイルパス</param>
    /// <param name="nameSpace">名前空間</param>
    /// <param name="useSnakeCase">スネークケースのままとするか</param>
    private void CreateFile(CreateCS createCS, string filePath, string nameSpace, bool useSnakeCase)
    {
      var csSource = ((ITransformText)createCS).TransformText();

      // ファイル出力
      using (FileStream fs = File.OpenWrite(filePath))
      {
        Byte[] info = new UTF8Encoding(true).GetBytes(csSource);
        fs.Write(info, 0, info.Length);
      }
    }
  }
}
