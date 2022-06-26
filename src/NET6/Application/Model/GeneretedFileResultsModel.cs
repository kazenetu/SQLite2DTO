using System.Collections.ObjectModel;

namespace Application.Model
{
  /// <summary>
  /// 生成結果モデル
  /// </summary>
  public class GeneretedFileResultsModel
  {
    /// <summary>
    /// 生成結果メッセージ
    /// </summary>
    public ReadOnlyCollection<string> Messages { get; init; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="messages">生成結果メッセージ</param>
    public GeneretedFileResultsModel(ReadOnlyCollection<string> messages)
    {
      Messages = messages;
    }
  }
}
