using Domain.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.CSFiles
{
  /// <summary>
  /// CSファイル出力リポジトリ インターフェイス
  /// </summary>
  public interface ICSFileRepository
  {
    /// <summary>
    /// CSファイル出力
    /// </summary>
    /// <param name="classEntities">出力対象のクラスエンティティリスト</param>
    /// <param name="fileDataEntity">出力情報</param>
    /// <returns>出力ファイル名リスト</returns>
    ReadOnlyCollection<string> Generate(List<ClassEntity> classEntities, FileDataEntity fileDataEntity);
  }
}