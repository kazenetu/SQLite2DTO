using System.Collections.ObjectModel;

namespace Application.Model
{
  /// <summary>
  /// 生成結果モデル
  /// </summary>
  /// <param name="Messages">生成結果メッセージ</param>
  public record GeneretedFileResultsModel(ReadOnlyCollection<string> Messages);
}
