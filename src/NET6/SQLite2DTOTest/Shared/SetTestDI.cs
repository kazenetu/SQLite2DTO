using Domain.CSFiles;
using Domain.DB;
using TinyDIContainer;

namespace PostgreSQL2DTOTest.Shared
{
  /// <summary>
  /// テスト用DI設定
  /// </summary>
  public class SetTestDI
  {
    /// <summary>
    /// セットアップ済み
    /// </summary>
    private static bool Setuped = false;

    /// <summary>
    /// 非公開コンストラクタ
    /// </summary>
    private SetTestDI()
    {
    }

    /// <summary>
    /// DIコンテナ設定
    /// </summary>
    public static void SetDI()
    {
      if (Setuped) return;

      DIContainer.Add<IDBRepository, MockDBRepository>();
      DIContainer.Add<ICSFileRepository, MockCSFileRepository>();

      Setuped = true;
    }
  }
}