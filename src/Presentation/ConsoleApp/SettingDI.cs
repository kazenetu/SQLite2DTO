using Domain.CSFiles;
using Domain.DB;
using Infrastructure.CSFiles;
using Infrastructure.DB;
using TinyDIContainer;

/// <summary>
/// DIコンテナ設定クラス
/// </summary>
public class SettingDIContainer
{
  /// <summary>
  /// 非公開コンストラクタ
  /// </summary>
  private SettingDIContainer()
  {
  }

  /// <summary>
  /// DIコンテナ設定
  /// </summary>
  public static void SetDI()
  {
    DIContainer.Add<IDBRepository, DBRepository>();
    DIContainer.Add<ICSFileRepository, CSFileRepository>();
  }
}