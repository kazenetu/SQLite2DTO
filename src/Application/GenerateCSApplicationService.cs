using System.Collections.Generic;
using Application.Model;
using Domain.CSFiles;
using Domain.DB;
using Domain.Exceptions;
using TinyDIContainer;

namespace Application
{
  /// <summary>
  /// CSファイル作成アプリケーションサービス
  /// </summary>
  public class GenerateCSApplicationService
  {
    /// <summary>
    /// DBリポジトリ
    /// </summary>
    private IDBRepository dbRepository;

    /// <summary>
    /// CSファイル出力リポジトリ
    /// </summary>
    private ICSFileRepository csFileRepository;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GenerateCSApplicationService()
    {
      // DIコンテナからリポジトリインスタンスを取得
      dbRepository = DIContainer.CreateInstance<IDBRepository>();
      csFileRepository = DIContainer.CreateInstance<ICSFileRepository>();
    }

    /// <summary>
    /// DB情報からCSファイル生成
    /// </summary>
    /// <param name="inputParamModel">DB情報や出力ファイル設定のインスタンス</param>
    /// <returns>生成結果メッセージリスト</returns>
    public GeneretedFileResultsModel GenerateCSFileFromDB(InputParamModel inputParamModel)
    {
      // パラメーターチェック
      var exceptionMessages = new List<DomainExceptionMessage>();
      if (inputParamModel is null) exceptionMessages.Add(new DomainExceptionMessage($"{nameof(inputParamModel)}[{inputParamModel}]", DomainExceptionMessage.ExceptionType.Empty));
      if (exceptionMessages.Count > 0) throw new DomainException(exceptionMessages.AsReadOnly());

      var classes = dbRepository.GetClasses(DBParameterEntity.Create(inputParamModel.SQLiteFilePath));
      var messages = csFileRepository.Generate(classes, FileDataEntity.Create(inputParamModel.OutputPath, inputParamModel.NameSpace), inputParamModel.UseSnakeCase);

      return new GeneretedFileResultsModel(messages);
    }
  }
}
